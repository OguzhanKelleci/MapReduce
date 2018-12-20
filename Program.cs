using System;
using System.Linq;
using System.Collections.Generic;

namespace Her
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("-------------SQUARE---------------");

            var x = Foo.square(new int[] { 1, 2, 35, 7 });

            foreach (var item in x)
            {
                System.Console.WriteLine(item);
            }


            System.Console.WriteLine("-------------CONVERT---------------");

            var y = Foo.Convert(x);

            foreach (var item in y)
            {
                System.Console.WriteLine(item);
            }



            // #####################################

            var db = DB.testDb();

            // -------------MAP---------------

            // SELECT * FROM Employee
            var q = MapReduceExtension.Map(db.EmployeeTable, e => e);
            var q1 = db.EmployeeTable.Map(e => e);
            
            //SELECT id, name FROM Employee
            var q2 = MapReduceExtension.Map(db.EmployeeTable, e => e.Id);
            var q22 = db.EmployeeTable.Map(e => new Tuple<int, string>(e.Id, e.Name));



            // -------------FILTER---------------     

            // SELECT * FROM Employee WHERE Salary > 1500
            var q3 = db.EmployeeTable.Filter(e => e.Salary > 1500.0);

            //Select id,name FROM Empoloyee WHERE salary > 1500
            var q33 = db.EmployeeTable.Filter(e => e.Salary > 1500.0).Map(e => new Tuple<int, string>(e.Id, e.Name));

            // SELECT Sum(Salary) From Employee Where salary > 1500.0
            var q4 = db.EmployeeTable.Filter(e => e.Salary > 1500.0).Reduce(0.0, (sum, e) => sum + e.Salary);

            System.Console.WriteLine(q4);


            // -------------REDUCE--------------- 

            //SELECT * FROM Employee Where Salary > 1500.0
            var q5 = db.EmployeeTable.Reduce(
            new List<Employee>(),
            (el, e) =>
            {
                if (e.Salary > 1500)
                {
                    el.Add(e);

                }
                return el;
            });

            // foreach (var item in q5)
            // {
            //     System.Console.WriteLine($"{item.Id} {item.Name} {item.Email} {item.Salary}");
            // }


            // SELECT * FROM Employee, Task WHERE Empltoyee.Id == Task.EmployeeId
            var q6 = db.EmployeeTable.Join(db.TaskTable, (e, t) => e.Id == t.EmployeeId);

        }
    }


    class Foo
    {
        public static int[] square(int[] l)
        {

            int[] li = new int[l.Length];
            li = l;
            for (int i = 0; i < l.Length; i++)
            {
                li[i] = li[i] * li[i];
            }

            return li;
        }

        public static string[] Convert<T>(T[] l)
        {

            string[] ls = new string[l.Length];
            for (int i = 0; i < l.Length; i++)
            {
                ls[i] = l[i].ToString() + "a";
            }
            return ls;



        }
    }

    class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int EmployeeId { get; set; }

        public Task(int id, string title, int eid)
        {
            Id = id;
            Title = title;
            EmployeeId = eid;
        }

    }

    class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public double Salary { get; set; }

        public Employee(int id, string name, string email, double salary)
        {
            Id = id;
            Name = name;
            Email = email;
            Salary = salary;
        }

    }

    class DB
    {
        public IEnumerable<Employee> EmployeeTable { get; set; }
        public IEnumerable<Task> TaskTable { get; set; }
        public DB(IEnumerable<Employee> e, IEnumerable<Task> t)
        {
            EmployeeTable = e;
            TaskTable = t;
        }

        public static DB testDb()
        {
            Employee[] le = new Employee[] {
                    new Employee(1,"Sander", "Sander@test.com", 1200.00),
                    new Employee(1,"Tim", "tim@test.com", 1900.12),
                    new Employee(1,"Jan", "JAN@test.com", 2000.40),
                };

            Task[] t = new Task[]{
                new Task(1,"Instructions",1),
                new Task(2,"Driving",3)
            };

            return new DB(le, t);
        }

    }
}




