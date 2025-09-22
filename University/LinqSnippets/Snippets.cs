using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;

namespace LinqSnippets
{
    public class Snippets
    {
        static public void BasicLinQ()
        {
            string[] cars =
            {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat León"
            };

            // 1. SELECT * of cars
            var carList1 = from car in cars
                           select car;

            foreach (var car in carList1)
            {
                Console.WriteLine(car);
            }

            // 2. SELECT WHERE Audi
            var audiList = from car in cars
                           where car.Contains("Audi")
                           select car;

            foreach (var car in audiList)
            {
                Console.WriteLine(car);
            }

        }

        // Number examples
        static public void LinqNumbers()
        {
            List<int> numbers = Enumerable.Range(1, 9).ToList();

            var processedNumberList =
                numbers
                    .Select(n => n * 3)
                    .Where(n => n != 9)
                    .OrderBy(n => n);
        }

        static public void SearchExamples()
        {
            List<string> textList = new List<string>() { "a", "bx", "c", "d", "e", "cj", "f", "c" };

            var first = textList.First();
            var cText = textList.First(t => t.Equals("c"));
            var jText = textList.First(t => t.Contains("j"));
            var zText = textList.FirstOrDefault(t => t.Contains("z"));
            var uniqueTexts = textList.SingleOrDefault();

            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2, 6 };

            var nums = evenNumbers.Except(otherEvenNumbers);
        }

        static public void MultipleSelects()
        {
            string[] myOpinions =
            {
                "Opinión 1, text 1",
                "Opinión 2, text 2",
                "Opinión 3, text 3",
            };

            var myOpinionSelection = myOpinions.SelectMany(op => op.Split(","));

            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id = 1,
                    Name = "Enterprise 1",
                    Employees = new[]
                    {
                        new Employee()
                        {
                            Id = 1,
                            Name = "Martín",
                            Email = "martin@imaginagroup.com",
                            Salary = 3000
                        },
                        new Employee()
                        {
                            Id = 2,
                            Name = "Pepe",
                            Email = "pepe@imaginagroup.com",
                            Salary = 1000
                        },
                        new Employee()
                        {
                            Id = 3,
                            Name = "Juanjo",
                            Email = "juanjo@imaginagroup.com",
                            Salary = 2000
                        }
                    }
                },
                new Enterprise()
                {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees = new[]
                    {
                        new Employee()
                        {
                            Id = 4,
                            Name = "Ana",
                            Email = "ana@imaginagroup.com",
                            Salary = 3000
                        },
                        new Employee()
                        {
                            Id = 5,
                            Name = "Maria",
                            Email = "maria@imaginagroup.com",
                            Salary = 1500
                        },
                        new Employee()
                        {
                            Id = 6,
                            Name = "Marta",
                            Email = "marta@imaginagroup.com",
                            Salary = 4000
                        }
                    }
                },
            };

            // Obtain all employees
            var employeeList = enterprises.SelectMany(e => e.Employees);
            // Boolean checks
            bool hasEnterprises = enterprises.Any();
            bool hasEmployees = enterprises.Any(e => e.Employees.Any());
            bool hasEmployeeWithSalaryGrater1000 = enterprises.Any(ent => ent.Employees.Any(emp => emp.Salary > 1000));
        }

        public static void LinqCollections()
        {
            var firstList = new List<string>() { "a", "b", "c" };
            var secondList = new List<string>() { "a", "b", "d" };

            var innerJoin = from element in firstList
                            join element2 in secondList
                            on element equals element2
                            select new { element, element2 };

            var innerJoin2 = firstList.Join(
                    secondList,
                    element => element,
                    element2 => element2,
                    (element, element2) => new { element, element2 }
                );

            var leftOuterJoin = from element in firstList
                                join element2 in secondList
                                on element equals element2
                                into temporalList
                                from tempElement in temporalList.DefaultIfEmpty()
                                where element != tempElement
                                select new { Element = element };

            var leftOuterJoin2 = from element in firstList
                                 from element2 in secondList.Where(s => s == element).DefaultIfEmpty()
                                 select new { Element = element };

            var rightOuterJoin = from element2 in secondList
                                 join element in firstList
                                 on element2 equals element
                                 into temporalList
                                 from tempElement in temporalList.DefaultIfEmpty()
                                 where element2 != tempElement
                                 select new { Element = element2 };

            var union = leftOuterJoin.Union(rightOuterJoin);
        }

        static public void SkipTakeLinq()
        {
            var myList = new[]
            {
                1,2,3,4,5,6,7,8,9,10
            };

            // SKIP
            var skipFirstTwo = myList.Skip(2);
            var skipLastTwo = myList.SkipLast(2);
            var skipWhile = myList.SkipWhile(n => n < 4);

            // TAKE
            var takeFirstTwo = myList.Take(2);
            var takeLastTwo = myList.TakeLast(2);
            var takeWhile = myList.TakeWhile(n => n < 4);
        }

    }
}
