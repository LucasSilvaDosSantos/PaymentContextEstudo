using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests.Commands
{
    //Este é um command que não faria tanto sentido ser testado, porem aqui fica o exemplo de como seria possivel
    [TestClass]
    public class CreateBoletoSusbcriptionCommandTest
    {
        [TestMethod]
        public void ShouldReturnErrorWhenNameIsInvalid()
        {
            var command = new CreateBoletoSusbcriptionCommand();
            command.FirstName = "";

            command.Validate();

            Assert.IsFalse(command.IsValid);
        }
    }
}
