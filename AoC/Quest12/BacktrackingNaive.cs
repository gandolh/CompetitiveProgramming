using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Quest12
{
    internal class BacktrackingNaive
    {
        private string _sequence;
        private int[] _numbers;
        private int _solutions;
        private char[] _currentSolution;


        public BacktrackingNaive(string sequence, int[] numbers)
        {
            _sequence = sequence;
            _numbers = numbers;
            _solutions = 0;
            _currentSolution = new char[sequence.Length];

        }
        public int FindSolutions()
        {
            RecursiveBacktracking(0);
            Console.WriteLine(_solutions);
            return _solutions;

        }

        private void RecursiveBacktracking(int seqCurrentIndex)
        {
            if (seqCurrentIndex == _sequence.Length)
            {
                int countDiez = 0;
                int numberIndex = 0;
                for (int i = 0; i < _sequence.Length; i++)
                {
                    if (_currentSolution[i] == '#')
                        countDiez++;
                    if (_currentSolution[i] == '.' && countDiez != 0)
                    {
                        if (numberIndex >= _numbers.Length || countDiez != _numbers[numberIndex])
                            return;
                        else
                        {
                            countDiez = 0;
                            numberIndex++;
                        }
                    }

                }

                if ((numberIndex == _numbers.Length && countDiez == 0) || 
                    (numberIndex == _numbers.Length - 1 && countDiez == _numbers[numberIndex]))
                    _solutions++;
                return;
            }

            if (_sequence[seqCurrentIndex] == '#')
            {
                _currentSolution[seqCurrentIndex] = '#';
                RecursiveBacktracking(seqCurrentIndex + 1);

            }
            else if (_sequence[seqCurrentIndex] == '.')
            {
                _currentSolution[seqCurrentIndex] = '.';
                RecursiveBacktracking(seqCurrentIndex + 1);

            }
            else if (_sequence[seqCurrentIndex] == '?')
            {
                // try to put # in place of ?
                _currentSolution[seqCurrentIndex] = '#';
                RecursiveBacktracking(seqCurrentIndex + 1);
                // try to put . in place of ?
                _currentSolution[seqCurrentIndex] = '.';
                RecursiveBacktracking(seqCurrentIndex + 1);
            }


        }
    }
}
