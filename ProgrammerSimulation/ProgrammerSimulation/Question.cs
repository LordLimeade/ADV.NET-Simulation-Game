using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammerSimulation
{
    class Question
    {
        /**
         * tries: the amount of tries the user has to correctly answer the question for points.
         * points: a dynami value of how many points will be given once the question is answered correctly.
         * question: the question to be asked.
         * answer: the exact syntax of the correct answer.
         * **Remove all empty spaces from the answer before setting it.
         * correct: the correct
         */

        private int _tries;
        private int _points;

        private String _question;
        private String _answer;
        private String _correct;
        private String _help = "If you're having trouble check out this resource.\n";

        private Boolean isCorrect = false;

        public Question(String question, String answer, String correct, String help, int tries)
        {
            if (tries <= 0)
            {
                _tries = 1;
            }
            else
            {
                _tries = tries;
            }

            _question = question;
            _answer = answer;
            _correct = correct;
            _points = _tries;
            _help += help;
        }

        public int getPoints()
        {
            return _points;
        }

        public void askQuestion()
        {
            isCorrect = false;
            int count = 1;
            
            while (count <= _tries + 1)
            {
                Console.WriteLine("\n-------------------------------------------");
                Console.WriteLine(_question);
                String entry = Console.ReadLine();

                Console.WriteLine("{0,-24}", "You entered \"" + entry + "\" ");

                entry = entry.Replace(" ", String.Empty);

                if (System.Text.RegularExpressions.Regex.IsMatch(entry, _answer, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    Console.Write(_correct);

                    Console.WriteLine();
                    isCorrect = true;
                }
                else
                {
                    Console.WriteLine();
                    _points -= 1;
                    if (_points < 0)
                    {
                        _points = 0;
                    }
                }

                if (count == _tries - 1)
                {
                    Console.WriteLine(_help);
                }
                count++;
            }
        }
    }
}
