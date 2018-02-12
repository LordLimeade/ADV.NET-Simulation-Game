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

        private Boolean isCorrect = false;

        public Question(String question, String answer, String correct, int tries)
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
        }

        public void askQuestion()
        {
            isCorrect = false;

            while (!isCorrect)
            {
                Console.WriteLine(_question);
                String entry = Console.ReadLine();

                Console.Write("{0,24}", "You entered \"" + entry + "\" ");

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
                }
            }


        }
    }
}
