using System;
using System.Collections.Generic;

public abstract class Subscription
{
    public abstract decimal MonthlyFee { get; }
    public abstract int MinimumPeriodMonths { get; }
    public abstract List<string> Channels { get; }

    public void PrintDetails()
    {
        Console.WriteLine($"Тип: {this.GetType().Name}");
        Console.WriteLine($"Ціна: {MonthlyFee} грн/міс");
        Console.WriteLine($"Мін. період: {MinimumPeriodMonths} міс.");
        Console.WriteLine($"Канали: {string.Join(", ", Channels)}");
        Console.WriteLine(new string('-', 30));
    }
}

public class DomesticSubscription : Subscription
{
    public override decimal MonthlyFee => 150.00m;
    public override int MinimumPeriodMonths => 1;
    public override List<string> Channels => new List<string> { "Новини", "Кіно", "Музика" };
}

public class EducationalSubscription : Subscription
{
    public override decimal MonthlyFee => 100.00m;
    public override int MinimumPeriodMonths => 3;
    public override List<string> Channels => new List<string> { "Discovery", "History", "Science" };
}

public class PremiumSubscription : Subscription
{
    public override decimal MonthlyFee => 400.00m;
    public override int MinimumPeriodMonths => 12;
    public override List<string> Channels => new List<string> { "Всі канали", "4K контент", "Спорт HD" };
}

public abstract class SubscriptionProvider
{
    public abstract Subscription CreateSubscription(string type);
}

public class WebSite : SubscriptionProvider
{
    public override Subscription CreateSubscription(string type)
    {
        Console.WriteLine("[Система]: Створення підписки через Web-сайт...");

        switch (type.ToLower())
        {
            case "domestic":
                return new DomesticSubscription();
            case "educational":
                return new EducationalSubscription();
            case "premium":
                return new PremiumSubscription();
            default:
                throw new ArgumentException("Невірний тип підписки");
        }
    }
}

public class MobileApp : SubscriptionProvider
{
    public override Subscription CreateSubscription(string type)
    {
        Console.WriteLine("[Система]: Оформлення через Mobile App...");
        switch (type.ToLower())
        {
            case "domestic":
                return new DomesticSubscription();
            case "educational":
                return new EducationalSubscription();
            case "premium":
                return new PremiumSubscription();
            default:
                throw new ArgumentException("Невірний тип підписки");
        }
    }
}

public class ManagerCall : SubscriptionProvider
{
    public override Subscription CreateSubscription(string type)
    {
        Console.WriteLine("[Система]: Менеджер оформлює підписку вручну...");
        switch (type.ToLower())
        {
            case "domestic":
                return new DomesticSubscription();
            case "educational":
                return new EducationalSubscription();
            case "premium":
                return new PremiumSubscription();
            default:
                throw new ArgumentException("Невірний тип підписки");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        SubscriptionProvider webProvider = new WebSite();
        Subscription sub1 = webProvider.CreateSubscription("domestic");
        sub1.PrintDetails();

        SubscriptionProvider mobileProvider = new MobileApp();
        Subscription sub2 = mobileProvider.CreateSubscription("educational");
        sub2.PrintDetails();

        SubscriptionProvider managerProvider = new ManagerCall();
        Subscription sub3 = managerProvider.CreateSubscription("premium");
        sub3.PrintDetails();

        Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
        Console.ReadKey();
    }
}