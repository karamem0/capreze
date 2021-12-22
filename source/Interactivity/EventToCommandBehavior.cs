using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Karamem0.Capreze.Interactivity
{

    public class EventToCommandBehavior : Behavior<DependencyObject>
    {

        public static readonly DependencyProperty EventNameProperty =
            DependencyProperty.Register(
                "EventName",
                typeof(string),
                typeof(EventToCommandBehavior)
            );

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                "Command",
                typeof(ICommand),
                typeof(EventToCommandBehavior)
            );

        private EventInfo eventInfo;

        private Delegate eventDelegate;

        public EventToCommandBehavior()
        {
        }

        public string EventName
        {
            get => (string)this.GetValue(EventNameProperty);
            set => this.SetValue(EventNameProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)this.GetValue(CommandProperty);
            set => this.SetValue(CommandProperty, value);
        }

        protected override void OnAttached()
        {
            if (this.EventName is not null)
            {
                this.eventInfo = this.AssociatedObject.GetType().GetEvent(this.EventName);
                this.eventDelegate = this.GetType()
                    .GetMethod("OnEventRaised", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .CreateDelegate(this.eventInfo.EventHandlerType, this);
                this.eventInfo.AddEventHandler(this.AssociatedObject, this.eventDelegate);
            }
        }

        protected override void OnDetaching()
        {
            if (this.EventName is not null)
            {
                this.eventInfo.RemoveEventHandler(this.AssociatedObject, this.eventDelegate);
            }
        }

        protected void OnEventRaised(object sender, EventArgs e)
        {
            if (this.Command is not null)
            {
                if (this.Command.CanExecute(e))
                {
                    this.Command.Execute(e);
                }
            }
        }

    }

}
