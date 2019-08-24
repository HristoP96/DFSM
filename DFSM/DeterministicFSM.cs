using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DFSM.Program;

namespace DFSM
{
    class DeterministicFSM
    {

        public void Accepts(string input)
        {
            var currentState = Q0;
            var steps = new StringBuilder();
            foreach (var symbol in input.ToCharArray())
            {
                var transition = Delta.Find((t) =>
                {
                    if (t.Symbols == null)
                    {
                        return t.StartState == currentState && t.Symbol == symbol;
                    }

                    if (t.StartState == currentState && t.Symbols.Contains(symbol))
                    {
                        t.ChosenSymbol = symbol;
                        return true;
                    }
                    return false;
                });
                if (transition == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine("No transitions for current state and symbol");
                    Console.Error.WriteLine(steps);
                    return;
                }
                currentState = transition.EndState;
                steps.Append(transition + "\n");
            }
            if (F.Contains(currentState))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Accepted the input with steps:\n" + steps);
                return;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine("Stopped in state " + currentState +
                                 " which is not a final state.");
            Console.Error.WriteLine(steps);
        }
        private readonly List<string> Q = new List<string>();
        private readonly List<char> Sigma = new List<char>();
        private readonly List<Transition> Delta = new List<Transition>();
        private string Q0;
        private readonly List<string> F = new List<string>();

        private void AddTransitions(IEnumerable<Transition> transitions)
        {
            foreach (var transition in transitions.Where(ValidTransition))
            {
                Delta.Add(transition);
            }
        }

        private bool ValidTransition(Transition transition)
        {
            if (transition.Symbols == null)
            {
                return Q.Contains(transition.StartState) &&
                    Q.Contains(transition.EndState) &&
                    Sigma.Contains(transition.Symbol) &&
                    !TransitionAlreadyDefined(transition);
            }
            return Q.Contains(transition.StartState) &&
                       Q.Contains(transition.EndState) &&
                       transition.Symbols.All(s => Sigma.Contains(s)) &&
                       !TransitionAlreadyDefined(transition);
        }

        private bool TransitionAlreadyDefined(Transition transition)
        {
            if (transition.Symbols == null)
            {
                return Delta.Any(t => t.StartState == transition.StartState &&
                                  t.Symbol == transition.Symbol);
            }

            return Delta.Any(t => t.StartState == transition.StartState &&
                                  t.Symbols.All(s => transition.Symbols.Contains(s)));
        }

        private void AddInitialState(string q0)
        {
            if (Q.Contains(q0))
            {
                Q0 = q0;
            }
        }

        private void AddFinalStates(IEnumerable<string> finalStates)
        {
            foreach (var finalState in finalStates.Where(
                       finalState => Q.Contains(finalState)))
            {
                F.Add(finalState);
            }
        }

        public DeterministicFSM(IEnumerable<string> q, IEnumerable<char> sigma,
                                IEnumerable<Transition> delta, string q0,
                                IEnumerable<string> f)
        {
            Q = q.ToList();
            Sigma = sigma.ToList();
            AddTransitions(delta);
            AddInitialState(q0);
            AddFinalStates(f);
        }
    }
}
