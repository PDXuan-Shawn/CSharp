using System;
/*
	Component(Coffee): This is the main object to which you want to add some responsibility dynamically.
	Concrete Component(SimpleCoffe): This is an concrete object yo'll be decorating
	Decorator: This is an abstract class that subclasses the component is the base for concrete decorators
	ConcreteDecorator (e.g. MilkDecorator, SugarDecorator): These are the decorator subclasses that add nes functionalities to the component
*/
public abstract class Coffee 
{
		public abstract double Cost();
		public abstract string Description { get;}
}
public class SimpleCoffee: Coffee
{
	public override double Cost()
	{
		return 5; // Base cost for coffee
	}
	public override string Description => "Simple Coffee";
}
public abstract class CoffeeDecorator : Coffee
{
	protected Coffee _coffee;
	public CoffeeDecorator(Coffee coffe)
	{
		_coffee = coffe;
	}
	public abstract override double Cost();
	public abstract override string Description { get;}
}
public class MilkDecorator : CoffeeDecorator
{
	public MilkDecorator(Coffee coffee) : base(coffee) {}
	public override double Cost()
	{
		return _coffee.Cost()+1; // Base cost for coffee
	}
	public override string Description => _coffee.Description + ", with milk";
}
public class SugarDecorator : CoffeeDecorator
{
	public SugarDecorator(Coffee coffee) : base(coffee) {}
	public override double Cost()
	{
		return _coffee.Cost()+1; // Base cost for coffee
	}
	public override string Description => _coffee.Description + ", with sugar";
}

public class Program
{
	public static void Main()
	{
		Coffee simpleCoffee = new SimpleCoffee();
		Console.WriteLine($"{simpleCoffee.Description}: ${simpleCoffee.Cost()}");
		
		Coffee milkCoffe = new MilkDecorator(simpleCoffee);
		Console.WriteLine($"{milkCoffe.Description}: ${milkCoffe.Cost()}");
		
		Coffee sugarCoffee = new SugarDecorator(milkCoffe);
		Console.WriteLine($"{sugarCoffee.Description}: ${sugarCoffee.Cost()}");
	}
}