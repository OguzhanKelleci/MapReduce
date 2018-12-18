using System;
using System.Linq;
using System.Collections.Generic;

namespace Her
{
    class Program
    {
        static void Main(string[] args)
        {
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

            System.Console.WriteLine("-----------------------------");


            // #####################################

            var db = DB.testDb();

            // SELECT * FROM Employee

            var q = MapReduceExtension.Map(db.EmployeeTable, e => e);
            var q1 = db.EmployeeTable.Map(e => e);

            //SELECT id, name FROM Employee
            var q2 = MapReduceExtension.Map(db.EmployeeTable, e => e.Id);



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
        public DB(IEnumerable<Employee> e)
        {
            EmployeeTable = e;
        }

        public static DB testDb()
        {
            Employee[] le = new Employee[] {
                    new Employee(1,"Sander", "Sander@test.com", 1200.00),
                    new Employee(1,"Tim", "tim@test.com", 1900.12),
                    new Employee(1,"Jan", "JAN@test.com", 2000.40),
                };

            return new DB(le);
        }

    }
}




