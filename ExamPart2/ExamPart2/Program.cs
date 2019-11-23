using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPart2
{
    class TimeStamp
    {
        private int hour;
        private int minute;
        private int seconds;
        private const int MAX_HOUR = 24;
        private const int MAX_MINUTE_SECONDS = 60;
        private const int MIN_HOUR_MINUTE_SECONDS = 0;


        public TimeStamp()
        {
            Hour = 0;
            Minute = 0;
            Seconds = 0;
        }
        public TimeStamp(int _hour, int _minute, int _seconds)
        {
            Hour = _hour;
            Minute = _minute;
            Seconds = _seconds;

        }
        public int Hour
        {
            get
            {
                return hour;
            }
            set
            {
                if (value < MIN_HOUR_MINUTE_SECONDS)
                    throw new ArgumentException("Hour must be greater than 0", "Hour");
                hour = value;
            }
        }

        public int Minute
        {
            get
            {
                return minute;
            }
            set
            {
                if (value < MIN_HOUR_MINUTE_SECONDS || value > MAX_MINUTE_SECONDS)
                    throw new ArgumentException("Minutes must be from 0 to 60", "Minute");
                minute = value;
            }
        }

        public int Seconds
        {
            get
            {
                return seconds;
            }
            set
            {
                if (value < MIN_HOUR_MINUTE_SECONDS || value > MAX_MINUTE_SECONDS)
                    throw new ArgumentException("Seconds must be from 0 to 60", "Seconds");
                seconds = value;
            }
        }
        public TimeStamp ConvertFromSeconds(int SecondsToConvert)
        {
            Hour = SecondsToConvert / 3600;
            Minute = (SecondsToConvert % 3600) / 60;
            Seconds = SecondsToConvert % 60;
            return this;
        }
        public int ConvertToSeconds()
        {
            return ((Hour * 3600) + (Minute * 60) + Seconds);

        }
        public double ConvertToHours()
        {
            double HoursFromMinutes = Minute /60.00;
            double hours = Hour + HoursFromMinutes;
            return hours;
        }
        public void AddSeconds(int TheSeconds)
        {
            int totalSeconds = ConvertToSeconds() + TheSeconds;
            ConvertFromSeconds(totalSeconds);
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}", this.Hour.ToString("d2"), this.Minute.ToString("d2"), this.Seconds.ToString("d2"));

        }

        private int GetIntegerInput(string customMessage)
        {
            int input;
            Console.Write("{0}:\t", customMessage);

            while ((int.TryParse(Console.ReadLine(), out input) == false) || input < MIN_HOUR_MINUTE_SECONDS || input > MAX_MINUTE_SECONDS)
            {
                Console.Write("Input is not numeric or out of range, input again:");
            }
            return input;
        }

        public void ReadFromConsole()
        {

            Hour = GetIntegerInput("Please enter Time Stamp for hours:");
            Minute = GetIntegerInput("Please enter Time Stamp for minutes:");
            Seconds = GetIntegerInput("Please enter Time Stamp for seconds:");

        }
        public static TimeStamp AddTwoTimeStamps(TimeStamp TimeStampOne, TimeStamp TimeStampTwo)
        {
            int totalSeconds1 = TimeStampOne.ConvertToSeconds();
            int totalSeconds2 = TimeStampTwo.ConvertToSeconds();
            int grandTotaSeconds = totalSeconds1 + totalSeconds2;
            TimeStamp TimeStamp3 = new TimeStamp();
            return TimeStamp3.ConvertFromSeconds(grandTotaSeconds);

        }

    }
    class Employee 
    {
        private int id;
        private string firstName;
        private string lastName;
        private double hourlyWage;
        private TimeStamp timeStamp;

        public Employee() { }
        /* public Employee(int _id, string _firstName, string _lastName, double _hourlyWage)
         {
             Id = _id;
             FirstName = _firstName;
             LastName = _lastName;
             HourlyWage = _hourlyWage;



         }*/
        public Employee(int _id, string _firstName, string _lastName, double _hourlyWage, TimeStamp _timeStamp)
        {
            Id = _id;
            FirstName = _firstName;
            LastName = _lastName;
            HourlyWage = _hourlyWage;
            TimeStamp = _timeStamp;


        }
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("ID must be greater than 0", "Id");
                id = value;
            }
        }
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("First Name name cannot be empty", "FirstName");
                firstName = value;
            }
        }
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Last Name name cannot be empty", "LastName");
                lastName = value;
            }
        }
        public double HourlyWage
        {
            get
            {
                return hourlyWage;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Hourly Wage must be greater than 0", "HourlyWage");
                hourlyWage = value;
            }
        }
        public TimeStamp TimeStamp
        {
            get
            {
                return timeStamp;
            }
            set
            {
                timeStamp = value;
            }
        }
        public override string ToString()
        {
            return id + "|" + firstName + "|" + lastName + "|" + hourlyWage + "|" + timeStamp.ToString();
        }

    }
    class EmployeeComparer : IComparer<Employee>
    {
        public int Compare(Employee emp1, Employee emp2)

        {
     
            return emp1.LastName.CompareTo(emp2.LastName);
        }

        
    }
    class Program
    {
        static void Main(string[] args)
        {
            
            Employee[] employee = new Employee[9];
            generateEmployeeListFromFile(employee, "emp.txt");
            processTimeWorkedFile(employee, "hours.txt");
            SortEmployeeListByLastName(employee);
            printReport(employee, "report.txt");
           
        }
        public static void generateEmployeeListFromFile(Employee[] employeeArray, string fileName)
        {
            if (File.Exists(fileName))
            {

                StreamReader streamReader = null;
                string currentLine;
                int index = 0;
                try
                {
                    using (streamReader = new StreamReader(fileName))
                    {
                        while ((currentLine = streamReader.ReadLine()) != null)
                        {

                            string[] employeeData = currentLine.Split('|');
                            Employee employee = new Employee();
                            employee.Id = int.Parse(employeeData[0]);
                            employee.LastName = employeeData[1];
                            employee.FirstName = employeeData[2];
                            employee.HourlyWage = double.Parse(employeeData[3]);
                            employee.TimeStamp = new TimeStamp();
                            employeeArray[index++] = employee;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("File Error: " + e.StackTrace);

                }
                if (employeeArray != null)
                    foreach (Employee employee in employeeArray)
                    {
                        Console.WriteLine(employee);
                    }

            }
            else
                Console.WriteLine("Array is corrupted.");
            Console.ReadKey();
        }
        public static void addTimeWorkedToEmployee(Employee[] employeeArray, int employeeNumber, TimeStamp timeWorked)
        {
            for (int i = 0; i < employeeArray.Length; i++)
            {
                if (employeeArray[i] != null && employeeArray[i].Id == employeeNumber)
                {
                    employeeArray[i].TimeStamp = TimeStamp.AddTwoTimeStamps(employeeArray[i].TimeStamp, timeWorked);
                    break;
                }

            }

        }
        public static void processTimeWorkedFile(Employee[] employeeArray, string fileName)
        {
            if (File.Exists(fileName))
            {

                StreamReader streamReader = null;
                string currentLine;

                try
                {
                    using (streamReader = new StreamReader(fileName))
                    {
                        while ((currentLine = streamReader.ReadLine()) != null)
                        {
                            string[] employeeTimeWorkedData = currentLine.Split('|');

                            string[] time = employeeTimeWorkedData[1].Split(':');
                            TimeStamp timeStamp = new TimeStamp(int.Parse(time[0]), int.Parse(time[1]), int.Parse(time[2]));

                            addTimeWorkedToEmployee(employeeArray, int.Parse(employeeTimeWorkedData[0]), timeStamp);

                        }
                        
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("File Error: " + e.StackTrace);

                }
                if (employeeArray != null)
                    foreach (Employee employee in employeeArray)
                    {
                        Console.WriteLine(employee);
                    }

            }
            else
                Console.WriteLine("Employee is corrupted.");
            Console.ReadKey();
        }
        public static void printReport(Employee[] employeeArray, string fileName)
        {
            int totalTime = 0;
            double totalPay = 0;
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(fileName, true))
                {
                    streamWriter.WriteLine(string.Format("{0,5}:\t {1,-9}:\t {2,10}:\t {3,11}:\t {4,11}:\t {5,3}:\t", "Emp #", "Last Name",
                        "First Name", "Time Worked", " Hourly Wage", "Pay"));
                    streamWriter.WriteLine("{0,5}\t {1,9}\t {2,10}\t {3,11}\t {4,11}\t {5,3}\t", "-----", "-----------",
                        "-----------", "-----------", " -----------", "----");
                   
                    for (int i = 0; i < employeeArray.Length; i++)
                    {
                        if (employeeArray[i] != null)
                        {
                            streamWriter.WriteLine("{0,5}\t {1,9}\t {2,10}\t {3,11}\t {4,11}\t {5,3}\t",
                                employeeArray[i].Id, employeeArray[i].LastName, employeeArray[i].FirstName,
                           employeeArray[i].TimeStamp, employeeArray[i].HourlyWage.ToString("n2"),
                           (employeeArray[i].HourlyWage * employeeArray[i].TimeStamp.ConvertToHours()).ToString("C"));

                            totalTime += employeeArray[i].TimeStamp.ConvertToSeconds();
                            totalPay += employeeArray[i].HourlyWage * employeeArray[i].TimeStamp.ConvertToHours();
                        }
                    }
                    TimeStamp time = new TimeStamp();
                    streamWriter.WriteLine("Total Time Worked = {0}", time.ConvertFromSeconds(totalTime));
                    streamWriter.WriteLine("Total Pay = {0}", totalPay.ToString("C"));
                }
                
                Console.WriteLine("Report is done");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            Console.ReadKey();
        }
        public static void SortEmployeeListByLastName(Employee[] employeeArray)
        {
            
               
            Array.Sort(employeeArray, new EmployeeComparer());
            
        }
    }
}





