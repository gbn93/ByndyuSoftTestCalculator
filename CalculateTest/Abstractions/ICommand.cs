using System;
using System.Collections.Generic;
using System.Text;

namespace CalculateTest.Abstractions
{
    interface ICommand
    {
        void Calculate(Stack<decimal> heap);
    }
}
