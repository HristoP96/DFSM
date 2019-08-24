using System;
using System.Collections.Generic;
using System.Text;

namespace DFSM
{
    class Transition
    {
        public string StartState { get; private set; }
        public char Symbol { get; private set; }
        public List<char> Symbols { get; private set; }
        public string EndState { get; private set; }

        public char ChosenSymbol {  get;  set; }

        public Transition(string startState, char symbol, string endState)
        {
            StartState = startState;
            Symbol = symbol;
            EndState = endState;
        }

        public Transition(string startState, char symbol, char symbol2, string endState)
        {
            StartState = startState;
            Symbols = new List<char>(){symbol, symbol2};
            EndState = endState;
        }

        public override string ToString()
        {
            if (Symbols == null)
            {
                return string.Format("({0}, {1}) -> {2}", StartState, Symbol, EndState);
            }
            return string.Format("({0}, {1}) -> {2}", StartState, ChosenSymbol, EndState);
        }
    }
}
