using System;
using System.Collections.Generic;
using System.Threading.Tasks;
public interface IRequest
{
	void Process();
}
public class BasicRequest : IRequest
{
	public void Process()
	{
		Console.WriteLine("Processing Basic Request...");
	}
}
public abstract class MiddleWareDecorator : IRequest 
{
	protected IRequest _request;
	public MiddleWareDecorator(IRequest request)
	{
		_request = request;
	}
	public abstract void Process();
}
public class LoggingMiddleware : MiddleWareDecorator
{
	public LoggingMiddleware(IRequest request) : base(request) {}
	public override void Process()
	{
		Console.WriteLine("Logging Request");
		_request.Process();
	}
}
public class CachingMiddleware : MiddleWareDecorator
{
	private static Dictionary<string,string> _cache = new Dictionary<string,string>();
	public CachingMiddleware(IRequest request) : base(request) {}
	public override void Process()
	{
		if(_cache.ContainsKey("key"))
		{
			Console.WriteLine("Fetching from the cache");
			return;
		}
		_request.Process();
		_cache["key"] = "value";
	}
}
public class AuthenticationMiddleware : MiddleWareDecorator
{
	public AuthenticationMiddleware(IRequest request) : base(request) {}
	public override void Process()
	{
		Console.WriteLine("Authenticating request");
		bool isAuthenticated = true;
		
		if(isAuthenticated)
		{
			_request.Process();
		}
		else
		{

			Console.WriteLine("Authentication failed");
		}
	}
}
public class Program
{
	public static void Main()
	{
		IRequest request = new BasicRequest();
		request = new LoggingMiddleware(request);
		request = new CachingMiddleware(request);
		request = new AuthenticationMiddleware(request);
		
		request.Process();
	}
}