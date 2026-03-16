using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    public class Character
    {
        public string Type { get; set; }
        public int Height { get; set; }
        public string Build { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string Outfit { get; set; }
        public List<string> Inventory { get; set; } = new List<string>();
        public List<string> Deeds { get; set; } = new List<string>();

        public override string ToString()
        {
            return $"--- {Type} ---\n" +
                   $"Зріст: {Height} см\n" +
                   $"Статура: {Build}\n" +
                   $"Волосся: {HairColor}, Очі: {EyeColor}\n" +
                   $"Одяг: {Outfit}\n" +
                   $"Інвентар: {string.Join(", ", Inventory)}\n" +
                   $"Історія справ: {string.Join(", ", Deeds)}\n";
        }
    }
    public interface ICharacterBuilder
    {
        ICharacterBuilder SetHeight(int height);
        ICharacterBuilder SetBuild(string build);
        ICharacterBuilder SetAppearance(string hair, string eyes);
        ICharacterBuilder SetOutfit(string outfit);
        ICharacterBuilder AddToInventory(string item);
        ICharacterBuilder AddDeed(string deed);
        Character Build();
    }
    public class HeroBuilder : ICharacterBuilder
    {
        private Character _character = new Character { Type = "Герой" };

        public ICharacterBuilder SetHeight(int height) { _character.Height = height; return this; }
        public ICharacterBuilder SetBuild(string build) { _character.Build = build; return this; }
        public ICharacterBuilder SetAppearance(string hair, string eyes) { _character.HairColor = hair; _character.EyeColor = eyes; return this; }
        public ICharacterBuilder SetOutfit(string outfit) { _character.Outfit = outfit; return this; }
        public ICharacterBuilder AddToInventory(string item) { _character.Inventory.Add(item); return this; }
        public ICharacterBuilder AddDeed(string deed) { _character.Deeds.Add("Добро: " + deed); return this; }

        public Character Build() => _character;
    }
    public class EnemyBuilder : ICharacterBuilder
    {
        private Character _character = new Character { Type = "Ворог" };

        public ICharacterBuilder SetHeight(int height) { _character.Height = height; return this; }
        public ICharacterBuilder SetBuild(string build) { _character.Build = build; return this; }
        public ICharacterBuilder SetAppearance(string hair, string eyes) { _character.HairColor = hair; _character.EyeColor = eyes; return this; }
        public ICharacterBuilder SetOutfit(string outfit) { _character.Outfit = outfit; return this; }
        public ICharacterBuilder AddToInventory(string item) { _character.Inventory.Add(item); return this; }
        public ICharacterBuilder AddDeed(string deed) { _character.Deeds.Add("Зло: " + deed); return this; }

        public Character Build() => _character;
    }
    public class Director
    {
        public void ConstructEpicHero(ICharacterBuilder builder)
        {
            builder.SetHeight(185)
                   .SetBuild("Атлетична")
                   .SetAppearance("Золотисте", "Блакитні")
                   .SetOutfit("Сяючі обладунки")
                   .AddToInventory("Меч Світла")
                   .AddToInventory("Щит Істини")
                   .AddDeed("Врятував село від дракона")
                   .AddDeed("Звільнив полонених");
        }

        public void ConstructDarkLord(ICharacterBuilder builder)
        {
            builder.SetHeight(210)
                   .SetBuild("Масивна")
                   .SetAppearance("Лисий", "Палаючі червоні")
                   .SetOutfit("Чорна мантія")
                   .AddToInventory("Посох Хаосу")
                   .AddDeed("Захопив королівство")
                   .AddDeed("Викрав сонце");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Director director = new Director();

            var heroBuilder = new HeroBuilder();
            director.ConstructEpicHero(heroBuilder);
            Character hero = heroBuilder.Build();

            var enemyBuilder = new EnemyBuilder();
            director.ConstructDarkLord(enemyBuilder);
            Character enemy = enemyBuilder.Build();

            Console.WriteLine("Створення персонажів завершено:\n");
            Console.WriteLine(hero);
            Console.WriteLine(enemy);

            Console.ReadKey();
        }
    }
}