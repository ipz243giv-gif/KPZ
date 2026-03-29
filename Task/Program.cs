using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public class Logger
    {
        public virtual void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[LOG]: {message}");
            Console.ResetColor();
        }

        public virtual void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR]: {message}");
            Console.ResetColor();
        }

        public virtual void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"[WARN]: {message}");
            Console.ResetColor();
        }
    }

    public class FileWriter
    {
        private string _filePath;

        public FileWriter(string filePath)
        {
            _filePath = filePath;
        }

        public void Write(string text)
        {
            File.AppendAllText(_filePath, text);
        }

        public void WriteLine(string text)
        {
            File.AppendAllLines(_filePath, new[] { text });
        }
    }

    public class FileLoggerAdapter : Logger
    {
        private readonly FileWriter _fileWriter;

        public FileLoggerAdapter(FileWriter fileWriter)
        {
            _fileWriter = fileWriter;
        }

        public override void Log(string message)
        {
            _fileWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [LOG]: {message}");
        }

        public override void Error(string message)
        {
            _fileWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [ERROR]: {message}");
        }

        public override void Warn(string message)
        {
            _fileWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [WARN]: {message}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("--- Робота стандартного Logger (Консоль) ---");
            Logger consoleLogger = new Logger();
            consoleLogger.Log("Система запущена.");
            consoleLogger.Warn("Мало вільної пам'яті.");
            consoleLogger.Error("Критична помилка доступу!");

            Console.WriteLine("\n--- Робота FileLoggerAdapter (Запис у файл) ---");

            string path = "log.txt";
            FileWriter fileWriter = new FileWriter(path);
            Logger fileLogger = new FileLoggerAdapter(fileWriter);

            fileLogger.Log("Запис логу у файл через адаптер.");
            fileLogger.Warn("Попередження записано у файл.");
            fileLogger.Error("Помилка записана у файл.");

            Console.WriteLine($"Всі повідомлення успішно записані у файл: {Path.GetFullPath(path)}");

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}
