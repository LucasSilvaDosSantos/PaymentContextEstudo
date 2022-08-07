using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;
using System;

namespace PaymentContext.Tests.Handler
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShoudReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSusbcriptionCommand();
            
            //Realmente tinha que ter tudo isso? 
            command.BarCode = "123456789123";
            command.FirstName = "Lucas";
            command.LastName = "Santos";
            command.Document = "99999999999";
            command.Email = "test@test.com";
            command.BarCode = "1234567989";
            command.BoletoNumber = "1234567989";
            command.PaymentNumber = "1234567989";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 1;
            command.TotalPaid = 1;
            command.PayerDocument = "12345678911";
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = "t@t.com";
            command.Street = "qwer";
            command.Number = "qewr";
            command.Neighborhood = "qewr";
            command.City = "qwer";
            command.State = "qewr";
            command.Country = "qwer";
            command.ZipCode = "qwer";

            // Achei esse teste meio fraco, o assert não esta validando exatamente o documento, e sim qualquer coisa que esteja invalida
            handler.Handler(command);
            Assert.IsFalse(handler.IsValid);
        }
    }
}
