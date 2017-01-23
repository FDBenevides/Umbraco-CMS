using System;

namespace Umbraco.Core.Events
{
    public abstract class EventDefinitionBase : IEventDefinition, IEquatable<EventDefinitionBase>
    {
        protected EventDefinitionBase(object sender, object args, string eventName)
        {
            if (sender == null) throw new ArgumentNullException("sender");
            if (eventName == null) throw new ArgumentNullException("eventName");
            Sender = sender;
            Args = args;
            EventName = eventName;
        }

        protected EventDefinitionBase(object sender, object args)
        {
            if (sender == null) throw new ArgumentNullException("sender");
            if (args == null) throw new ArgumentNullException("args");
            Sender = sender;
            Args = args;
            var findResult = EventNameExtractor.FindEvent(sender, args, EventNameExtractor.MatchIngNames);            
            if (findResult.Success == false)
                throw new InvalidOperationException("Could not automatically find the event name, the event name will need to be explicitly registered for this event definition. Error: " + findResult.Result.Error);
            EventName = findResult.Result.Name;
        }

        public object Sender { get; private set; }
        public object Args { get; private set; }
        public string EventName { get; private set; }
        
        public abstract void RaiseEvent();

        public bool Equals(EventDefinitionBase other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Sender.Equals(other.Sender) && string.Equals(EventName, other.EventName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EventDefinitionBase) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Sender.GetHashCode() * 397) ^ EventName.GetHashCode();
            }
        }

        public static bool operator ==(EventDefinitionBase left, EventDefinitionBase right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EventDefinitionBase left, EventDefinitionBase right)
        {
            return !Equals(left, right);
        }
    }
}