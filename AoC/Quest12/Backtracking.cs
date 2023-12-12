using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Quest12
{
    internal class Backtracking
    {
        private string _sequence;
        private int[] _numbers;
        private Int64 _solutions;
        private char[] _currentSolution;


        public Backtracking(string sequence, int[] numbers)
        {
            _sequence = sequence;
            _numbers = numbers;
            _solutions = 0;
            _currentSolution = new char[sequence.Length];

        }
        public Int64 FindSolutions()
        {
            RecursiveBacktracking(0, 0, 0);
            Console.WriteLine(_solutions);
            return _solutions;

        }

        private void RecursiveBacktracking(int seqCurrentIndex, int numCurrentIndex, int accumulatedDiez)
        {
            if (seqCurrentIndex == _sequence.Length)
            {
                if (numCurrentIndex == _numbers.Length && accumulatedDiez == 0)
                    _solutions++;
                else if (numCurrentIndex == _numbers.Length - 1 && accumulatedDiez == _numbers[numCurrentIndex])
                    _solutions++;
                return;
            }

            if (seqCurrentIndex > 0 && _currentSolution[seqCurrentIndex - 1] == '.')
            {
                if (accumulatedDiez != 0)
                {
                    if (numCurrentIndex < _numbers.Length && accumulatedDiez == _numbers[numCurrentIndex])
                    {
                        numCurrentIndex++;
                        accumulatedDiez = 0;
                    }
                    else return;

                }
            }



            if (_sequence[seqCurrentIndex] == '#')
            {
                _currentSolution[seqCurrentIndex] = '#';
                RecursiveBacktracking(seqCurrentIndex + 1, numCurrentIndex, accumulatedDiez + 1);

            }
            else if (_sequence[seqCurrentIndex] == '.')
            {
                _currentSolution[seqCurrentIndex] = '.';
                RecursiveBacktracking(seqCurrentIndex + 1, numCurrentIndex, accumulatedDiez);

            }
            else if (_sequence[seqCurrentIndex] == '?')
            {
                // try to put # in place of ?
                _currentSolution[seqCurrentIndex] = '#';
                RecursiveBacktracking(seqCurrentIndex + 1, numCurrentIndex, accumulatedDiez + 1);
                // try to put . in place of ?
                _currentSolution[seqCurrentIndex] = '.';
                RecursiveBacktracking(seqCurrentIndex + 1, numCurrentIndex, accumulatedDiez);
            }


        }
    }
}
