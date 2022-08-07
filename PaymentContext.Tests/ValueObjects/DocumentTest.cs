using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTest
    {
        [TestMethod]
        public void ShouldReturnErrorWhenCNPJIsInvalid()
        {
            var doc = new Document("123", EDocumentType.CNPJ);

            Assert.IsFalse(doc.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSucessWhenCNPJIsValid()
        {
            var doc = new Document("34110468000150", EDocumentType.CNPJ);

            Assert.IsTrue(doc.IsValid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenCPFIsInvalid()
        {
            var doc = new Document("123", EDocumentType.CPF);

            Assert.IsFalse(doc.IsValid);
        }

        // Multiple values ​​for testing
        [TestMethod]
        [DataTestMethod]
        [DataRow("34225545806")]
        [DataRow("54139739347")]
        [DataRow("01077284608")]
        public void ShouldReturnSucessWhenCPFIsValid(string cpf)
        {
            var doc = new Document(cpf, EDocumentType.CPF);

            Assert.IsTrue(doc.IsValid);
        }
    }
}
