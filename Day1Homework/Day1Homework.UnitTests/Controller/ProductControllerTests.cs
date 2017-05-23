using System;
using Day1Homework.Controller;
using Day1Homework.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day1Homework.UnitTests.Controller
{
    [TestClass]
    public class ProductControllerTests
    {
        private ProductController _controller;

        [TestInitialize]
        public void TestInitialize()
        {
            _controller = new ProductController();
        }

        [TestMethod]
        public void Can_Get_Group_Sum_for_Cost()
        {
            var expected = new [] { 6, 15, 24, 21 };
            var response = _controller.GetGroupSumByColumnAndCount(3, "Cost");
            response.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod]
        public void Can_Get_Group_Sum_for_Revenue()
        {
            var expected = new[] { 50, 66, 60 };
            var response = _controller.GetGroupSumByColumnAndCount(4, "Revenue");
            response.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod]
        public void Can_Get_Group_Sum_for_Sell_Price()
        {
            var expected = new[] { 43, 47, 51,55, 59, 31 };
            var response = _controller.GetGroupSumByColumnAndCount(2, "SellPrice");
            response.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod]
        public void Cannot_Get_Group_Sum_With_Invalid_Column()
        {
            const string expectedMessage = "Please input the following columns \"Cost, Revenue, SellPrice\".";
            Action action = () =>_controller.GetGroupSumByColumnAndCount(3, "Cost1");
            action.ShouldThrow<InvalidColumnException>(expectedMessage);
        }

        [TestMethod]
        public void Cannot_Get_Group_Sum_With_Invalid_Count()
        {
            const string expectedMessage = "Please input number between 2 and 11 for Count.";
            Action action = () => _controller.GetGroupSumByColumnAndCount(100, "Cost");
            action.ShouldThrow<InvalidCountException>(expectedMessage);
        }
    }
}
