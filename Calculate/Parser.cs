using CalculateTest.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculateTest
{
    public class Parser : IParser
    {
        public Lexem[] Parse(string input)
        {
            var lexems = new List<Lexem>();

            char[] sym = input.ToCharArray();
            for (int i = 0; i < sym.Length; i++)
            {
                char c = sym[i];
                if (c == ' ')
                    continue;
                if (c == '(')
                {
                    lexems.Add(new Lexem("(", LexemType.OpenBracket));
                    continue;
                }
                if (c == ')')
                {
                    lexems.Add(new Lexem(")", LexemType.CloseBraket));
                    continue;
                }
                if (IsDigitOrPoint(c))
                {
                    int p = i;
                    while (p < input.Length && IsDigitOrPoint(input[p]))
                        ++p;

                    var value = input.Substring(i, p - i);
                    lexems.Add(new Lexem(value, LexemType.Number));
                    i = p - 1;
                    continue;
                }
                lexems.Add(new Lexem(c.ToString(), LexemType.Operator));
            }
            return lexems.ToArray();
        }

        private bool IsDigitOrPoint(char c)
        {
            return (c >= '0' && c <= '9') || c == '.';
        }
    }
}
