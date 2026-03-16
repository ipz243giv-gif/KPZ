using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task3
{
    public sealed class Authenticator
    {
        private static readonly Lazy<Authenticator> _instance =
            new Lazy<Authenticator>(() => new Authenticator());

        private Authenticator()
        {
            Console.WriteLine("[System]: Authenticator ініціалізовано.");
        }

        public static Authenticator Instance => _instance.Value;

        public void Authenticate(string user)
        {
            Console.WriteLine($"[Auth]: Користувач '{user}' пройшов перевірку.");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("--- Тест Singleton у багатопотоковому середовищі ---");

            Task t1 = Task.Run(() => AccessAuthenticator("Thread-A"));
            Task t2 = Task.Run(() => AccessAuthenticator("Thread-B"));

            Task.WaitAll(t1, t2);

            Console.WriteLine("\n--- Перевірка на ідентичність об'єктів ---");

            Authenticator s1 = Authenticator.Instance;
            Authenticator s2 = Authenticator.Instance;

            if (object.ReferenceEquals(s1, s2))
            {
                Console.WriteLine("Результат: Обидва об'єкти є одним і тим самим екземпляром. Singleton працює!");
            }
            else
            {
                Console.WriteLine("Помилка: Створено різні екземпляри!");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }

        static void AccessAuthenticator(string threadName)
        {
            Authenticator auth = Authenticator.Instance;
            auth.Authenticate(threadName);
        }
    }
}