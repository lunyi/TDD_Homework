using System.Collections.Generic;
using System.Linq;
using HerryPorterBook.Interfaces;

namespace HerryPorterBook
{
    public static class HerryPorterStore
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
            double result = 0 ;
            while (books.Any(qty => qty > 0) )
            {
                var bookQty = books.Where(qty => qty > 0).Min();
                var countOfBookType = books.Count(qty => qty >= bookQty);
                for (var i = 0; i < books.Count; i++)
                {
                    books[i] = books[i] - bookQty;     
                }
                result += bookQty * BookPrice * countOfBookType * GetDiscount(countOfBookType);
            }
            return result;
        }

        private static double GetDiscount(int countOfBookType)
        {
            switch (countOfBookType)
            {
                case 5:
                    return 0.75;
                case 4:
                    return 0.8;
                case 3:
                    return 0.9;
                case 2:
                    return 0.95;
                case 1:
                    return 1.0;
                default:
                    return 0;
            }
        }
    }
}