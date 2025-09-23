using LinqSnippets.Classes;
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

        // Paging using Skip & Take
        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int pageElements)
        {
            int startIndex = (pageNumber - 1) * pageElements;
            return collection.Skip(startIndex).Take(pageElements);
        }

        // Variables
        static public void LinqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var aboveAverage = from number in numbers
                               let average = numbers.Average()
                               let nSquared = Math.Pow(number, 2)
                               where nSquared > average
                               select number;

            Console.WriteLine($"Average: {numbers.Average()}");
            foreach (int number in aboveAverage)
            {
                Console.WriteLine($"Number: {number}, Squared: {Math.Pow(number, 2)}");
            }
        }

        // ZIP
        static public void LinqZip()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] stringNumbers = { "one", "two", "three", "four", "five" };

            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => $"{number} = {word}");
        }

        // Repeat & Range
        static public void LinqRepeatRange()
        {
            // Generate collection from 1 to 1000
            var first1000 = Enumerable.Range(1, 1000);

            // Repeat value N times
            var fiveXs = Enumerable.Repeat("X", 5);
        }

        static public void LinqStudents()
        {
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Martín",
                    Grade = 90,
                    Certified = true,
                },
                new Student
                {
                    Id = 2,
                    Name = "Juan",
                    Grade = 50,
                    Certified = false,
                },
                new Student
                {
                    Id = 3,
                    Name = "Ana",
                    Grade = 95,
                    Certified = true,
                },
                new Student
                {
                    Id = 4,
                    Name = "Álvaro",
                    Grade = 10,
                    Certified = false,
                },
                new Student
                {
                    Id = 5,
                    Name = "Pedro",
                    Grade = 50,
                    Certified = true,
                },
            };

            var certifiedStudents = from student in classRoom
                                    where student.Certified
                                    select student;

            var notCertifiedStudents = from student in classRoom
                                       where !student.Certified
                                       select student;

            var approvedStudentsName = from student in classRoom
                                       where student.Grade >= 50 && student.Certified
                                       select student.Name;
        }

        // ALL
        static public void LinqAll()
        {
            var numbers = new List<int>() { 1, 2, 3, 4, 5 };
            bool areAllSmallerThan10 = numbers.All(n => n < 10); // true
            bool areAllGraterThan2 = numbers.All(n => n > 2); // false

            var emptyList = new List<int>();
            bool allNumbersAreGreaterThan0 = emptyList.All(n => n > 0); // true
        }

        // Aggregate
        static public void LinqAggregateQueries()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int sum = numbers.Aggregate((prevSum, current) => prevSum + current);

            string[] words = { "hello,", "my", "name", "is", "Martín" };
            string greeting = words.Aggregate((prevGreeting, current) => prevGreeting + " " + current);

        }

        // Distinct
        static public void DistinctValues()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };
            var distinctNumbers = numbers.Distinct();
        }

        // GroupBy
        static public void LinqGroupBy()
        {
            List<int> numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

            var groupedByEven = numbers.GroupBy(x => x % 2 == 0);

            foreach (var group in groupedByEven)
            {
                Console.WriteLine("Group by " + group.Key);
                foreach (var item in group)
                {
                    Console.WriteLine(item);
                }
            }

            // Another example
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Martín",
                    Grade = 90,
                    Certified = true,
                },
                new Student
                {
                    Id = 2,
                    Name = "Juan",
                    Grade = 50,
                    Certified = false,
                },
                new Student
                {
                    Id = 3,
                    Name = "Ana",
                    Grade = 95,
                    Certified = true,
                },
                new Student
                {
                    Id = 4,
                    Name = "Álvaro",
                    Grade = 10,
                    Certified = false,
                },
                new Student
                {
                    Id = 5,
                    Name = "Pedro",
                    Grade = 50,
                    Certified = true,
                },
            };

            var certifiedQuery = classRoom.GroupBy(x => x.Certified);
            // We obtain 2 groups
            // 1 - Not certified students
            // 2 - Certified students

            foreach (var group in certifiedQuery)
            {
                Console.WriteLine("Certified " + group.Key);
                foreach (var student in group)
                {
                    Console.WriteLine(student.Name);
                }
            }
        }

        static public void LinqRelations()
        {
            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Title = "My first post",
                    Content = "My first content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 1,
                            Created = DateTime.Now,
                            Title = "My first comment",
                            Content = "My content"
                        },
                        new Comment()
                        {
                            Id = 2,
                            Created = DateTime.Now,
                            Title = "My second comment",
                            Content = "My other content"
                        },
                    }
                },
                new Post()
                {
                    Id = 2,
                    Title = "My second post",
                    Content = "My second content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 3,
                            Created = DateTime.Now,
                            Title = "My other comment",
                            Content = "My new content"
                        },
                        new Comment()
                        {
                            Id = 4,
                            Created = DateTime.Now,
                            Title = "My other new comment",
                            Content = "My other new content"
                        },
                    }
                }
            };

            var commentsWithContent = posts.SelectMany(
                post => post.Comments,
                    (post, comment) => new { PostId = post.Id, CommentContent = comment.Content });
        }
    }
}
