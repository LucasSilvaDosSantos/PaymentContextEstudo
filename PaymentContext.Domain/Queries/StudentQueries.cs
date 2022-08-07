using PaymentContext.Domain.Entities;
using System.Linq.Expressions;

namespace PaymentContext.Domain.Queries
{
    public static class StudentQueries
    {
        // ToDo: Estudar + sobre Expression; isso é uma query porem escrida de uma forma tranquila via linq 
        public static Expression<Func<Student, bool>> GetStudentInfo(string document)
        {
            return _ => _.Document.Number == document;
        }
    }
}
