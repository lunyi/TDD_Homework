using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Day1Homework.Controller.Messages.Product;

namespace Day1Homework.Controller
{
    public class ProductController
    {
        private static readonly ProductModel[] ProductData =
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

        private static readonly Dictionary<string, Func<ProductModel, int>> ProductModelGettersCache = new Dictionary<string, Func<ProductModel, int>>();

        private readonly string[] _validColumns = {"Cost", "Revenue", "SellPrice"};

        public GetGroupSumResponse GetGroupSumByColumnAndCount(GetGroupSumRequest request)
        {
            if (request.Count == 0 || request.Count > ProductData.Length)
            {
                return CreateInvalidResponse($"Please input number between 1 and {ProductData.Length} for Count.");
            }

            if (!_validColumns.Contains(request.Column))
            {
                return CreateInvalidResponse($"Please input the following columns \"{string.Join(", ", _validColumns)}\".");
            }

            var groupNumber = 0;
            foreach (var product in ProductData)
            {
                if (product.Id % request.Count == 1)
                {
                    groupNumber++;
                }
                product.Group = groupNumber;
            }

            var columnGetter = GetColumnGetter(request.Column);

            var result = (from product in ProductData
                       group product by product.Group into grp
                       select grp.Sum(columnGetter)).ToArray();

            return new GetGroupSumResponse
            {
                Result = result.ToArray()
            };
        }

        private GetGroupSumResponse CreateInvalidResponse(string message)
        {
            return new GetGroupSumResponse
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
