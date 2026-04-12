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

        protected virtual void OnCreated() => Console.WriteLine($"[Hook] Елемент <{_tagName}> створено.");
        protected virtual void OnInserted() => Console.WriteLine($"[Hook] Елемент <{_tagName}> вставлено в DOM.");
        protected virtual void OnStylesApplied(string className) => Console.WriteLine($"[Hook] До <{_tagName}> додано клас: {className}");

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

        public IEnumerable<LightNode> GetBFS()
        {
            var queue = new Queue<LightNode>();
            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                yield return current;
                if (current is LightElementNode element)
                {
                    foreach (var child in element._children)
                        queue.Enqueue(child);
                }
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
            if (node is LightElementNode element)
            {
                element.OnInserted();
            }
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

            var body = new LightElementNode("body", DisplayType.Block, ClosingType.Normal);
            var section = new LightElementNode("section", DisplayType.Block, ClosingType.Normal);
            var p = new LightElementNode("p", DisplayType.Block, ClosingType.Normal);
            
            p.AddChild(new LightTextNode("Текст для ітератора"));
            section.AddChild(p);
            body.AddChild(section);

            Console.WriteLine("\n--- Тестування Ітератора (DFS) ---");
            foreach (var node in body.GetDFS())
            {
                Console.WriteLine($"Відвідано вузол: {node.GetType().Name}");
            }

            Console.WriteLine("\n--- Кінцевий HTML ---");
            Console.WriteLine(body.OuterHtml());
            
            Console.ReadKey();
        }
    }
}
