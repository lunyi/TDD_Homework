using Day1Homework.Models.Product;

namespace Day1Homework.Repository
{
    public interface IProductionRepository
    {
        ProductModel[] GetProducts();
    }

    public class ProductionRepository : IProductionRepository
    {
        ProductModel[] IProductionRepository.GetProducts()
        {
            return new []
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
        }
    }
}
