using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    public interface IPrototype<T>
    {
        T Clone();
    }

    public class Virus : IPrototype<Virus>
    {
        public double Weight { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public List<Virus> Children { get; set; }

        public Virus(double weight, int age, string name, string species)
        {
            Weight = weight;
            Age = age;
            Name = name;
            Species = species;
            Children = new List<Virus>();
        }

        public Virus Clone()
        {
            Virus clone = (Virus)this.MemberwiseClone();

            clone.Children = new List<Virus>();

            foreach (var child in this.Children)
            {
                clone.Children.Add(child.Clone());
            }

            return clone;
        }

        public void PrintStructure(int indent = 0)
        {
            string space = new string(' ', indent * 4);
            Console.WriteLine($"{space}Вірус: {Name} (Вид: {Species}, Вік: {Age}, Вага: {Weight})");
            foreach (var child in Children)
            {
                child.PrintStructure(indent + 1);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Virus grandParent = new Virus(1.5, 10, "Дідусь Вірус", "COVID-Alpha");

            Virus parent1 = new Virus(1.1, 5, "Батько 1", "COVID-Beta");
            Virus parent2 = new Virus(1.2, 4, "Батько 2", "COVID-Gamma");
            grandParent.Children.Add(parent1);
            grandParent.Children.Add(parent2);

            parent1.Children.Add(new Virus(0.5, 1, "Дитина 1.1", "COVID-Delta"));
            parent1.Children.Add(new Virus(0.6, 1, "Дитина 1.2", "COVID-Epsilon"));
            parent2.Children.Add(new Virus(0.4, 1, "Дитина 2.1", "COVID-Zeta"));

            Console.WriteLine("--- Оригінальна структура ---");
            grandParent.PrintStructure();

            Console.WriteLine("\n--- Клонування всієї структури... ---");
            Virus grandParentClone = grandParent.Clone();

            grandParentClone.Name = "КЛОНОВАННИЙ Дідусь";
            grandParentClone.Children[0].Name = "КЛОНОВАННИЙ Батько 1";

            Console.WriteLine("\n--- Структура КЛОНУ (після змін) ---");
            grandParentClone.PrintStructure();

            Console.WriteLine("\n--- Оригінальна структура (має залишитись незмінною) ---");
            grandParent.PrintStructure();

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}