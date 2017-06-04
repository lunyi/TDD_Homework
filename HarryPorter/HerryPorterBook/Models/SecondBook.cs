using HerryPorterBook.Interfaces;

namespace HerryPorterBook.Models
{
    public class SecondBook : IHerryPorterBook
    {
        private readonly int _number;
        public SecondBook(int number = 0)
        {
            _number = number;
        }
        int IHerryPorterBook.Number => _number;
    }
}