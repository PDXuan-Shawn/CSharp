using System;
					
// Product Interface
public interface IPaymenGateway
{
	bool ProcessPayment(decimal amount);
}

// Concrete Classes
public class StripePaymentGateway: IPaymenGateway
{
	public bool ProcessPayment(decimal amount)
	{
		// Logic to process payment 
		Console.WriteLine($"Processed payment of ${amount} using stripe");
		return true;
	}
}
public class PayPalPaymentGateway: IPaymenGateway
{
	public bool ProcessPayment(decimal amount)
	{
		// Logic to process payment 
		Console.WriteLine($"Processed payment of ${amount} using PayPal");
		return true;
	}
}
public class BanktransferPaymentGateway: IPaymenGateway
{
	public bool ProcessPayment(decimal amount)
	{
		// Logic to process payment 
		Console.WriteLine($"Processed payment of ${amount} using Banktransfer");
		return true;
	}
}
public class CryptoPaymentGateway: IPaymenGateway
{
	public bool ProcessPayment(decimal amount)
	{
		// Logic to process payment 
		Console.WriteLine($"Processed payment of ${amount} using Crypto");
		return true;
	}
}
// Factory Class
public static class PaymentGatewayFactory
{
	public static IPaymenGateway CreatePaymentGateway(string type)
	{
		switch(type)
		{
			case "Stripe":
				return new StripePaymentGateway();
			case "PayPal":
				return new PayPalPaymentGateway();
			case "BankTransfer":
				return new BanktransferPaymentGateway();
			case "Crypto":
				return new CryptoPaymentGateway();
			default:
				throw new ArgumentException("Invalid payment gateway type");
		}
	
	}
}


public class Program
{
	public static void Main()
	{
		decimal amountToPay = 100.5M;
		
		IPaymenGateway stripeGateway = PaymentGatewayFactory.CreatePaymentGateway("Stripe");
		stripeGateway.ProcessPayment(amountToPay);
		
		IPaymenGateway payPalGateway = PaymentGatewayFactory.CreatePaymentGateway("PayPal");
		payPalGateway.ProcessPayment(amountToPay);
		
		IPaymenGateway bankTransferGateway = PaymentGatewayFactory.CreatePaymentGateway("BankTransfer");
		bankTransferGateway.ProcessPayment(amountToPay);
		
		IPaymenGateway cryptoGateway = PaymentGatewayFactory.CreatePaymentGateway("Crypto");
		cryptoGateway.ProcessPayment(amountToPay);
		
		
	}
}