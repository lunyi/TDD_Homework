using Day1Homework.Controller;
using Day1Homework.Controller.Messages.Product;
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
            var request = new GetGroupSumRequest
            {
                Column = "Cost",
                Count = 3
            };
            var expected = new [] { 6, 15, 24, 21 };

            var response = _controller.GetGroupSumByColumnAndCount(request);
            response.Result.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod]
        public void Can_Get_Group_Sum_for_Revenue()
        {
            var request = new GetGroupSumRequest
            {
                Column = "Revenue",
                Count = 4
            };

            var expected = new[] { 50, 66, 60 };

            var response = _controller.GetGroupSumByColumnAndCount(request);
            response.Result.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod]
        public void Can_Get_Group_Sum_for_Sell_Price()
        {
            var request = new GetGroupSumRequest
            {
                Column = "SellPrice",
                Count = 2
            };

            var expected = new[] { 43, 47, 51,55, 59, 31 };

            var response = _controller.GetGroupSumByColumnAndCount(request);
            response.Result.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod]
        public void Cannot_Get_Group_Sum_With_Invalid_Column()
        {
            var request = new GetGroupSumRequest
            {
                Column = "Cost1",
                Count = 3
            };

            const string expectedMessage = "Please input the following columns \"Cost, Revenue, SellPrice\".";

            var response = _controller.GetGroupSumByColumnAndCount(request);
            response.Valid.Should().BeFalse();
            response.Message.Should().Be(expectedMessage);
        }

        [TestMethod]
        public void Cannot_Get_Group_Sum_With_Invalid_Count()
        {
            var request = new GetGroupSumRequest
            {
                Column = "Cost",
                Count = 100
            };

            const string expectedMessage = "Please input number between 1 and 11 for Count.";

            var response = _controller.GetGroupSumByColumnAndCount(request);
            response.Valid.Should().BeFalse();
            response.Message.Should().Be(expectedMessage);
        }
    }
}
