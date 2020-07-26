using Calculate.Infrastructure;
using CalculateTest.Abstractions.Command;
using CalculateTest.Command;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Calculate.Tests
{
    public class CommandTests
    {
        [Fact]
        public void PushCommand_InsertValueInStack_ValueInStack()
        {
            var command = new PushNumberCommand(10m);
            var stack = new Stack<decimal>();

            command.Calculate(stack);

            Assert.Single(stack);
            Assert.Equal(10m, stack.Peek());
        }

        [Fact]
        public void PlusCommand_WithTwoNumberInStack_SumInStack()
        {
            var command = new PlusCommand();
            var stack = new Stack<decimal>();
            stack.Push(4);
            stack.Push(2);

            command.Calculate(stack);

            Assert.Single(stack);
            Assert.Equal(6m, stack.Peek());
        }

        [Fact]
        public void PlusCommand_WithOneNumberInStack_FailExecute()
        {
            var command = new PlusCommand();
            var stack = new Stack<decimal>();
            stack.Push(1);

            Exception ex = Assert.Throws<CalculateException>(() => command.Calculate(stack));

            Assert.Equal("Невозможно сложить 2 числа", ex.Message);
        }

        [Fact]
        public void MultiplyCommand_WithTwoNumberInStack_MultiplyInStack()
        {
            var command = new MultiplyCommand();
            var stack = new Stack<decimal>();
            stack.Push(3);
            stack.Push(2);

            command.Calculate(stack);

            Assert.Single(stack);
            Assert.Equal(6m, stack.Peek());
        }

        [Fact]
        public void MultiplyCommand_WithOneNumberInStack_FailExecute()
        {
            var command = new MultiplyCommand();
            var stack = new Stack<decimal>();
            stack.Push(1);

            Exception ex = Assert.Throws<CalculateException>(() => command.Calculate(stack));

            Assert.Equal("Невозможно умножить 2 числа", ex.Message);
        }

        [Fact]
        public void MinusCommand_WithTwoNumberInStack_DifferenceInStack()
        {
            var command = new MinusCommand();
            var stack = new Stack<decimal>();
            stack.Push(3);
            stack.Push(2);

            command.Calculate(stack);

            Assert.Single(stack);
            Assert.Equal(1m, stack.Peek());
        }

        [Fact]
        public void MinusCommand_WithOneNumberInStack_FailExecute()
        {
            var command = new MinusCommand();
            var stack = new Stack<decimal>();
            stack.Push(1);

            Exception ex = Assert.Throws<CalculateException>(() => command.Calculate(stack));

            Assert.Equal("Невозможно вычесть 2 числа", ex.Message);
        }

        [Fact]
        public void DivideCommand_WithTwoNumberInStack_DivideInStack()
        {
            var command = new DivideCommand();
            var stack = new Stack<decimal>();
            stack.Push(6);
            stack.Push(4);

            command.Calculate(stack);

            Assert.Single(stack);
            Assert.Equal(1.5m, stack.Peek());
        }

        [Fact]
        public void DivideCommand_WithOneNumberInStack_FailExecute()
        {
            var command = new DivideCommand();
            var stack = new Stack<decimal>();
            stack.Push(1);

            Exception ex = Assert.Throws<CalculateException>(() => command.Calculate(stack));

            Assert.Equal("Невозможно разделить 2 числа", ex.Message);
        }

        [Fact]
        public void DivideCommand_DivideByZero_FailExecute()
        {
            var command = new DivideCommand();
            var stack = new Stack<decimal>();
            stack.Push(1);
            stack.Push(0);

            Exception ex = Assert.Throws<CalculateException>(() => command.Calculate(stack));

            Assert.Equal("деление ноль", ex.Message);
        }
    }
}
