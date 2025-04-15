using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SimpleClassConlsole
{
    public class Date
    {
        protected int Year {  get; set; }
        protected int Month { get; set; }
        protected int Day { get; set; }
        protected int Hours { get; set; }
        protected int Minutes { get; set; }
        public Date() { }

        public Date(int year, int month, int day, int hours, int minutes)
        {
            Year = year;
            Month = month;
            Day = day;
            Hours = hours;
            Minutes = minutes;
        }
        public Date(Date copy)
        {
            Year = copy.Year;
            Month = copy.Month;
            Day = copy.Day;
            Hours = copy.Hours;
            Minutes = copy.Minutes;
        }
        public int getYear() { return Year; }
        public int getMonth() { return Month; }
        public int getDay() { return Day; }
        public int getHours() { return Hours; }
        public int getMinutes() { return Minutes; }
        
    }
    public class Airplane
    {
        protected string StartCity {  get; set; }
        protected string FinishCity { get; set; }
        protected Date StartDate { get; set; }
        protected Date FinishDate { get; set; }
        public Airplane() { }
        public Airplane(string startCity, string finishCity, Date startDate, Date finishDate)
        {
            StartCity = startCity;
            FinishCity = finishCity;
            StartDate = startDate;
            FinishDate = finishDate;
        }
        public Airplane(Airplane copy)
        {
            StartCity = copy.StartCity;
            FinishCity = copy.FinishCity;
            StartDate = new Date(copy.StartDate);
            FinishDate = new Date(copy.FinishDate);
        }
        public int GetTotalTime()
        {
            int startTotalMinutes = StartDate.getDay() * 24 * 60 + StartDate.getHours() * 60 + StartDate.getMinutes();
            int finishTotalMinutes = FinishDate.getDay() * 24 * 60 + FinishDate.getHours() * 60 + FinishDate.getMinutes();

            return finishTotalMinutes - startTotalMinutes;
        }
        public bool IsArrivingToday()
        {
            return StartDate.getYear() == FinishDate.getYear() && StartDate.getMonth() == FinishDate.getMonth() && StartDate.getDay() == FinishDate.getDay();
        }
        public string getStartCity() { return StartCity; }
        public string getFinishCity() { return FinishCity; }
        public Date getStartDate() {  return StartDate; }
        public Date getFinishDate() { return FinishDate; }
    }
    internal class Program
    {
        public static Airplane[] ReadAirplaneArray()
        {
            Console.WriteLine("Введіть кількість літаків: ");
            int n = int.Parse(Console.ReadLine());
            Airplane[] airplanes = new Airplane[n];
            for (int i = 0; i < n; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Введіть дані рейсу {i + 1}: ");

                Console.Write("Місто відправлення: "); 
                string startCity = Console.ReadLine();

                Console.Write("Місто прибуття: "); 
                string finishCity = Console.ReadLine();

                Console.Write("Рік відправлення: ");
                int year;
                while (!int.TryParse(Console.ReadLine(), out year) || year <= 0)
                {
                    Console.Write("Невірний ввід. Введіть додатне число для ріку: ");
                }

                Console.Write("Місяць відправлення (1-12): ");
                int month;
                while (!int.TryParse(Console.ReadLine(), out month) || month < 1 || month > 12) 
                { 
                    Console.Write("Невірний ввід. Введіть числовід 1 до 12: "); 
                }
                Console.Write("День відправлення: ");
                int day;
                while (!int.TryParse(Console.ReadLine(), out day) || day <= 0 || day > 31)
                {
                    Console.Write("Невірний ввід.: ");
                }
                Console.Write("Година відправлення (0-23): ");
                int hour;
                while (!int.TryParse(Console.ReadLine(), out hour) || hour < 0 || hour > 23)
                {
                    Console.Write("Невірний ввід. Введіть числовід 0 до 23: ");
                }
                Console.Write("Хвилина відправлення (0-59): ");
                int minute;
                while (!int.TryParse(Console.ReadLine(), out minute) || minute < 0 || minute > 59)
                {
                    Console.Write("Невірний ввід. Введіть числовід 0 до 59: ");
                }

                Date startDate = new Date(year, month, day, hour, minute);

                Console.Write("Рік прибуття: ");
                int arriveyear;
                while (!int.TryParse(Console.ReadLine(), out arriveyear) || arriveyear <= 0)
                {
                    Console.Write("Невірний ввід. Введіть додатне число для року: ");
                }

                Console.Write("Місяць прибуття (1-12): ");
                int arrivemonth;
                while (!int.TryParse(Console.ReadLine(), out arrivemonth) || arrivemonth < 1 || arrivemonth > 12)
                {
                    Console.Write("Невірний ввід. Введіть числовід 1 до 12: ");
                }
                Console.Write("День прибуття: ");
                int arriveday;
                while (!int.TryParse(Console.ReadLine(), out arriveday) || arriveday <= 0 || arriveday > 31)
                {
                    Console.Write("Невірний ввід.: ");
                }
                Console.Write("Година прибуття (0-23): ");
                int arrivehour;
                while (!int.TryParse(Console.ReadLine(), out arrivehour) || arrivehour < 0 || arrivehour > 23)
                {
                    Console.Write("Невірний ввід. Введіть числовід 0 до 23: ");
                }
                Console.Write("Хвилина прибуття (0-59): ");
                int arriveminute;
                while (!int.TryParse(Console.ReadLine(), out arriveminute) || arriveminute < 0 || arriveminute > 59)
                {
                    Console.Write("Невірний ввід. Введіть числовід 0 до 59: ");
                }
                Date finishDate = new Date(arriveyear, arrivemonth, arriveday, arrivehour, arriveminute);
                airplanes[i] = new Airplane(startCity, finishCity, startDate, finishDate);
            }
            Console.ForegroundColor = ConsoleColor.White;
            return airplanes;
        }
        public static void PrintAirplane(Airplane airplane)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Місто відправлення: {airplane.getStartCity()}");
            Console.WriteLine($"Місто прибуття: {airplane.getFinishCity()}");
            Console.WriteLine($"Дата відправлення: {airplane.getStartDate().getYear()}-{airplane.getStartDate().getMonth():D2}-{airplane.getStartDate().getDay():D2}-{airplane.getStartDate().getHours():D2}:{airplane.getStartDate().getMinutes():D2}");
            Console.WriteLine($"Дата прибуття: {airplane.getFinishDate().getYear()}-{airplane.getFinishDate().getMonth():D2}-{airplane.getFinishDate().getDay():D2}-{airplane.getFinishDate().getHours():D2}:{airplane.getFinishDate().getMinutes():D2}");
        }
        public static void PrintAirplanes(Airplane[] airplane)
        {
            for (int i = 0; i < airplane.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n------------------------");
                PrintAirplane(airplane[i]);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("------------------------");
            }

        }
        static void GetAirplaneInfo(Airplane[] airplanes, out int maxTime, out int minTime)
        {
            maxTime = int.MinValue;
            minTime = int.MaxValue;
            for (int i = 0; i < airplanes.Length; i++)
            {
                int totaltime = airplanes[i].GetTotalTime();
                if (totaltime > maxTime)
                {
                    maxTime = totaltime;
                }
                if (totaltime < minTime)
                {
                    minTime = totaltime;
                }
            }
        }
        public static void SortAirplanesByDate(Airplane[] airplanes)
        {
            Array.Sort(airplanes, (s1, s2) => DateTime.Compare(new DateTime(s1.getStartDate().getYear(), s1.getStartDate().getMonth(), s1.getStartDate().getDay()), 
                new DateTime(s2.getStartDate().getYear(), s2.getStartDate().getMonth(), s2.getStartDate().getDay())));
        }
        public static void SortAirplanesByTotalTime(Airplane[] airplanes)
        {
            Array.Sort(airplanes, (x, y) => x.GetTotalTime().CompareTo(y.GetTotalTime()));
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            Airplane[] airplanes = ReadAirplaneArray();
            int choice;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nМеню: ");
                Console.WriteLine("1) Інформація рейс");
                Console.WriteLine("2) Інформація про всі рейси");
                Console.WriteLine("3) Вивести найбільший та найменший час подорожі");
                Console.WriteLine("4) Сортування рейсів за датою відправлення");
                Console.WriteLine("5) Сортування рейсів за часом подорожі");
                Console.WriteLine("0) Вихід");
                Console.Write("Виберіть опцію: ");
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 5)
                {
                    Console.Write("Невірний ввід. Введіть число від 0 до 5: ");
                }
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        Console.Write("Введіть номер рейсу: ");
                        int airplaneIndex;
                        while (!int.TryParse(Console.ReadLine(), out airplaneIndex) || airplaneIndex < 1 || airplaneIndex > airplanes.Length)
                        {
                            Console.Write($"Невірний ввід. Введіть номер рейсу від 1 до {airplanes.Length}: ");
                        }
                        Console.WriteLine($"\nІнформація про рейс {airplaneIndex}:");
                        PrintAirplane(airplanes[airplaneIndex - 1]);
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("\nІнформація про всі рейси:");
                        PrintAirplanes(airplanes);
                        break;
                    case 3:
                        Console.Clear();
                        int maxtime, mintime;
                        GetAirplaneInfo(airplanes, out maxtime, out mintime);
                        Console.WriteLine($"Найбільший час подорожі {maxtime} хвилин");
                        Console.WriteLine($"Найменший час подорожі {mintime} хвилин");
                        break;
                    case 4:
                        Console.Clear();
                        SortAirplanesByDate(airplanes);
                        Console.WriteLine("Сортування за датою:");
                        PrintAirplanes(airplanes);
                        break;
                    case 5:
                        Console.Clear();
                        SortAirplanesByTotalTime(airplanes);
                        Console.WriteLine("Сортування за часом:");
                        PrintAirplanes(airplanes);
                        break;
                    default:
                        break;
                }
                if (choice != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nEnter для продовження: ");
                    Console.ReadKey();
                }

            } while (choice != 0);
        }
    }
}
