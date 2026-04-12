using System;
using System.Collections.Generic;
using System.Text;

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

            OnCreated();
        }
        
        protected virtual void OnCreated() 
        {
            Console.WriteLine($"[Hook] Елемент <{_tagName}> успішно створено в пам'яті.");
        }

        protected virtual void OnInserted() 
        {
            Console.WriteLine($"[Hook] Елемент <{_tagName}> було вставлено в дерево документа.");
        }

        protected virtual void OnStylesApplied(string className) 
        {
            Console.WriteLine($"[Hook] До елемента <{_tagName}> додано новий CSS клас: {className}");
        }

        public void AddClass(string className) 
        {
            _cssClasses.Add(className);
            OnStylesApplied(className);
        }

        public void AddChild(LightNode node) 
        {
            _children.Add(node);
            if (node is LightElementNode element)
            {
                element.OnInserted();
            }
        }

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

            Console.WriteLine("--- Створення елементів (Демонстрація хуків) ---");

            var container = new LightElementNode("div", DisplayType.Block, ClosingType.Normal);
            container.AddClass("main-wrapper");

            var title = new LightElementNode("h1", DisplayType.Block, ClosingType.Normal);
            title.AddChild(new LightTextNode("Лабораторна робота №5"));

            container.AddChild(title);

            Console.WriteLine("\n--- Кінцевий HTML ---");
            Console.WriteLine(container.OuterHtml());

            Console.ReadKey();
        }
    }
}
