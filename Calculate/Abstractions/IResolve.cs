using System;
using System.Collections.Generic;
using System.Text;

namespace CalculateTest.Abstractions
{
    interface IResolve
    {
        ICommand[] Compile(Lexem[] lexems);
    }
}
