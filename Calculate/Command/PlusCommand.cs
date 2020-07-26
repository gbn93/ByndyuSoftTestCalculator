using Calculate.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculateTest.Abstractions.Command
{
    public class PlusCommand : ICommand
    {
        public void Calculate(Stack<decimal> heap)
        {
            if (heap.Count < 2)
                throw new CalculateException("Невозможно сложить 2 числа");

            var a = heap.Pop();
            var b = heap.Pop();
            var res = b + a;
            heap.Push(res);
        }
    }
}
