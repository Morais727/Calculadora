﻿using System;
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

                var operation = Lexer.Parse(restOfExpression, @"\+|\-|\*|\/", out tokenLength).FirstOrDefault();
                if (operation != 0)
                {
                    while (_operations.Count > 0 && OpPrecedence.IsPrecided(operation, _operations.Peek()))
                    {
                        _values.Push(Calculate(_operations.Pop(), _values.Pop(), _values.Pop()));
                    }
                    _operations.Push(operation);
                    i += tokenLength;
                }
            }

            while (_operations.Count > 0)
            {
                _values.Push(Calculate(_operations.Pop(), _values.Pop(), _values.Pop()));
            }

            var result = _values.Pop();
            if (_values.Any() || _operations.Any())
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        private static Number Calculate(char op, Number b, Number a)
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
                    return Number.FromValue(Math.Sqrt(a.AsDouble()));
                default:
                    throw new InvalidOperationException(op.ToString());
            }
        }
    }
}
