using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw_11
{
    interface IEmployeePrinter
    {
        void PrintAllEmployees(Employee[] employees);
        void PrintEmployeesByPosition(Employee[] employees, string position);
        void PrintManagersAboveAverageSalary(Employee[] employees);
        void PrintEmployeesHiredAfterDate(Employee[] employees, DateTime hireDate);
        void PrintEmployeesByGender(Employee[] employees, char gender);
    }

    struct Employee
    {
        public string FirstName;
        public string LastName;
        public DateTime HireDate;
        public string Position;
        public char Gender;
        public decimal Salary;

        public override string ToString()
        {
            return $"Имя: {FirstName} {LastName}\n" +
                   $"Дата приема на работу: {HireDate}\n" +
                   $"Должность: {Position}\n" +
                   $"Пол: {Gender}\n" +
                   $"Заработная плата: {Salary:C}\n";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите количество сотрудников: ");
            int numEmployees = int.Parse(Console.ReadLine());

            Employee[] employees = new Employee[numEmployees];

            for (int i = 0; i < numEmployees; i++)
            {
                employees[i] = ReadEmployeeInfo();
            }

            IEmployeePrinter employeePrinter = new EmployeePrinter();

            employeePrinter.PrintAllEmployees(employees);

            Console.Write("Введите должность для фильтрации сотрудников: ");
            string positionToFilter = Console.ReadLine();
            employeePrinter.PrintEmployeesByPosition(employees, positionToFilter);

            employeePrinter.PrintManagersAboveAverageSalary(employees);

            Console.Write("Введите дату приема на работу (ГГГГ-ММ-ДД): ");
            DateTime hireDate = DateTime.Parse(Console.ReadLine());
            employeePrinter.PrintEmployeesHiredAfterDate(employees, hireDate);

            Console.Write("Введите пол (М/Ж) или нажмите Enter, чтобы показать всех: ");
            char genderFilter = char.TryParse(Console.ReadLine(), out char gender) ? gender : '\0';
            employeePrinter.PrintEmployeesByGender(employees, gender);

            Console.ReadLine();
        }

        static Employee ReadEmployeeInfo()
        {
            Employee employee = new Employee();
            Console.Write("Введите имя: ");
            employee.FirstName = Console.ReadLine();
            Console.Write("Введите фамилию: ");
            employee.LastName = Console.ReadLine();
            Console.Write("Введите дату приема на работу (ГГГГ-ММ-ДД): ");
            employee.HireDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Введите должность: ");
            employee.Position = Console.ReadLine();
            Console.Write("Введите пол (М/Ж): ");
            employee.Gender = char.Parse(Console.ReadLine());
            Console.Write("Введите заработную плату: ");
            employee.Salary = decimal.Parse(Console.ReadLine());
            return employee;
        }
    }


    class EmployeePrinter : IEmployeePrinter
    {
        public void PrintAllEmployees(Employee[] employees)
        {
            Console.WriteLine("Все сотрудники:");
            foreach (var employee in employees)
            {
                Console.WriteLine(employee);
            }
            Console.WriteLine();
        }

        public void PrintEmployeesByPosition(Employee[] employees, string position)
        {
            Console.WriteLine($"Сотрудники с должностью '{position}':");
            foreach (var employee in employees.Where(e => e.Position == position))
            {
                Console.WriteLine(employee);
            }
            Console.WriteLine();
        }

        public void PrintManagersAboveAverageSalary(Employee[] employees)
        {
            decimal averageClerkSalary = employees.Where(e => e.Position == "Clerk").Average(e => e.Salary);
            Console.WriteLine($"Менеджеры с зарплатой выше средней зарплаты клерков ({averageClerkSalary:C}):");
            foreach (var manager in employees.Where(e => e.Position == "Manager" && e.Salary > averageClerkSalary).OrderBy(e => e.LastName))
            {
                Console.WriteLine(manager);
            }
            Console.WriteLine();
        }

        public void PrintEmployeesHiredAfterDate(Employee[] employees, DateTime hireDate)
        {
            Console.WriteLine($"Сотрудники, принятые на работу после {hireDate.ToString("yyyy-MM-dd")}:");
            foreach (var employee in employees.Where(e => e.HireDate > hireDate).OrderBy(e => e.LastName))
            {
                Console.WriteLine(employee);
            }
            Console.WriteLine();
        }

        public void PrintEmployeesByGender(Employee[] employees, char gender)
        {
            string genderFilter = gender != '\0' ? $" ({gender})" : "";
            Console.WriteLine($"Сотрудники пола{genderFilter}:");
            foreach (var employee in employees.Where(e => gender == '\0' || e.Gender == gender))
            {
                Console.WriteLine(employee);
            }
            Console.WriteLine();
        }
    }
}

