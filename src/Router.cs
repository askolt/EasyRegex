﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Regexoop.src
{
    class Router : IRule
    {

        protected List<string> _result = new List<string>();

        protected List<string> _rawResult = new List<string>();

        protected InputText _input;

        protected Stack<Rule> _rule = new Stack<Rule>();

        protected Rule _rootRule;

        //protected int _cursor = 0;

        public Router(Rule rule, InputText input)
        {
            _rule.Push(rule);
            _rootRule = rule;
            _input = input;
        }

        public bool Step()
        {
            if (_input.IsComplete())
            {
                return true;
            }

            if (!_rule.Peek().CheckRequires())
            {
                return false;
            }

            while(_input.IsComplete() == false)
            {
                if (_rule.Count == 0)
                {
                    _rule.Push(_rootRule);
                }

                Rule.Status stepResult = _rule.Peek().ParseSymbol(_input);
                
                if (stepResult == Rule.Status.Complete)
                {
                    _rawResult.Add(_rule.Peek().GetResult());
                    _rule.Pop();

                    if (_rule.Count == 0) //root rule has been complete
                    {
                        _rule.Push(_rootRule);
                        string tempResult = "";
                        for (int index = _rawResult.Count - 1; index >= 0; index--)
                        {
                            tempResult += _rawResult[index];
                        }
                        _result.Add(tempResult);
                        _rawResult.Clear();
                    }
                }
                //Console.WriteLine("Char: {0}  Step: {1}", _input.GetSymbols(1), stepResult);
                //_cursor += 1;

                if (_rule.Count == 0) //todo
                {
                    _rule.Push(_rootRule);
                }
                // todo maybe let call above yourself??
                if (_rule.Peek().NeedRedirect())
                {
                    _rule.Push(_rule.Peek().Variables[_rule.Peek().GetRedirectRule()]);
                }
                else
                {
                    _input.MoveCursor(1); //todo 
                }

            }
            //Console.WriteLine(_rule.Pattern);

            return true;
        }

        public bool Complete()
        {
            return true;
        }

        public bool HasErrors()
        {
            return false;
        }

        public List<string> GetResult()
        {
            return _result;
        }
    }
}
