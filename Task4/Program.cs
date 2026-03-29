using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Task4
{
    public interface ITextReader
    {
        char[][] ReadText(string filePath);
    }

    public class SmartTextReader : ITextReader
    {
        public char[][] ReadText(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            char[][] result = new char[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                result[i] = lines[i].ToCharArray();
            }

            return result;
        }
    }
    public class SmartTextChecker : ITextReader
    {
        private readonly ITextReader _realReader;

        public SmartTextChecker(ITextReader reader)
        {
            _realReader = reader;
        }

        public char[][] ReadText(string filePath)
        {
            Console.WriteLine($"[Checker] Спроба відкриття файлу: {filePath}");

            char[][] data = _realReader.ReadText(filePath);

            Console.WriteLine($"[Checker] Файл успішно прочитано та закрито.");

            int totalChars = data.Sum(line => line.Length);
            Console.WriteLine($"[Checker] Статистика: Рядків: {data.Length}, Символів: {totalChars}");

            return data;
        }
    }
    public class SmartTextReaderLocker : ITextReader
    {
        private readonly ITextReader _realReader;
        private readonly Regex _lockPattern;

        public SmartTextReaderLocker(ITextReader reader, string pattern)
        {
            _realReader = reader;
            _lockPattern = new Regex(pattern);
        }

        public char[][] ReadText(string filePath)
        {
            if (_lockPattern.IsMatch(filePath))
            {
                Console.WriteLine($"[Locker] Access denied! Файл '{filePath}' заблоковано за паттерном.");
                return null;
            }

            return _realReader.ReadText(filePath);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string publicFile = "public.txt";
            string secretFile = "secret_data.txt";
            File.WriteAllText(publicFile, "Hello\nWorld!");
            File.WriteAllText(secretFile, "Top Secret Content");

            ITextReader reader = new SmartTextReader();

            ITextReader checkerProxy = new SmartTextChecker(reader);

            ITextReader fullProxyChain = new SmartTextReaderLocker(checkerProxy, ".*secret.*");

            Console.WriteLine("--- Тест 1: Публічний файл ---");
            fullProxyChain.ReadText(publicFile);

            Console.WriteLine("\n--- Тест 2: Заблокований файл ---");
            fullProxyChain.ReadText(secretFile);

            File.Delete(publicFile);
            File.Delete(secretFile);

            Console.ReadKey();
        }
    }
}
