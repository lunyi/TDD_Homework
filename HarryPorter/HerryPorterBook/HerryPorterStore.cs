using System.Collections.Generic;
using System.Linq;
using HerryPorterBook.Interfaces;

namespace HerryPorterBook
{
    public class HerryPorterStore
    {
        public double CalculatePrice(params IHerryPorterBook[] books)
        {
            var groupedBooks = books.ToLookup(p => p.GetType())
                               .Select(p=> p.Sum(g=>g.Number))
                               .ToList();
            return GetGroupBooksPrice(groupedBooks);
        }

        private double CalculatePrice(int differentBookCount, int bookNumberInGroup)
        {
            const double bookPrice = 100;
            double discountPercent;
            switch (differentBookCount)
            {
                case 5:
                    discountPercent = 0.75;
                    break;
                case 4:
                    discountPercent = 0.8;
                    break;
                case 3:
                    discountPercent = 0.9;
                    break;
                case 2:
                    discountPercent = 0.95;
                    break;
                case 1:
                    discountPercent = 1.0;
                    break;
                default:
                    discountPercent = 0;
                    break;
            }
            return bookNumberInGroup * bookPrice * differentBookCount * discountPercent;
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