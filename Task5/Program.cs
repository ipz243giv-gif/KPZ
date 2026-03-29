using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    public abstract class LightNode
    {
        public abstract string InnerHtml();
        public abstract string OuterHtml();
    }
    public class LightTextNode : LightNode
    {
        private string _text;

        public LightTextNode(string text) => _text = text;

        public override string InnerHtml() => _text;
        public override string OuterHtml() => _text;
    }
    public enum DisplayType { Block, Inline }
    public enum ClosingType { Normal, SelfClosing }

    public class LightElementNode : LightNode
    {
        private string _tagName;
        private DisplayType _display;
        private ClosingType _closing;
        private List<string> _cssClasses = new List<string>();
        private List<LightNode> _children = new List<LightNode>();

        public LightElementNode(string tagName, DisplayType display, ClosingType closing)
        {
            _tagName = tagName;
            _display = display;
            _closing = closing;
        }

        public void AddClass(string className) => _cssClasses.Add(className);
        public void AddChild(LightNode node) => _children.Add(node);
        public int ChildrenCount => _children.Count;

        public override string InnerHtml()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var child in _children)
            {
                sb.Append(child.OuterHtml());
            }
            return sb.ToString();
        }

        public override string OuterHtml()
        {
            StringBuilder sb = new StringBuilder();
            string classes = _cssClasses.Count > 0 ? $" class=\"{string.Join(" ", _cssClasses)}\"" : "";

            sb.Append($"<{_tagName}{classes}");

            if (_closing == ClosingType.SelfClosing)
            {
                sb.Append(" />");
            }
            else
            {
                sb.Append(">");
                sb.Append(InnerHtml());
                sb.Append($"</{_tagName}>");
            }

            if (_display == DisplayType.Block) sb.Append("\n");

            return sb.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var container = new LightElementNode("div", DisplayType.Block, ClosingType.Normal);
            container.AddClass("container");

            var title = new LightElementNode("h1", DisplayType.Block, ClosingType.Normal);
            title.AddChild(new LightTextNode("Мій список завдань"));
            container.AddChild(title);

            var list = new LightElementNode("ul", DisplayType.Block, ClosingType.Normal);
            list.AddClass("todo-list");

            string[] tasks = { "Вивчити патерни", "Здати лабораторну", "Попити кави" };
            foreach (var task in tasks)
            {
                var li = new LightElementNode("li", DisplayType.Block, ClosingType.Normal);
                li.AddChild(new LightTextNode(task));
                list.AddChild(li);
            }

            container.AddChild(list);

            var img = new LightElementNode("img", DisplayType.Inline, ClosingType.SelfClosing);
            img.AddClass("avatar");
            container.AddChild(img);

            // Вивід результатів
            Console.WriteLine("--- Перевірка ChildrenCount ---");
            Console.WriteLine($"Кількість дочірніх елементів у списку: {list.ChildrenCount}");

            Console.WriteLine("\n--- Вивід innerHTML списку ---");
            Console.WriteLine(list.InnerHtml());

            Console.WriteLine("--- Вивід outerHTML всього контейнера ---");
            Console.WriteLine(container.OuterHtml());

            Console.ReadKey();
        }
    }
}
