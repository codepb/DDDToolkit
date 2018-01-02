using DDDToolkit.Core;
using System;

namespace DDDToolkit.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new Aggregate();
            a.ApplyChange(new Event1());
            a.ApplyChange(new Event2());
            a.ApplyChange(new Event3());
            Console.ReadLine();
        }
    }

    public class Aggregate : AggregateRoot<int>
    {
        public void ApplyChange(IDomainEvent domainEvent)
        {
            Apply(domainEvent);
        }

        protected void Handle(Event1 @event)
        {
            Console.WriteLine("Event 1");
        }

        protected void Handle(Event2 @event)
        {
            Console.WriteLine("Event 2");
        }

        protected void Handle(IDomainEvent @event)
        {
            Console.WriteLine("IDomainEvent");
        }
    }

    public class Event1 : IDomainEvent
    {
        public int Version { get; set; }
    }

    public class Event2 : Event1
    {
    }

    public class Event3 : IDomainEvent
    {
        public int Version { get; set; }
    }
}
