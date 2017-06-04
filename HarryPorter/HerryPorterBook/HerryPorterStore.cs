using System.Collections.Generic;
using System.Linq;
using HerryPorterBook.Interfaces;

namespace HerryPorterBook
{
    public class HerryPorterStore
    {
        private const double BookPrice = 100;
        public double CalculatePrice(params IHerryPorterBook[] books)
        {
            var groupedBooks = books.ToLookup(p => p.GetType())
                               .Select(p=> p.Sum(g=>g.Number))
                               .ToList();
            return GetGroupBooksPrice(groupedBooks);
        }

        private double GetGroupBooksPrice(IList<int> booksNumbers)
        {
            double result = 0 ;
            // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
            while (booksNumbers.Any(b => b > 0) )
            {
                var bookNumberInGroup = booksNumbers.Where(b => b > 0).Min();
                var differentBookCount = booksNumbers.Count(p => p >= bookNumberInGroup);
                for (var i = 0; i < booksNumbers.Count; i++)
                {
                    var tmp = booksNumbers[i] - bookNumberInGroup;
                    booksNumbers[i] = tmp;       
                }
                result += bookNumberInGroup * BookPrice * differentBookCount * GetDiscountPercent(differentBookCount);
            }
            return result;
        }

        private double GetDiscountPercent(int differentBookCount)
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