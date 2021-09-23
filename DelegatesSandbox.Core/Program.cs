using System;
using System.Text;

/// <summary>
/// Core functionality for this small sandbox
/// </summary>
namespace DelegatesSandbox.Core
{
    /// <summary>
    /// This class is responsible for being the main entry point
    /// </summary>
    public class Program
    {
        /// <summary>
        /// This delegate will hold the format message methods references
        /// </summary>
        /// <param name="message">Message to be formatted and displayed</param>
        private delegate void FormatMessage(string message);

        /// <summary>
        /// Entry point of this application
        /// </summary>
        private static void Main()
        {
            // Declare delegates
            FormatMessage formatMessage, 
                          formatMessageToUppercase, 
                          formatMessageToLowercase,
                          formatMessageWithSpaces;

            // Set method references
            formatMessageToUppercase = FormatMessageToUppercase;
            formatMessageToLowercase = FormatMessageToLowercase;
            formatMessageWithSpaces = FormatMessageWithSpaces;

            // Add delegates to multicast delegate
            formatMessage = 
                formatMessageToUppercase + 
                formatMessageToLowercase +
                formatMessageWithSpaces;

            // Standard way of using a multicast delegate
            // Calls each method in order
            // formatMessage("Hello world");

            // Add a space
            Console.WriteLine();

            // Single instance of the random class is always a good idea
            // https://docs.microsoft.com/en-us/dotnet/api/system.random?view=net-5.0
            // Although apparently the random class in .NET Core no longer has the
            // issue of generating the same random numbers anymore if you create new
            // instances all the time
            Random random = new Random();

            // Just some random messages to pass into the methods inside of
            // the multicast delegate
            string[] messages =
            {
                "Hello world",
                "Good day",
                "Today is Thursday",
                "Code Code Code",
                "aBcDeFg"
            };

            // ------------------------------------
            // New stuff for working with methods referenced
            // inside of a multicast delegate
            // Goal was to be able to allow for different arguments
            // to be passed into each method inside of the multicast
            // delegate
            // ------------------------------------

            // Here is where the solution begins
            // Initial solution to the problem
            // formatMessage.GetInvocationList()[0].Method.Invoke(null, new string[] { "Hello world" });

            // Solution to call each method inside the multicast delegate
            // passing in random arguments to each method
            foreach (var methodReference in formatMessage.GetInvocationList())
            {
                methodReference.Method.Invoke(
                    null,
                    // This is the parameters to pass to the method
                    // The order and type should match the method signature
                    new string[]
                    {
                        // Just grabs a random message to pass as a string argument
                        messages[random.Next(0, messages.Length)]
                    });
            }

            // ------------------------------------
            // End of solution, the rest of the code is just to create
            // a small working example
            // ------------------------------------
        }

        // Not important from this point forward
        // Just some methods to make the example work
        #region Formatting methods

        /// <summary>
        /// Formats the message to uppercase
        /// </summary>
        /// <param name="message">Message to be formatted and displayed</param>
        private static void FormatMessageToUppercase(string message) =>
            Console.WriteLine(message.ToUpper());

        /// <summary>
        /// Formats the message to lowercase
        /// </summary>
        /// <param name="message">Message to be formatted and displayed</param>
        private static void FormatMessageToLowercase(string message) =>
            Console.WriteLine(message.ToLower());

        /// <summary>
        /// Adds spaces between characters in the message
        /// </summary>
        /// <param name="message">Message to be formatted and displayed</param>
        private static void FormatMessageWithSpaces(string message)
        {
            StringBuilder newMessage = new StringBuilder();

            foreach (var character in message)
            {
                newMessage.Append($"{character} ");
            }

            Console.WriteLine(newMessage.ToString());
        }

        #endregion
    }
}
