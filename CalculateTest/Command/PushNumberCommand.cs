using CalculateTest.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculateTest.Command
{
    public class PushNumberCommand : ICommand
    {
        public readonly decimal Value;
        public PushNumberCommand(decimal value)
        {
            Value = value;
        }
        public void Calculate(Stack<decimal> heap)
        {
            heap.Push(Value);
        }
    }
}
