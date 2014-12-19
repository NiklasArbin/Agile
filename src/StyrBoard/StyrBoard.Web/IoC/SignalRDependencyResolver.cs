using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Castle.Windsor;

namespace StyrBoard.Web.IoC
{
    public class SignalRDependencyResolver : Microsoft.AspNet.SignalR.DefaultDependencyResolver
    {
        private readonly IWindsorContainer _container;

        public SignalRDependencyResolver(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            _container = container;
        }

        public override object GetService(Type serviceType)
        {
            return TryGet(serviceType) ?? base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            return TryGetAll(serviceType).Concat(base.GetServices(serviceType));
        }

        [DebuggerStepThrough]
        private object TryGet(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private IEnumerable<object> TryGetAll(Type serviceType)
        {
            try
            {
                var array = _container.ResolveAll(serviceType);
                return array.Cast<object>().ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}