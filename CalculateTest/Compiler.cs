using Calculate.Infrastructure;
using CalculateTest.Abstractions;
using CalculateTest.Command;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CalculateTest
{
    class Compiler : IResolve
    {
        //private readonly Dictionary<string, IOperation> _operation;
        private readonly IOperation[] _operations;
        public Compiler(IOperation[] operations)
        {
            //_operations = operations.ToDictionary(x => new String(x.Operation, 1));
            _operations = operations; 
        }
        public ICommand[] Compile(Lexem[] lexems)
        {
            var commands = new List<ICommand>();
            var lexStack = new Stack<Lexem>();

            foreach (var lex in lexems)
            {
                switch (lex.Type)
                {
                    case LexemType.Number:
                        AddNumberLexem(lex, commands, lexStack);
                        break;

                    case LexemType.OpenBracket:
                        AddOpenBracketLexem(lex, lexStack);
                        break;

                    case LexemType.CloseBraket:
                        AddCloseBracketLexem(lex, commands, lexStack);
                        break;

                    case LexemType.Operator:
                        AddOperationLexem(lex, commands, lexStack);
                        break;

                    default:
                        throw new NotSupportedException("Неизвестный тип лексемы " + lex.Type);
                }
            }

            AddRemainingLexem(commands, lexStack);
            return commands.ToArray();
        }

        private void AddRemainingLexem(List<ICommand> commands, Stack<Lexem> lexStack)
        {
            while (lexStack.Count > 0)
            {
                var lex = lexStack.Pop();
                if (lex.Type == LexemType.OpenBracket)
                    throw new CalculateException("Обнаружена непарная открывающая скобка");

                //Перекидываем операции в результат
                if (lex.Type == LexemType.Operator)
                    commands.Add(GetOperation(lex).GetCommand());
            }
        }

        private void AddOperationLexem(Lexem lex, List<ICommand> commands, Stack<Lexem> lexStack)
        {
            if (lex.Type != LexemType.Operator)
                throw new ArgumentException("lexeme");

            while (lexStack.Count > 0 && (lexStack.Peek().Type == LexemType.Operator))
            {
                Lexem topStackOperation = lexStack.Peek();
                if (!CompareLexem(lex, topStackOperation))
                    break;

                lexStack.Pop();
                commands.Add(GetOperation(topStackOperation).GetCommand());
            }

            lexStack.Push(lex);
        }

        protected bool CompareLexem(Lexem opLexeme, Lexem topStackLexeme)
        {
            IOperation op = GetOperation(opLexeme);
            IOperation topStackOp = GetOperation(topStackLexeme);

            if (op.Priority < topStackOp.Priority)
                return true;

            return false;
        }

        private void AddCloseBracketLexem(Lexem lex, List<ICommand> commands, Stack<Lexem> lexStack)
        {
            if (lex.Type != LexemType.CloseBraket)
                throw new ArgumentException("lexeme");

            while (lexStack.Count > 0 && lexStack.Peek().Type != LexemType.OpenBracket)
            {
                var lexem = lexStack.Pop();
                if (lexem.Type == LexemType.Operator)
                    commands.Add(GetOperation(lexem).GetCommand());
            }

            if (lexStack.Count == 0 || lexStack.Peek().Type != LexemType.OpenBracket)
                throw new CalculateException("Обнаружена непарная закрывающая скобка");
            lexStack.Pop();
        }

        private void AddOpenBracketLexem(Lexem lex, Stack<Lexem> lexStack)
        {
            if (lex.Type != LexemType.OpenBracket)
                throw new ArgumentException("lexeme");
            lexStack.Push(lex);
        }

        private void AddNumberLexem(Lexem lex, List<ICommand> commands, Stack<Lexem> lexStack)
        {
            if (lex.Type != LexemType.Number)
                throw new ArgumentException("lexeme");

            decimal number;
            if (!decimal.TryParse(lex.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out number))
                throw new CalculateException("Не удалось преобразовать в число " + lex.Value);

            commands.Add(new PushNumberCommand(number));
        }

        private IOperation GetOperation(Lexem lex)
        {
            var op = _operations.Where(c => c.Operation.ToString() == lex.Value).FirstOrDefault();
            return op;
        }
    }
}
