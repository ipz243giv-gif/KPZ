using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    public enum DisplayType { Block, Inline }
    public enum ClosingType { Normal, SelfClosing }

    public abstract class LightNode
    {
        public abstract string OuterHtml();
    }

    public class LightTextNode : LightNode
    {
        private readonly string _text;
        public LightTextNode(string text) => _text = text;
        public override string OuterHtml() => _text;
        public long GetSize() => _text.Length * sizeof(char);
    }
    public class HtmlTagType
    {
        public string TagName { get; }
        public DisplayType Display { get; }
        public ClosingType Closing { get; }

        public HtmlTagType(string tagName, DisplayType display, ClosingType closing)
        {
            TagName = tagName;
            Display = display;
            Closing = closing;
        }
    }
    public class TagFactory
    {
        private readonly Dictionary<string, HtmlTagType> _tags = new Dictionary<string, HtmlTagType>();

        public HtmlTagType GetTagType(string name, DisplayType display, ClosingType closing)
        {
            if (!_tags.ContainsKey(name))
            {
                _tags[name] = new HtmlTagType(name, display, closing);
            }
            return _tags[name];
        }
    }
    public class LightElementNode : LightNode
    {
        private readonly HtmlTagType _type;
        private readonly LightNode _content;

        public LightElementNode(HtmlTagType type, LightNode content)
        {
            _type = type;
            _content = content;
        }

        public override string OuterHtml()
        {
            string html = $"<{_type.TagName}>{_content.OuterHtml()}</{_type.TagName}>";
            return _type.Display == DisplayType.Block ? html + "\n" : html;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            TagFactory factory = new TagFactory();
            List<LightNode> bookTree = new List<LightNode>();

            string bookText = @"ACT V
Scene I. Mantua. A Street.
Scene II. Friar Lawrence's Cell.
Scene III. A churchyard; in it a Monument belonging to the Capulets

 Dramatis Personae

ESCALUS, Prince of Verona.
MERCUTIO, kinsman to the Prince, and friend to Romeo.
PARIS, a young Nobleman, kinsman to the Prince.
Page to Paris.";

            string[] lines = bookText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (string.IsNullOrEmpty(line)) continue;

                HtmlTagType tagType;

                if (i == 0)
                    tagType = factory.GetTagType("h1", DisplayType.Block, ClosingType.Normal);
                else if (line.Length < 20)
                    tagType = factory.GetTagType("h2", DisplayType.Block, ClosingType.Normal);
                else if (line.StartsWith(" "))
                    tagType = factory.GetTagType("blockquote", DisplayType.Block, ClosingType.Normal);
                else
                    tagType = factory.GetTagType("p", DisplayType.Block, ClosingType.Normal);

                bookTree.Add(new LightElementNode(tagType, new LightTextNode(line.Trim())));
            }

            Console.WriteLine("--- ГЕНЕРОВАНИЙ HTML ---\n");
            foreach (var node in bookTree)
            {
                Console.Write(node.OuterHtml());
            }

            long totalTextSize = lines.Sum(l => l.Length * sizeof(char));
            Console.WriteLine("\n-------------------------------------------");
            Console.WriteLine($"Кількість рядків: {bookTree.Count}");
            Console.WriteLine($"Приблизний об'єм тексту: {totalTextSize} байт");
            Console.WriteLine("Патерн 'Легковаговик' дозволив уникнути створення дублікатів метаданих для кожного тега.");

            Console.ReadKey();
        }
    }
}
