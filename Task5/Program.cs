using System;
using System.Collections.Generic;
using System.Text;

namespace Task5
{
    public interface IVisitor
    {
        void VisitText(LightTextNode text);
        void VisitElement(LightElementNode element);
    }

    public class StatisticsVisitor : IVisitor
    {
        public int ElementsCount { get; private set; } = 0;
        public int TextNodesCount { get; private set; } = 0;

        public void VisitText(LightTextNode text) => TextNodesCount++;
        public void VisitElement(LightElementNode element) => ElementsCount++;
    }

    public interface INodeState { void Render(string tagName); }
    public class VisibleState : INodeState { public void Render(string tagName) => Console.WriteLine($"[State] <{tagName}> видимий."); }
    public class HiddenState : INodeState { public void Render(string tagName) => Console.WriteLine($"[State] <{tagName}> прихований."); }

    public interface ICommand { void Execute(); }
    public class AddChildCommand : ICommand
    {
        private readonly LightElementNode _parent;
        private readonly LightNode _child;
        public AddChildCommand(LightElementNode parent, LightNode child) { _parent = parent; _child = child; }
        public void Execute() => _parent.AddChild(_child);
    }

    public abstract class LightNode
    {
        public abstract string InnerHtml();
        public abstract string OuterHtml();

        public abstract void Accept(IVisitor visitor);
    }

    public class LightTextNode : LightNode
    {
        private string _text;
        public LightTextNode(string text) => _text = text;
        public override string InnerHtml() => _text;
        public override string OuterHtml() => _text;
        
        public override void Accept(IVisitor visitor) => visitor.VisitText(this);
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
        private INodeState _state = new VisibleState();

        public LightElementNode(string tagName, DisplayType display, ClosingType closing)
        {
            _tagName = tagName;
            _display = display;
            _closing = closing;
            OnCreated();
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitElement(this);
            foreach (var child in _children)
            {
                child.Accept(visitor);
            }
        }

        protected virtual void OnCreated() => Console.WriteLine($"[Hook] <{_tagName}> створено.");
        protected virtual void OnInserted() => Console.WriteLine($"[Hook] <{_tagName}> додано.");

        public IEnumerable<LightNode> GetDFS()
        {
            yield return this;
            foreach (var child in _children)
                if (child is LightElementNode element) foreach (var node in element.GetDFS()) yield return node;
                else yield return child;
        }

        public void SetState(INodeState state) => _state = state;
        public void AddChild(LightNode node) { _children.Add(node); if (node is LightElementNode el) el.OnInserted(); }
        public void AddClass(string className) => _cssClasses.Add(className);

        public override string InnerHtml() { StringBuilder sb = new StringBuilder(); foreach (var child in _children) sb.Append(child.OuterHtml()); return sb.ToString(); }
        public override string OuterHtml()
        {
            StringBuilder sb = new StringBuilder();
            string classes = _cssClasses.Count > 0 ? $" class=\"{string.Join(" ", _cssClasses)}\"" : "";
            sb.Append($"<{_tagName}{classes}");
            if (_closing == ClosingType.SelfClosing) sb.Append(" />");
            else { sb.Append(">"); sb.Append(InnerHtml()); sb.Append($"</{_tagName}>"); }
            if (_display == DisplayType.Block) sb.Append("\n");
            return sb.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            var div = new LightElementNode("div", DisplayType.Block, ClosingType.Normal);
            var h1 = new LightElementNode("h1", DisplayType.Block, ClosingType.Normal);
            var p = new LightElementNode("p", DisplayType.Block, ClosingType.Normal);

            ICommand cmd = new AddChildCommand(div, h1);
            cmd.Execute();
            div.AddChild(p);
            p.AddChild(new LightTextNode("Фінальний текст для статистики."));

            Console.WriteLine("\n--- Тестування ПАТЕРНУ ВІДВІДУВАЧ (ПР5) ---");
            var stats = new StatisticsVisitor();
            div.Accept(stats);
            
            Console.WriteLine($"Результат аналізу дерева:");
            Console.WriteLine($"- Кількість HTML-елементів: {stats.ElementsCount}");
            Console.WriteLine($"- Кількість текстових вузлів: {stats.TextNodesCount}");

            Console.WriteLine("\n--- Перевірка інших патернів ---");
            Console.WriteLine("Обхід дерева (Iterator):");
            foreach (var node in div.GetDFS()) Console.WriteLine($"  > {node.GetType().Name}");

            Console.ReadKey();
        }
    }
}
