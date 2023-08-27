using System;
using System.Collections.Generic;
using System.Linq;

namespace Trabalho_final.Controller
{
    public class CalculatorController : CalculatorBase
    {
        private Stack<Number> _values;
        private Stack<char> _operations;
        public override Number Calculate(string expression)
        {
            _values = new Stack<Number>();
            _operations = new Stack<char>();
            var i = 0;
            if(expression.First() == '-')
                _values.Push(Number.Create("0"));
            while (true)
            {
                var restOfExpression = expression.Substring(i);
                if (string.IsNullOrEmpty(restOfExpression))
                {
                    break;
                }

                var number = Lexer.Parse(restOfExpression, out var tokenLength);
                if (number != null)
                {
                    _values.Push(number);
                    i += tokenLength;
                    continue;
                }

                if (Lexer.ParseExact(restOfExpression, "(", out tokenLength) != null)
                {
                    _operations.Push('(');
                    i += tokenLength;
                    if(restOfExpression.ElementAt(1) == '-' )
                        _values.Push(Number.Create("0"));
                    continue;
                }

                if (Lexer.ParseExact(restOfExpression, ")", out tokenLength) != null)
                {
                    while (_operations.Peek() != '(')
                    {
                        _values.Push(Calculate(_operations.Pop(), _values.Pop(), _values.Pop()));
                    }
                    _operations.Pop();
                    i += tokenLength;
                    continue;
                }

                var operation = Lexer.Parse(restOfExpression, @"\+|\-|\*|\/|\^|\√|%", out tokenLength).FirstOrDefault();
                if (operation != 0)
                {
                    while (_operations.Count > 0 && OpPrecedence.IsPrecided(operation, _operations.Peek()))
                    {
                        if(_operations.Peek() == '%')
                            _values.Push(Calculate(_operations.Pop(),_values.Pop(), Number.Create("0")));
                        else if(_operations.Peek() == '√')
                            _values.Push(Calculate(_operations.Pop(), _values.Pop(), Number.Create("0")));
                        else
                            _values.Push(Calculate(_operations.Pop(), _values.Pop(), _values.Pop()));
                    }
                    _operations.Push(operation);
                    i += tokenLength;
                }
            }

            while (_operations.Count > 0)
            {
                if(_operations.Peek() == '%'){
                    var opaux = _operations.Pop();
                    if(_operations.Count > 0 && (_operations.Peek() == '+' || _operations.Peek() == '-')){
                        _values.Push(Calculate(opaux, _values.Pop(), _values.Pop(),_operations.Pop()));
                    }
                    else{
                        _values.Push(Calculate(opaux,_values.Pop(), Number.Create("0")));
                    }
                }
                else 
                    if(_operations.Peek() == '√')
                        _values.Push(Calculate(_operations.Pop(), _values.Pop(), Number.Create("0")));
                else
                    _values.Push(Calculate(_operations.Pop(), _values.Pop(), _values.Pop()));
            }

            var result = _values.Pop();
            if (_values.Any() || _operations.Any())
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        private static Number Calculate(char op, Number b, Number a, char opp = '%')
        {
            switch (op)
            {
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '*':
                    return a * b;
                case '/':
                    return a / b;
                case '^': 
                    return Number.Pow(a, b);
                case '√':
                    return Number.Root(b);
                case '%':
                    return Number.Percentage(a,b,opp);
                default:
                    throw new InvalidOperationException(op.ToString());
            }
        }
    }
}
