using FluentAssertions;
using HerryPorterBook.Interfaces;
using HerryPorterBook.Models;
using NUnit.Framework;

namespace HerryPorterBook.Tests
{
    [TestFixture]
    public class HerryPorterBookTests
    {
        [TestCase(1, 0, 0, 0, 0, 100)]
        [TestCase(1, 1, 0, 0, 0, 190)]
        [TestCase(1, 1, 1, 0, 0, 270)]
        [TestCase(1, 1, 1, 1, 0, 320)]
        [TestCase(1, 1, 1, 1, 1, 375)]
        [TestCase(2, 1, 1, 1, 1, 475)]
        [TestCase(3, 2, 1, 0, 0, 560)]
        [TestCase(4, 3, 2, 1, 0, 880)]
        [TestCase(5, 4, 3, 2, 1, 1255)]
        public void Should_Get_Correct_Price_For_Buy_Herry_Porter_Books(int firstNum, int secondNum, int thirdNum, int fourthNum, int fifthNum, int expected)
        {
            var books = new IHerryPorterBook[]
            {
                new FirstBook(firstNum),
                new SecondBook(secondNum),
                new ThirdBook(thirdNum),
                new FourthBook(fourthNum),
                new FifthBook(fifthNum)
            };
            var actual = books.GetPrice();
            actual.Should().Be(expected);
        }
    }
}
