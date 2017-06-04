using HerryPorterBook.Interfaces;

namespace HerryPorterBook.Models
{
    public class FirstBook : IHerryPorterBook
    {
        private readonly int _number;
        public FirstBook(int number = 0)
        {
            _number = number;
        }
        int IHerryPorterBook.Number => _number;
    }
}