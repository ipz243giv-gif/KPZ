using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public interface ILaptop { void GetInfo(); }
    public interface INetbook { void GetInfo(); }
    public interface IEBook { void GetInfo(); }
    public interface ISmartphone { void GetInfo(); }

    public class IProneLaptop : ILaptop { public void GetInfo() => Console.WriteLine("Laptop: IProne Air (Ultra-thin)"); }
    public class IProneNetbook : INetbook { public void GetInfo() => Console.WriteLine("Netbook: IProne Mini (Stylish)"); }
    public class IProneEBook : IEBook { public void GetInfo() => Console.WriteLine("EBook: IProne Read (Retina Display)"); }
    public class IProneSmartphone : ISmartphone { public void GetInfo() => Console.WriteLine("Smartphone: IProne 15 Pro"); }

    public class KiaomiLaptop : ILaptop { public void GetInfo() => Console.WriteLine("Laptop: Kiaomi Book (Budget Performance)"); }
    public class KiaomiNetbook : INetbook { public void GetInfo() => Console.WriteLine("Netbook: Kiaomi Go (Portable)"); }
    public class KiaomiEBook : IEBook { public void GetInfo() => Console.WriteLine("EBook: Kiaomi Paper (E-Ink)"); }
    public class KiaomiSmartphone : ISmartphone { public void GetInfo() => Console.WriteLine("Smartphone: Kiaomi RedNote 13"); }

    public class BalaxyLaptop : ILaptop { public void GetInfo() => Console.WriteLine("Laptop: Balaxy Ultra Book (Workstation)"); }
    public class BalaxyNetbook : INetbook { public void GetInfo() => Console.WriteLine("Netbook: Balaxy Tab Pro (Hybrid)"); }
    public class BalaxyEBook : IEBook { public void GetInfo() => Console.WriteLine("EBook: Balaxy Leaf (Long Battery)"); }
    public class BalaxySmartphone : ISmartphone { public void GetInfo() => Console.WriteLine("Smartphone: Balaxy S24 Ultra"); }
    public interface ITechFactory
    {
        ILaptop CreateLaptop();
        INetbook CreateNetbook();
        IEBook CreateEBook();
        ISmartphone CreateSmartphone();
    }
    public class IProneFactory : ITechFactory
    {
        public ILaptop CreateLaptop() => new IProneLaptop();
        public INetbook CreateNetbook() => new IProneNetbook();
        public IEBook CreateEBook() => new IProneEBook();
        public ISmartphone CreateSmartphone() => new IProneSmartphone();
    }

    public class KiaomiFactory : ITechFactory
    {
        public ILaptop CreateLaptop() => new KiaomiLaptop();
        public INetbook CreateNetbook() => new KiaomiNetbook();
        public IEBook CreateEBook() => new KiaomiEBook();
        public ISmartphone CreateSmartphone() => new KiaomiSmartphone();
    }

    public class BalaxyFactory : ITechFactory
    {
        public ILaptop CreateLaptop() => new BalaxyLaptop();
        public INetbook CreateNetbook() => new BalaxyNetbook();
        public IEBook CreateEBook() => new BalaxyEBook();
        public ISmartphone CreateSmartphone() => new BalaxySmartphone();
    }

    internal class Program
    {
        static void ClientCode(ITechFactory factory, string brandName)
        {
            Console.WriteLine($"=== Виробництво техніки бренду: {brandName} ===");

            ILaptop laptop = factory.CreateLaptop();
            INetbook netbook = factory.CreateNetbook();
            IEBook ebook = factory.CreateEBook();
            ISmartphone phone = factory.CreateSmartphone();

            laptop.GetInfo();
            netbook.GetInfo();
            ebook.GetInfo();
            phone.GetInfo();

            Console.WriteLine(new string('-', 45));
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            ClientCode(new IProneFactory(), "IProne");
            ClientCode(new KiaomiFactory(), "Kiaomi");
            ClientCode(new BalaxyFactory(), "Balaxy");

            Console.WriteLine("Програму завершено. Натисніть будь-яку клавішу...");
            Console.ReadKey();
        }
    }
}