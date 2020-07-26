using CalculateTest;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Calculate.Tests
{
    public class ParserTests
    {
        [Theory]
        [InlineData("123.23", LexemType.Number)]
        [InlineData("123...23", LexemType.Number)]
        [InlineData("(", LexemType.OpenBracket)]
        [InlineData(")", LexemType.CloseBraket)]
        [InlineData("*", LexemType.Operator)]
        public void Parse_SimpleLexeme_LexemeEqualsInputText(string text, LexemType type)
        {
            //IParser parser = Mock.Of<IParser>(c => c.Parse(text));
            Parser _parser = new Parser();
            Lexem[] lexemes = _parser.Parse(text);

            Assert.Single(lexemes);
            Assert.Equal(type, lexemes[0].Type);
            Assert.Equal(text, lexemes[0].Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        public void Parse_EmptyText_EmpytLexemeArray(string text)
        {
            var lexer = new Parser();

            Lexem[] lexemes = lexer.Parse(text);

            Assert.Empty(lexemes);
        }

        [Fact]
        public void Parse_NumberWithOperator_NumberAndOperatorLexems()
        {
            var lexer = new Parser();
            var text = "1+";

            Lexem[] lexemes = lexer.Parse(text);

            Assert.Equal(2, lexemes.Length);
            Assert.Equal(LexemType.Number, lexemes[0].Type);
            Assert.Equal("1", lexemes[0].Value);
            Assert.Equal(LexemType.Operator, lexemes[1].Type);
            Assert.Equal("+", lexemes[1].Value);
        }

        [Fact]
        public void Parse_WhenOperationAfterCloseBracket_Operation()
        {
            var lexer = new Parser();
            var text = ")+1";

            Lexem[] lexemes = lexer.Parse(text);

            Assert.Equal(3, lexemes.Length);
            Assert.Equal(LexemType.CloseBraket, lexemes[0].Type);
            Assert.Equal(LexemType.Operator, lexemes[1].Type);
        }

        [Fact]
        public void Parse_WhenOperationAfterNumber_Operation()
        {
            var lexer = new Parser();
            var text = "1+1";

            Lexem[] lexemes = lexer.Parse(text);

            Assert.Equal(3, lexemes.Length);
            Assert.Equal(LexemType.Number, lexemes[0].Type);
            Assert.Equal(LexemType.Operator, lexemes[1].Type);
        }
    }
}
