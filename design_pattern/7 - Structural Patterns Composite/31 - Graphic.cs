using System;
using System.Collections.Generic;
public interface IGraphic
{
	void Draw();
}
// Leaf 
public class Circle: IGraphic
{
	public void Draw()
	{
		Console.WriteLine("Drawing Circle");
		
	}
}
public class Rectangle : IGraphic
{
	public void Draw()
	{
		Console.WriteLine("Drawing Rectangle");
	}
}
// Composite 
public class CompositeGraphic: IGraphic
{
	private readonly List<IGraphic> _graphics = new List<IGraphic>();
	public void add(IGraphic graphic)
	{
		_graphics.Add(graphic);
	}
	public void remove(IGraphic graphic)
	{
		_graphics.Remove(graphic);
	}
	public void Draw()
	{
		foreach(var graphic in _graphics)
		{
			graphic.Draw();
		}
	}
}
public class Program
{
	public static void Main()
	{
		IGraphic circle = new Circle();
		IGraphic rectangle = new Rectangle();
		CompositeGraphic graphics1 = new CompositeGraphic();
		graphics1.add(circle);
		graphics1.add(rectangle);
		
		CompositeGraphic graphics2 = new CompositeGraphic();
		graphics2.add(graphics1);
		graphics1.add(new Circle());
		graphics2.Draw();
		
		
	}
}