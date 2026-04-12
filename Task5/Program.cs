using System;
using System.Collections.Generic;
using System.Text;

namespace Task5
{
    public interface ICommand
    {
        void Execute();
    }

    public class AddClassCommand : ICommand
    {
        private readonly LightElementNode _node;
        private readonly string _className;

        public AddClassCommand(LightElementNode node, string className)
        {
            _node = node;
            _className = className;
        }

        public void Execute() => _node.AddClass(_className);
    }

    public class AddChildCommand : ICommand
    {
        private readonly LightElementNode _parent;
        private readonly LightNode _child;

        public AddChildCommand(LightElementNode parent, LightNode child)
        {
            _parent = parent;
            _child = child;
        }

        public void Execute() => _parent.AddChild(_child);
    }

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

        protected virtual void OnCreated() => Console.WriteLine($"[Hook] <{_tagName}> створено.");
        protected virtual void OnInserted() => Console.WriteLine($"[Hook] <{_tagName}> вставлено в DOM.");
        protected virtual void OnStylesApplied(string className) => Console.WriteLine($"[Hook] Додано клас: {className}");

        public IEnumerable<LightNode> GetDFS()
        {
            yield return this;
            foreach (var child in _children)
            {
                if (child is LightElementNode element)
                    foreach (var node in element.GetDFS()) yield return node;
                else
                    yield return child;
            }
        }

        public void AddClass(string className) 
        {
            _cssClasses.Add(className);
            OnStylesApplied(className);
        }

        public void AddChild(LightNode node) 
        {
            _children.Add(node);
            if (node is LightElementNode element) element.OnInserted();
        }

        public override string InnerHtml()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var child in _children) sb.Append(child.OuterHtml());
            return sb.ToString();
        }

        public override string OuterHtml()
        {
            StringBuilder sb = new StringBuilder();
            string classes = _cssClasses.Count > 0 ? $" class=\"{string.Join(" ", _cssClasses)}\"" : "";
            sb.Append($"<{_tagName}{classes}");
            if (_closing == ClosingType.SelfClosing) sb.Append(" />");
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

            var main = new LightElementNode("main", DisplayType.Block, ClosingType.Normal);
            var h2 = new LightElementNode("h2", DisplayType.Block, ClosingType.Normal);
            h2.AddChild(new LightTextNode("Демонстрація патерну Команда"));

            Console.WriteLine("\n--- Виконання команд ---");

            ICommand addTitle = new AddChildCommand(main, h2);
            addTitle.Execute();

            ICommand addClass = new AddClassCommand(main, "container");
            addClass.Execute();

            Console.WriteLine("\n--- Кінцевий HTML ---");
            Console.WriteLine(main.OuterHtml());
            
            Console.ReadKey();
        }
    }
}
