using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace PaymentContext.Tests.Queries
{
    [TestClass]
    public class StudentQueriesTest
    {
        private IList<Student> _students;

        [TestInitialize]
        public void Setup()
        {
            _students = new List<Student>();
            for (int i = 0; i < 10; i++)
            {
                _students.Add(
                    new Student(
                        new Name("Aluno", "Silva"), 
                        new Document("1111111111" + i, Domain.Enums.EDocumentType.CPF), 
                        new Email("t@t.com")));
            }
        }

        [TestMethod]
        public void ShoudReturnNullWhenDocumentNotExists()
        {
            var exp = StudentQueries.GetStudentInfo("12345678911");

            var student = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.IsNull(student);
        }

        [TestMethod]
        public void ShoudReturnStudentWhenDocumentExists()
        {
            var exp = StudentQueries.GetStudentInfo("11111111115");

            var student = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.IsNotNull(student);
        }
    }
}
