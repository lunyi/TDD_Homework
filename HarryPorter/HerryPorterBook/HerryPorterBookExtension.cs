using System.Collections.Generic;
using System.Linq;
using HerryPorterBook.Interfaces;

namespace HerryPorterBook
{
    public static class HerryPorterBookExtension
    {
        private const double BookPrice = 100;
        public static double GetPrice(this IEnumerable<IHerryPorterBook> books)
        {
            var groupedBooks = books.ToLookup(p => p.GetType())
                               .Select(p => p.Sum(g => g.Number))
                               .ToList();
            return GetGroupBooksPrice(groupedBooks);
        }

        private static double GetGroupBooksPrice(IList<int> books)
        {
            double totalPrice = 0 ;
            while (books.Any(qty => qty > 0) )
            {
                var bookQty = books.Where(qty => qty > 0).Min();
                var countOfBookType = books.Count(qty => qty >= bookQty);
                for (var i = 0; i < books.Count; i++)
                {
                    books[i] = books[i] - bookQty;     
                }
                totalPrice += bookQty * BookPrice * countOfBookType * GetDiscount(countOfBookType);
            }
            return totalPrice;
        }

        private static double GetDiscount(int countOfBookType)
        {
            double discount;
            GetAllDiscounts().TryGetValue(countOfBookType, out discount);
            return discount;
        }

        private static Dictionary<int, double> GetAllDiscounts()
        {
            return new Dictionary<int, double>
            {
                { 1, 1.00 },
                { 2, 0.95 },
                { 3, 0.90 },
                { 4, 0.80 },
                { 5, 0.75 },
            };
        }
    }
}