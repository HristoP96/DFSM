using System;
using System.Collections.Generic;

namespace DFSM
{

    class Program
    {
        static void Main(string[] args)
        {

            var Q = new List<string> { "q0", "q1", "q2", "q3", "q4" };
            var Sigma = new List<char> { 'a', 'b', 'c' };
            var Delta = new List<Transition>{
            new Transition("q0", 'c', "q1"),
            new Transition("q1", 'b', 'a', "q2"),
            new Transition("q2", 'a', "q3"),
            new Transition("q3", 'c', 'b', "q4"),
            new Transition("q4", 'b', 'c', "q4"),

            };
            var Q0 = "q0";
            var F = new List<string> { "q3", "q4" };
            var dFSM = new DeterministicFSM(Q, Sigma, Delta, Q0, F);
            Console.Write("Input a string for validation: ");
            var inputString = Console.ReadLine();
            if (inputString.Length > 8)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine("String must be max 8 characters");

            }
            else
            {
                dFSM.Accepts(inputString);

            }

        }
    }
}
