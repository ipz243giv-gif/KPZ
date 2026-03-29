using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public interface IRenderer
    {
        string RenderAs();
    }

    public class VectorRenderer : IRenderer
    {
        public string RenderAs() => "as vectors";
    }

    public class RasterRenderer : IRenderer
    {
        public string RenderAs() => "as pixels";
    }
    public abstract class Shape
    {
        protected IRenderer _renderer;
        protected string _name;

        protected Shape(IRenderer renderer, string name)
        {
            _renderer = renderer;
            _name = name;
        }

        public abstract void Draw();
    }
    public class Circle : Shape
    {
        public Circle(IRenderer renderer) : base(renderer, "Circle") { }

        public override void Draw()
        {
            Console.WriteLine($"Drawing {_name} {_renderer.RenderAs()}");
        }
    }

    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer, "Square") { }

        public override void Draw()
        {
            Console.WriteLine($"Drawing {_name} {_renderer.RenderAs()}");
        }
    }

    public class Triangle : Shape
    {
        public Triangle(IRenderer renderer) : base(renderer, "Triangle") { }

        public override void Draw()
        {
            Console.WriteLine($"Drawing {_name} {_renderer.RenderAs()}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            IRenderer vector = new VectorRenderer();
            IRenderer raster = new RasterRenderer();

            Shape vectorCircle = new Circle(vector);
            Shape rasterTriangle = new Triangle(raster);
            Shape vectorSquare = new Square(vector);
            Shape rasterSquare = new Square(raster);

            Console.WriteLine("--- Графічний редактор ---");

            vectorCircle.Draw();
            rasterTriangle.Draw();
            vectorSquare.Draw();
            rasterSquare.Draw();

            Console.ReadKey();
        }
    }
}
