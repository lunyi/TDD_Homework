using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Day1Homework.Controller.Messages.Product;
using Day1Homework.Extensions;
using Day1Homework.Repository;

namespace Day1Homework.Controller
{
    public class ProductController
    {
        private readonly IProductionRepository _repository;
        public ProductController(IProductionRepository repository)
        {
            _repository = repository;
        }

        // ReSharper disable once CollectionNeverUpdated.Local
        private static readonly Dictionary<string, Func<ProductModel, int>> ProductModelGettersCache = new Dictionary<string, Func<ProductModel, int>>();

        private readonly string[] _validColumns = {"Cost", "Revenue", "SellPrice"};

        public GetPagedSumResponse GetPagedSum(GetPagedSumRequest request)
        {
            var products = _repository.GetProducts();

            if (request.PagedSize == 0 || request.PagedSize > products.Length)
            {
                return CreateInvalidResponse($"Please input number between 1 and {products.Length} for Count.");
            }

            if (!_validColumns.Contains(request.Column))
            {
                return CreateInvalidResponse($"Please input the following columns \"{string.Join(", ", _validColumns)}\".");
            }

            return new GetPagedSumResponse
            {
                Result = products.GetSum(request.PagedSize, GetColumnGetter(request.Column)).ToArray()
            };
        }

        private GetPagedSumResponse CreateInvalidResponse(string message)
        {
            return new GetPagedSumResponse
            {
                Valid = false,
                Message = message
            };
        }

        private static Func<ProductModel, int> GetColumnGetter(string column)
        {
            Func<ProductModel, int> getter;

            if (ProductModelGettersCache.TryGetValue(column, out getter))
                return getter;

            var par = Expression.Parameter(typeof(ProductModel));
            var exp = Expression.Lambda<Func<ProductModel, int>>(Expression.Property(par, column), par);

            return exp.Compile();
        }
    }
}
