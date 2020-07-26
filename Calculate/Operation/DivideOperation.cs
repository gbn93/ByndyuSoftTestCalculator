using CalculateTest.Abstractions.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculateTest.Abstractions.Operation
{
    class DivideOperation : IOperation
    {
        public char Operation 
        { 
            get { return '/'; } 
        }

        public PriorityType Priority
        {
            get { return PriorityType.Middle; }
        }

        public ICommand GetCommand()
        {
            return new DivideCommand();
        }
    }
}
