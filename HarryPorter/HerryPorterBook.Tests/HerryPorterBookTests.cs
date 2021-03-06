﻿using System.Collections.Generic;
using FluentAssertions;
using HerryPorterBook.Interfaces;
using HerryPorterBook.Models;
using NUnit.Framework;

namespace HerryPorterBook.Tests
{
    [TestFixture]
    public class HerryPorterBookTests
    {
        private const double BookPrice = 100;

        private static readonly Dictionary<int, double> Discounts = new Dictionary<int, double>
            {
                { 1, 1.00 },
                { 2, 0.95 },
                { 3, 0.90 },
                { 4, 0.80 },
                { 5, 0.75 }
            };

        [TestCase(1, 0, 0, 0, 0, 100)]
        [TestCase(1, 1, 0, 0, 0, 190)]
        [TestCase(1, 1, 1, 0, 0, 270)]
        [TestCase(1, 1, 1, 1, 0, 320)]
        [TestCase(1, 1, 1, 1, 1, 375)]
        [TestCase(2, 1, 1, 1, 1, 475)]
        [TestCase(3, 2, 1, 0, 0, 560)]
        [TestCase(4, 3, 2, 1, 0, 880)]
        [TestCase(5, 4, 3, 2, 1, 1255)]
        [TestCase(0, 100, 0, 0, 0, 10000)]
        [TestCase(-1, -1, -100, 0, 0, 0)]
        [TestCase(100, 50, 10, 0, 0, 15300)]
        [TestCase(10, 10, 10, 10, 10, 3750)]
        [TestCase(2, 4, 6, 8, 10, 2510)]
        public void Should_Get_Correct_Price_For_Buy_Herry_Porter_Books(int firstQty, int secondQty, int thirdQty, int fourthQty, int fifthQty, int expected)
        {
            var books = new IHerryPorterBook[]
            {
                new FirstBook(firstQty),
                new SecondBook(secondQty),
                new ThirdBook(thirdQty),
                new FourthBook(fourthQty),
                new FifthBook(fifthQty)
            };
            var actual = books.GetPrice(BookPrice, Discounts);
            actual.Should().Be(expected);
        }
    }
}
