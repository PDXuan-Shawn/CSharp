using System;
// Abstract Products 
public interface IText
{
	void DisplayText();
}
public interface IImage
{
	void DisplayImage();
}
// Concrete Products for DOCX
public class DOCXText : IText
{
	public void DisplayText() => Console.WriteLine("Displaying DOCX Text");
}
public class DOCXImage: IImage
{
	public void DisplayImage() => Console.WriteLine("Displaying DOCX Image");
}


public class PDFText : IText
{
	public void DisplayText() => Console.WriteLine("Displaying PDF Text");
}
public class PDFImage: IImage
{
	public void DisplayImage() => Console.WriteLine("Displaying PDF Image");
}

// Abstract Factory
public interface IDocumentFactory
{
	IText CreateText();
	IImage CreateImage();
}

// Concrete Factories
public class DOCXFactory: IDocumentFactory
{
	public IText CreateText() => new DOCXText();
	public IImage CreateImage() => new DOCXImage();
}

public class PDFFactory: IDocumentFactory
{
	public IText CreateText() => new PDFText();
	public IImage CreateImage() => new PDFImage();
}


// Client
public class DocumentEditor
{
	private readonly IText _text;
	private readonly IImage _image;
	public DocumentEditor(IDocumentFactory factory)
	{
		_text = factory.CreateText();
		_image = factory.CreateImage();
	}
	public void DisplayContents()
	{
		_text.DisplayText();
		_image.DisplayImage();
	}
}
public class Program
{
	public static void Main()
	{
		IDocumentFactory docxFactory = new DOCXFactory();
		DocumentEditor docxEditor = new DocumentEditor(docxFactory);
		docxEditor.DisplayContents();
		
		IDocumentFactory pdfFactory = new PDFFactory();
		DocumentEditor pdfEditor = new DocumentEditor(pdfFactory);
		pdfEditor.DisplayContents();
	}
}