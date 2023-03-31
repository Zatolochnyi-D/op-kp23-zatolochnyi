using System;

namespace Assignment2
{
    class Task1
    {

        static string[] lastNames = new string[]
        {
            "Emp#0001",
            "Emp#0002",
            "Emp#0003",
            "Emp#0004",
        };

        static int[] salaries = new int[]
        {
            3000,
            5000,
            2000,
            3000,
        };

        static int[] withhelds = new int[]
        {
            1000,
            1500,
            500,
            1000,
        };

        static void Main()
        {
            Employee[] employees = new Employee[lastNames.Length];
            for (int i = 0; i < lastNames.Length; i++)
            {
                employees[i] = new(lastNames[i], salaries[i], withhelds[i]);
            }

            Console.WriteLine("Last Name\tSalary\tWithheld\tIssued");
            for (int i = 0; i < employees.Length; i++)
            {
                Console.WriteLine(employees[i].GetInfo());
            }
        }
    }

    public class Employee
    {
        private string _lastName;
        private int _salary;
        private int _withheld;
        private int _issued = 0;

        public string LastName { get => _lastName; }
        public int Salary { get => _salary; }
        public int Withheld { get => _withheld; }
        public int Issued { get => _issued; }

        public Employee(string lastName, int salary, int withheld)
        {
            _lastName = lastName;
            _salary = salary;
            _withheld = withheld;

            CalcIssued();
        }

        private void CalcIssued()
        {
            _issued = _salary - _withheld;
        }

        public string GetInfo()
        {
            return $"{_lastName}\t{_salary}\t{_withheld}\t\t{_issued}";
        }
    }
}

//input:
//Emp#0001 3000 1000
//Emp#0002 5000 1500
//Emp#0003 2000 500
//Emp#0004 3000 1000

//expected output:
//Last Name   Salary   Withheld   Issued
//Emp#0001    3000     1000       2000
//Emp#0002    5000     1500       3500
//Emp#0003    2000     500        1500
//Emp#0004    3000     1000       2000

//result is equal to expected output