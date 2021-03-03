using PostmarkDotNet;
using System;
using System.Threading.Tasks;

public class Program
{
	public static async Task Main(string[] args)
	{
		var message = new PostmarkMessage()
		{
			To = "matt@infinimuse.com",
			From = "support@infinimuse.com",
			TrackOpens = true,
			Subject = "Our First Test Email",
			TextBody = "Hello, this is our first test email",
			HtmlBody = "<strong>Hello</strong> dear Postmark user.",
			MessageStream = "outbound",
			Tag = "Test Email"
		};

		var client = new PostmarkClient("7e3947d6-ad88-41aa-91ae-8166ae128b21");
		var sendResult = await client.SendMessageAsync(message);

		if (sendResult.Status == PostmarkStatus.Success)
        {
			Console.WriteLine("Success");
        }
        else
        {
			Console.WriteLine("Failure");
        }


	}	
		
}

