using System;
using System.Data;
using System.Data.OleDb;

namespace ProgrammerSimulation
{
    class Program
    {
        static OleDbConnection con;
        static OleDbCommand cmd;
        static OleDbDataReader reader;

        static void Main(string[] args)
        {
            Menu();            
        }

        private static void Menu()
        {
            Title();
            string menuOption;
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("Menu Items:   1-Game       2-High Scores    ");
            Console.WriteLine("-----------------------------------------------------------------------");
            menuOption = Console.ReadLine();

            if (menuOption == "1")
            {
                Game();
            }
            else if(menuOption == "2")
            {
                HighScores();
            }
                
        }

        private static void Title()
        {
            Console.Clear();
            String title = @"
__________                                                                  
\______   \_______  ____   ________________    _____   _____   ___________  
 |     ___/\_  __ \/  _ \ / ___\_  __ \__  \  /     \ /     \_/ __ \_  __ \ 
 |    |     |  | \(  <_> ) /_/  >  | \// __ \|  Y Y  \  Y Y  \  ___/|  | \/ 
 |____|     |__|   \____/\___  /|__|  (____  /__|_|  /__|_|  /\___  >__|    
                        /_____/            \/      \/      \/     \/        
    _________.__              .__          __                               
   /   _____/|__| _____  __ __|  | _____ _/  |_  ___________                
   \_____  \ |  |/     \|  |  \  | \__  \\   __\/  _ \_  __ \               
   /        \|  |  Y Y  \  |  /  |__/ __ \|  | (  <_> )  | \/               
  /_______  /|__|__|_|  /____/|____(____  /__|  \____/|__|                  
          \/          \/                \/                                  
";
            Console.WriteLine(title);
        }

        private static void HighScores()
        {
            Console.Clear();
            String top10 = @"                                                                                             
                                                                                             
TTTTTTTTTTTTTTTTTTTTTTT                                         1111111        000000000     
T:::::::::::::::::::::T                                        1::::::1      00:::::::::00   
T:::::::::::::::::::::T                                       1:::::::1    00:::::::::::::00 
T:::::TT:::::::TT:::::T                                       111:::::1   0:::::::000:::::::0
TTTTTT  T:::::T  TTTTTTooooooooooo   ppppp   ppppppppp           1::::1   0::::::0   0::::::0
        T:::::T      oo:::::::::::oo p::::ppp:::::::::p          1::::1   0:::::0     0:::::0
        T:::::T     o:::::::::::::::op:::::::::::::::::p         1::::1   0:::::0     0:::::0
        T:::::T     o:::::ooooo:::::opp::::::ppppp::::::p        1::::l   0:::::0 000 0:::::0
        T:::::T     o::::o     o::::o p:::::p     p:::::p        1::::l   0:::::0 000 0:::::0
        T:::::T     o::::o     o::::o p:::::p     p:::::p        1::::l   0:::::0     0:::::0
        T:::::T     o::::o     o::::o p:::::p     p:::::p        1::::l   0:::::0     0:::::0
        T:::::T     o::::o     o::::o p:::::p    p::::::p        1::::l   0::::::0   0::::::0
      TT:::::::TT   o:::::ooooo:::::o p:::::ppppp:::::::p     111::::::1110:::::::000:::::::0
      T:::::::::T   o:::::::::::::::o p::::::::::::::::p      1::::::::::1 00:::::::::::::00 
      T:::::::::T    oo:::::::::::oo  p::::::::::::::pp       1::::::::::1   00:::::::::00   
      TTTTTTTTTTT      ooooooooooo    p::::::pppppppp         111111111111     000000000     
                                      p:::::p                                                
                                      p:::::p                                                
                                     p:::::::p                                               
                                     p:::::::p                                               
                                     p:::::::p                                               
                                     ppppppppp                                               
                                                                                             ";
            Console.WriteLine(top10);
            try
            {
                int counter = 0;
                con = new OleDbConnection();
                con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ScoreDB.accdb";
                cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM Scores order by Score Desc";
                string printLine = String.Format("{0,-8}" + "{1,-15} {2,-3}",
                        "Rank", "Name", "Score");
                Console.WriteLine(printLine);
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    counter++;
                    if (counter < 10)
                    {
                        printLine = String.Format("{0,-8}" + "{1,-15} {2,-15}",
                            counter.ToString(), reader[1], reader[2]);
                    }
                    Console.WriteLine(printLine);
                }
                con.Close();
            }
            catch (Exception error)
            {
                Console.WriteLine("Error with Database Part: " + error);
            }
            Console.WriteLine("\nPress Enter to Return to Menu");
            Console.ReadLine();
            Menu();

        }

        public static void Game()
        {
            Title();
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
