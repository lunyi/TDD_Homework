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

        private static double GetGroupBooksPrice(IList<int> booksNumbers)
        {
            double result = 0 ;
            // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
            while (booksNumbers.Any(b => b > 0) )
            {
                var bookNumberInGroup = booksNumbers.Where(b => b > 0).Min();
                var differentBookCount = booksNumbers.Count(p => p >= bookNumberInGroup);
                for (var i = 0; i < booksNumbers.Count; i++)
                {
                    booksNumbers[i] = booksNumbers[i] - bookNumberInGroup;     
                }
                result += bookNumberInGroup * BookPrice * differentBookCount * GetDiscountPercent(differentBookCount);
            }
            return result;
        }

        private static double GetDiscountPercent(int differentBookCount)
        {
            switch (differentBookCount)
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