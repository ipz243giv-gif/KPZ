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
            int k = ReadInt("Введіть одиницю вимірювання дальності польоту (1 - km, 2 - metr, 3 - mile): ", 1, 3);
            int n = ReadInt("Введіть кількість літаків: ", 1, 100);

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

                var (distKM, distM, distMiles) = GetDistanceValues(k);

                airplanes[i] = new Airplane(startCity, finishCity, startDate, finishDate, dist);
            }
            Console.ForegroundColor = ConsoleColor.White;
            return airplanes;
        }
        private static (double km, double m, double miles) GetDistanceValues(int unitType)
        {
            double distance = ReadInt("Дальність польоту: ", 0, 40000);
            double km = 0, m = 0, miles = 0;

            switch (unitType)
            {
                case 1:
                    km = distance;
                    m = distance * 1000;
                    miles = distance / 1.60934;
                    break;
                case 2:
                    km = distance / 1000.0;
                    m = distance;
                    miles = distance / 1609.34;
                    break;
                case 3:
                    km = distance * 1.60934;
                    m = distance * 1609.34;
                    miles = distance;
                    break;
            }
            return (km, m, miles);
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

                choice = ReadInt("Виберіть опцію: ", 0, 5);
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
