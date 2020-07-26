using System;
using System.Collections.Generic;
using System.Text;

namespace CalculateTest.Abstractions
{
    enum PriorityType
    {
        Top = 4,
        Middle = 3,
        Low = 2,
        Lowest = 1
    }
    interface IOperation
    {
        char Operation { get; }
        PriorityType Priority { get; }
        ICommand GetCommand();
    }
}
