using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using System;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentTest
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Email _email;
        private readonly Address _address;
        private readonly Student _student;
        private readonly Subscription _subscription;

        public StudentTest()
        {
            _name = new Name("Bruce", "Wayne");
            _email = new Email("test@test.com");
            _address = new Address("Rua 1", "1234", "Bairro Legal", "Aqui", "SP", "BR", "1340000");
            _document = new Document("35111507795", EDocumentType.CPF);
            _student = new Student(_name, _document, _email);
            _subscription = new Subscription(null);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActivateSubscription()
        {
            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "WAYNE", _document, _address, _email);

            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsFalse(_student.IsValid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenSubscriptionNoPayment()
        {
            _student.AddSubscription(_subscription);
            
            Assert.IsFalse(_student.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "WAYNE", _document, _address, _email);

            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            
            Assert.IsTrue(_student.IsValid);
        }
    }
}
