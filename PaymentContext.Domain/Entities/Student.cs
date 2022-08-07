using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscripions;

        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscripions = new List<Subscription>();

            //Group Errors
            AddNotifications(name, document, email);
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscripions { get { return _subscripions.ToArray(); } }

        public void AddSubscription(Subscription subscripion)
        {
            var hasSubscriptionActive = false;
            foreach (var subscription in _subscripions)
            {
                if (subscripion.Active)
                    hasSubscriptionActive = true;
            }

            AddNotifications(new Contract<Student>()
                .Requires()
                .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Voce já tem uma assinatura ativa")
                .IsGreaterThan(0, subscripion.Payments.Count, "Student.Subscription.Payments", "Esta assinatura não possui pagamentos")
            );

            //Alternative, not using Contract
            //if (hasSubscriptionActive)
            //    AddNotification("Student.Subscriptions", "Voce já tem uma assinatura ativa");
            
        }
    }
}
