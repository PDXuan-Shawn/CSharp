using System;// Product interfaces
public interface IButton
{
	void Render();
}
public interface IWindow
{
	void Display();
}
// Concerete Products 
public class LightButton : IButton
{
	public void Render()
	{
		Console.WriteLine("Rendering Light Theme button");
	}
}
public class LightWindow : IWindow
{
	public void Display()
	{
		Console.WriteLine("Displaying Light Theme button");
	}
}
public class DarkButton : IButton
{
	public void Render()
	{
		Console.WriteLine("Rendering Dark Theme button");
	}
}
public class DarkWindow : IWindow
{
	public void Display()
	{
		Console.WriteLine("Displaying Dark Theme button");
	}
}

// ------------- Abstract Factory -------------------
public interface IUIFactory
{
	IButton CreateButton();
	IWindow CreateWindow();
}
public class LightThemeFactory : IUIFactory
{
	public IButton CreateButton()
	{
		return new LightButton();
	}
	public IWindow CreateWindow()
	{
		return new LightWindow();
	}
}
public class DarkThemeFactory : IUIFactory
{
	public IButton CreateButton()
	{
		return new DarkButton();
	}
	public IWindow CreateWindow()
	{
		return new DarkWindow();
	}
}
public class Program
{
	public static void Main()
	{
		IUIFactory lightThemeFactory = new LightThemeFactory();
		IButton lightButton =  lightThemeFactory.CreateButton();
		IWindow lightWindow =  lightThemeFactory.CreateWindow();
		
		lightButton.Render();
		lightWindow.Display();
		
		IUIFactory darkThemeFactory = new DarkThemeFactory();
		IButton darkButton =  darkThemeFactory.CreateButton();
		IWindow darkWindow =  darkThemeFactory.CreateWindow();
		
		darkButton.Render();
		darkWindow.Display();
		
	}
}