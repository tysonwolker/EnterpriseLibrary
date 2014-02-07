using System;
using System.Reflection;
using LayerArchitecture.Core.DependencyResolution;
namespace LayerArchitecture.Core.Communication.Base
{
    public class Mediator : IMediator
    {
        private readonly IDependencyResolver _dependencyResolver;

        public Mediator(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public virtual Response<TResponseData> Request<TResponseData>(IQuery<TResponseData> query)
        {
            var response = new Response<TResponseData>();

            try
            {
                var plan = new MediatorPlan<TResponseData>(typeof(IQueryHandler<,>), "Handle", query.GetType(), _dependencyResolver);

                response.Data = plan.Invoke(query);
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        public Response<TResponseData> Send<TResponseData>(ICommand<TResponseData> command)
        {
            var response = new Response<TResponseData>();

            try
            {
                var plan = new MediatorPlan<TResponseData>(typeof(ICommandHandler<,>), "Handle", command.GetType(), _dependencyResolver);

                response.Data = plan.Invoke(command);
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        class MediatorPlan<TResult>
        {
            readonly MethodInfo _handleMethod;
            readonly Func<object> _handlerInstanceBuilder;

            public MediatorPlan(Type handlerTypeTemplate, string handlerMethodName, Type messageType, IDependencyResolver dependencyResolver)
            {
                var handlerType = handlerTypeTemplate.MakeGenericType(messageType, typeof(TResult));

                _handleMethod = GetHandlerMethod(handlerType, handlerMethodName, messageType);

                _handlerInstanceBuilder = () => dependencyResolver.GetInstance(handlerType);
            }

            MethodInfo GetHandlerMethod(Type handlerType, string handlerMethodName, Type messageType)
            {
                return handlerType
                    .GetMethod(handlerMethodName,
                        BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod,
                        null, CallingConventions.HasThis,
                        new[] { messageType },
                        null);
            }

            public TResult Invoke(object message)
            {
                return (TResult)_handleMethod.Invoke(_handlerInstanceBuilder(), new[] { message });
            }
        }
    }
}
