using System;
using System.Collections.Generic;
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
            Console.WriteLine("Menu Items:   1-Game       2-High Scores       3-Exit");
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
            else if (menuOption == "3")
            {
                Environment.Exit(0);
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
            String name = "";
            int totPoints = 0;
            Boolean valid = false;

            Console.WriteLine("Lets get started!");
            while (!valid)
            {
                Console.WriteLine("Enter Your Name: ");
                name = Console.ReadLine();
                if (name != "")
                {
                    valid = true;
                }
            }

            String[] lines = System.IO.File.ReadAllLines("Questions.dat");
            List<Question> questions = new List<Question>();

            foreach (String s in lines)
            {
                String[] con = s.Split('~');
                questions.Add(new Question(con[0], con[1], con[2], con[3], int.Parse(con[4])));
            }

            foreach (Question q in questions)
            {
                q.askQuestion();
                totPoints += q.getPoints();
            }

            // Keep the console window open in debug mode.
            Console.WriteLine("Points: " + totPoints);
            con = new OleDbConnection();
            con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ScoreDB.accdb";
            cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO Scores (UserName,Score) VALUES ('" + name + "','" + totPoints + "')";
            con.Open();
            int sonuc = cmd.ExecuteNonQuery();
            con.Close();
            if (sonuc > 0)
            {
                Console.WriteLine("Inserted");
            }
            else
            {
                Console.WriteLine("You Screwed up something Dan. The record was not inserted.");
            }
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
            Menu();
        }


    }
}
