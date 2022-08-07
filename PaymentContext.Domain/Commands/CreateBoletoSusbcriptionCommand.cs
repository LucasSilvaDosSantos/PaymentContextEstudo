using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands
{
    public class CreateBoletoSusbcriptionCommand : Notifiable<Notification>, ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string BarCode { get; set; }
        public string BoletoNumber { get; set; }
        public string PaymentNumber { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string Payer { get; private set; }
        public string PayerDocument { get; set; }
        public EDocumentType PayerDocumentType { get; set; }
        public string PayerEmail { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public void Validate()
        {
            //Pode colocar as validações aqui, e caso faça isso não prescisa validar nas classes, ou deixar no vo que usar de lá, não colocar regra de negócio aqui
            AddNotifications(new Contract<Name>()
                .Requires()
                .IsGreaterOrEqualsThan(FirstName, 3, "Name.FirstName", "Nome deve conter pelomenos 3 caracteres")
                .IsGreaterOrEqualsThan(LastName, 3, "Sobrenome.FirstName", "Sobrenome deve conter pelomenos 3 caracteres")
            );
        }
    }
}
