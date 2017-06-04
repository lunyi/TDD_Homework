using System.Collections.Generic;
using System.Linq;

namespace HerryPorterBook
{
    public class HerryPorterStore
    {
        public double CalculatePrice(IHerryPorterBook[] books)
        {
            var groupedBooks = books.ToLookup(p => p.Name)
                               .Select(p=> p.Sum(g=>g.Number))
                               .ToList();
            return GetGroupBooksPrice(groupedBooks);
        }

        private double CalculatePrice(int differentBookCount, int bookNumberInGroup)
        {
            switch (differentBookCount)
            {
                case 5:
                    return bookNumberInGroup * 500 * 0.75;
                case 4:
                    return bookNumberInGroup * 400 * 0.8;
                case 3:
                    return bookNumberInGroup * 300 * 0.90;
                case 2:
                    return bookNumberInGroup * 200 * 0.95;
                case 1:
                    return bookNumberInGroup * 100;
                default:
                    return 0;
            }
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

                result += CalculatePrice(differentBookCount, bookNumberInGroup);
            }
            return result;
        }
    }
}