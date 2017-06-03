using Day1Homework.Controller;
using Day1Homework.Controller.Messages.Product;
using Day1Homework.Repository;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Day1Homework.UnitTests.Controller
{
    [TestClass]
    public class ProductControllerTests
    {
        private ProductController _controller;
        private IProductionRepository _repository;
        private readonly ProductModel[] _products = 
        {
            new ProductModel {Id = 1, Cost = 1, Revenue = 11, SellPrice = 21},
            new ProductModel {Id = 2, Cost = 2, Revenue = 12, SellPrice = 22},
            new ProductModel {Id = 3, Cost = 3, Revenue = 13, SellPrice = 23},
            new ProductModel {Id = 4, Cost = 4, Revenue = 14, SellPrice = 24},
            new ProductModel {Id = 5, Cost = 5, Revenue = 15, SellPrice = 25},
            new ProductModel {Id = 6, Cost = 6, Revenue = 16, SellPrice = 26},
            new ProductModel {Id = 7, Cost = 7, Revenue = 17, SellPrice = 27},
            new ProductModel {Id = 8, Cost = 8, Revenue = 18, SellPrice = 28},
            new ProductModel {Id = 9, Cost = 9, Revenue = 19, SellPrice = 29},
            new ProductModel {Id = 10, Cost = 10, Revenue = 20, SellPrice = 30},
            new ProductModel {Id = 11, Cost = 11, Revenue = 21, SellPrice = 31}
        };

        [TestInitialize]
        public void TestInitialize()
        {
            _repository = Substitute.For<IProductionRepository>();
            _repository.GetProducts()
                .Returns(_products);
            _controller = new ProductController(_repository);
        }

        [TestMethod]
        public void Can_Get_Group_Sum_for_Cost()
        {
            var request = new GetPagedSumRequest
            {
                Column = "Cost",
                PagedSize = 3
            };
            var expected = new [] { 6, 15, 24, 21 };

            var response = _controller.GetPagedSum(request);
            response.Result.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod]
        public void Can_Get_Group_Sum_for_Revenue()
        {
            var request = new GetPagedSumRequest
            {
                Column = "Revenue",
                PagedSize = 4
            };

            var expected = new[] { 50, 66, 60 };

            var response = _controller.GetPagedSum(request);
            response.Result.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod]
        public void Can_Get_Group_Sum_for_Sell_Price()
        {
            var request = new GetPagedSumRequest
            {
                Column = "SellPrice",
                PagedSize = 2
            };

            var expected = new[] { 43, 47, 51,55, 59, 31 };

            var response = _controller.GetPagedSum(request);
            response.Result.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod]
        public void Cannot_Get_Group_Sum_With_Invalid_Column()
        {
            var request = new GetPagedSumRequest
            {
                Column = "Cost1",
                PagedSize = 3
            };

            const string expectedMessage = "Please input the following columns \"Cost, Revenue, SellPrice\".";

            var response = _controller.GetPagedSum(request);
            response.Valid.Should().BeFalse();
            response.Message.Should().Be(expectedMessage);
        }

        [TestMethod]
        public void Cannot_Get_Group_Sum_With_Invalid_Count()
        {
            var request = new GetPagedSumRequest
            {
                Column = "Cost",
                PagedSize = 100
            };

            const string expectedMessage = "Please input number between 1 and 11 for Count.";

            var response = _controller.GetPagedSum(request);
            response.Valid.Should().BeFalse();
            response.Message.Should().Be(expectedMessage);
        }
    }
}
