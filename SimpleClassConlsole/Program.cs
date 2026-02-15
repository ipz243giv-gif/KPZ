using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SimpleClassLibrary;

namespace SimpleClassConlsole
{
    internal class Program
    {
        public static Airplane[] ReadAirplaneArray()
        {
            int k; 
            do 
            { 
                Console.WriteLine("Введіть одиницю вимірювання дальності польоту 1 - km, 2 - metr, 3 - mile"); 
            } while (!int.TryParse(Console.ReadLine(), out k) || k < 1 || k > 3);
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

                Date startDate = ReadDate("відправлення");

                Date finishDate = ReadDate("прибуття");
                
                int distance;
                Console.Write("Дальність польоту: ");
                while (!int.TryParse(Console.ReadLine(), out distance) || distance < 0)
                {
                    Console.Write("Невірний ввід.: ");
                }
                double distanceKM = 0, distanceM = 0, distanceMiles = 0;
                if (k == 1)
                {
                    distanceKM = distance;
                    distanceM = distance * 1000;
                    distanceMiles = distance / 1.609;
                }
                else if (k == 2)
                {
                    distanceKM = distance / 1000;
                    distanceM = distance;
                    distanceMiles = distance / 1609.34;
                }
                else if (k == 3)
                {
                    distanceKM = distance / 1.609;
                    distanceM = distance * 1609.34;
                    distanceMiles = distance;
                }
                airplanes[i] = new Airplane(startCity, finishCity, startDate, finishDate, distanceKM, distanceM, distanceMiles);
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
            Console.WriteLine($"Дальність польоту: {airplane.getFlightDistanceKM():F01} км / {airplane.getFlightDistanceM():F01} м / {airplane.getFlightDistanceMiles():F01} миль");
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
        
        private static int ReadInt(string prompt, int min, int max)
        {
            int value;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out value) || value < min || value > max)
            {
                Console.Write($"Помилка! Введіть ціле число від {min} до {max}: ");
            }
            return value;
        }

        private static Date ReadDate(string label)
        {
            Console.WriteLine($"\n--- Введення дати {label} ---");
            int year = ReadInt("Рік: ", 1, 3000);
            int month = ReadInt("Місяць (1-12): ", 1, 12);
            int day = ReadInt("День (1-31): ", 1, 31);
            int hour = ReadInt("Година (0-23): ", 0, 23);
            int minute = ReadInt("Хвилина (0-59): ", 0, 59);

            return new Date(year, month, day, hour, minute);
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
