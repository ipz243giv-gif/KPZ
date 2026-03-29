using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public abstract class Hero
    {
        public string Name { get; protected set; }
        public abstract int GetPower();
        public abstract string GetDescription();
    }
    public class Warrior : Hero
    {
        public Warrior() => Name = "Воїн";
        public override int GetPower() => 10;
        public override string GetDescription() => Name;
    }

    public class Mage : Hero
    {
        public Mage() => Name = "Маг";
        public override int GetPower() => 8;
        public override string GetDescription() => Name;
    }

    public class Palladin : Hero
    {
        public Palladin() => Name = "Палладин";
        public override int GetPower() => 12;
        public override string GetDescription() => Name;
    }
    public abstract class InventoryDecorator : Hero
    {
        protected Hero _hero;

        public InventoryDecorator(Hero hero)
        {
            _hero = hero;
        }

        public override string GetDescription() => _hero.GetDescription();
        public override int GetPower() => _hero.GetPower();
    }
    public class Sword : InventoryDecorator
    {
        public Sword(Hero hero) : base(hero) { }

        public override string GetDescription() => _hero.GetDescription() + " + Меч";
        public override int GetPower() => _hero.GetPower() + 15;
    }

    public class MagicArmor : InventoryDecorator
    {
        public MagicArmor(Hero hero) : base(hero) { }

        public override string GetDescription() => _hero.GetDescription() + " + Магічний Обладунок";
        public override int GetPower() => _hero.GetPower() + 10;
    }

    public class AncientArtifact : InventoryDecorator
    {
        public AncientArtifact(Hero hero) : base(hero) { }

        public override string GetDescription() => _hero.GetDescription() + " + Стародавній Артефакт";
        public override int GetPower() => _hero.GetPower() * 2;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Hero myHero = new Warrior();
            Console.WriteLine($"Створено: {myHero.GetDescription()}, Сила: {myHero.GetPower()}");

            myHero = new Sword(myHero);
            Console.WriteLine($"Оновлено: {myHero.GetDescription()}, Сила: {myHero.GetPower()}");

            myHero = new MagicArmor(myHero);
            Console.WriteLine($"Оновлено: {myHero.GetDescription()}, Сила: {myHero.GetPower()}");

            myHero = new AncientArtifact(myHero);
            Console.WriteLine($"Кінцевий герой: {myHero.GetDescription()}, Загальна сила: {myHero.GetPower()}");

            Console.WriteLine("\n--- Інший приклад (Маг з двома артефактами) ---");

            Hero myMage = new Mage();
            myMage = new AncientArtifact(new AncientArtifact(myMage));

            Console.WriteLine($"Герой: {myMage.GetDescription()}");
            Console.WriteLine($"Сила мага: {myMage.GetPower()}");

            Console.ReadKey();
        }
    }
}
