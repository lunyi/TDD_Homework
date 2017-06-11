using System.Collections.Generic;
using System.Linq;
using HerryPorterBook.Interfaces;

namespace HerryPorterBook
{
    public static class HerryPorterBookExtension
    {
        public static double GetPrice(this IEnumerable<IHerryPorterBook> books, double bookPrice, Dictionary<int, double> discounts)
        {
            var bookSets = books.ToLookup(p => p.GetType()).Select(p => p.Sum(g => g.Number)).ToList();
            double totalPrice = 0;
            while (bookSets.Any(qty => qty > 0))
            {
                var minBookQty = bookSets.Where(qty => qty > 0).Min();
                var bookSetCount = bookSets.Count(qty => qty >= minBookQty);
                for (var i = 0; i < bookSets.Count; i++)
                {
                    bookSets[i] = bookSets[i] - minBookQty;
                }
                totalPrice += minBookQty * bookPrice * bookSetCount * discounts[bookSetCount];
            }
            return totalPrice;
        }
    }
}