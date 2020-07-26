using System;
using System.Collections.Generic;
using System.Text;

namespace CalculateTest
{
    public class Lexem
    {
        public readonly string Value;
        public readonly LexemType Type;

        public Lexem(string value, LexemType lexemType)
        {
            Value = value;
            Type = lexemType;
        }
    }

    public enum LexemType
    {
        Number,
        OpenBracket,
        CloseBraket,
        Operator
    }
}
