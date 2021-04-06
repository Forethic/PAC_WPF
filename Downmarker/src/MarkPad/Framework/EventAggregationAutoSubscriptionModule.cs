using Autofac;
using Autofac.Core;
using Caliburn.Micro;

namespace MarkPad.Framework
{
    public class EventAggregationAutoSubscriptionModule : Module
    {
        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            registration.Activated += OnComponentActivated;
        }

        private void OnComponentActivated(object sender, ActivatedEventArgs<object> e)
        {
            // we never want to fail, so check for null (should never happen), and return if it is
            if (e == null) return;

            if (e.Instance is IHandle handler)
                e.Context.Resolve<IEventAggregator>().Subscribe(handler);
        }
    }
}