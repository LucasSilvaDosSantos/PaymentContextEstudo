using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable<Notification>, IHandler<CreateBoletoSusbcriptionCommand>, IHandler<CreatePayPalSusbcriptionCommand>
    {
        public readonly IStudentRepository _repository;
        public readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handler(CreateBoletoSusbcriptionCommand command)
        {
            // Fail fast Validation
            command.Validate();
            if (!command.IsValid)
            {
                //Para agrupar as notificações do commando no handler
                AddNotifications(command);
                return new CommandResult(false, "Não foi possivel realizar sua assinatura");
            }

            //Verificar se documento já esta cadastrado 
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já esta em uso");

            //Verificar se email já esta cadastrado
            if (_repository.DocumentExists(command.Email))
                AddNotification("Email", "Este E-mail já esta em uso");

            //Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            //Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1)); //Apenas exemplo
            var payment = new BoletoPayment(
                command.BarCode, 
                command.BoletoNumber, 
                command.PaidDate, 
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid, 
                command.Payer, 
                new Document(command.PayerDocument, command.PayerDocumentType), 
                address, 
                email
            );

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Checar as notificações 
            if (!IsValid)
                return new CommandResult(false, "Não foi possivel realizar sua assinatura");

            //Salvar as Informações
            _repository.CreateSubscription(student);

            //Enviar Email de boas vindas 
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao Teste de DDD", "Sua assinatura foi criada");

            //Retornar Informações
            return new CommandResult(true, "Assinatura Realizada com sucesso");
        }


        //Igual ao alterior, com pequenas mudança, para Credit Card a ideia é a mesma 
        public ICommandResult Handler(CreatePayPalSusbcriptionCommand command)
        {
            // Fail fast Validation
            //command.Validate(); Não foi implementado por questões de tempo
            //if (!command.IsValid)
            //{
            //    //Para agrupar as notificações do commando no handler
            //    AddNotifications(command);
            //    return new CommandResult(false, "Não foi possivel realizar sua assinatura");
            //}

            //Verificar se documento já esta cadastrado 
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já esta em uso");

            //Verificar se email já esta cadastrado
            if (_repository.DocumentExists(command.Email))
                AddNotification("Email", "Este E-mail já esta em uso");

            //Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            //Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1)); //Apenas exemplo
            var payment = new PayPalPayment(
                command.TransactionCode,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address,
                email
            );

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Salvar as Informações
            _repository.CreateSubscription(student);

            //Enviar Email de boas vindas 
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao Teste de DDD", "Sua assinatura foi criada");

            //Retornar Informações
            return new CommandResult(true, "Assinatura Realizada com sucesso");
        }
    }
}
