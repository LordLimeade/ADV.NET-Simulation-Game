using System;

namespace ProgrammerSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lets get started!");

            String question;
            String answer;
            String correct;

            question = "Declare a variable called x of type Integer (Don't forget your semicolons!)";
            answer = "intx;";
            correct = "Looks good! Here comes your next challenge";

            Question one = new Question(question, answer, correct, 3);

            one.askQuestion();

            question = "Assign 3 to the variable you defined above.";
            answer = "x=3;";
            correct = "Variable defined.";

            Question two = new Question(question, answer, correct, 3);
            two.askQuestion();

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
