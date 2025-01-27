﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regexoop.src
{
    class TextCommand : ICommand
    {

        private string _startCommand;

        private string _middle;

        private string _endCommand;

        public override string StartCommand { get => _startCommand; }
        public override string Middle { get => _middle; set => _middle = value; }
        public override string EndCommand { get => _endCommand; }

        public override Rule.Status Parse(ref InputText inputText, in Rule rule)
        {
            if (inputText.GetSymbols(1) == Middle)
            {
                rule.SetResult(Middle);
                return Rule.Status.Step;
            }
            return Rule.Status.Wrong;
        }
    }
}
