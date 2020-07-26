using System;
using System.Collections.Generic;
using System.Text;

namespace CalculateTest.Abstractions
{
    public interface IParser
    {
        Lexem[] Parse(string input);
    }
}
