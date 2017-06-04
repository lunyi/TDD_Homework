
namespace HerryPorterBook
{
    public class ThirdBook : IHerryPorterBook
    {
        private readonly int _number;
        public ThirdBook(int number = 0)
        {
            _number = number;
        }

        int IHerryPorterBook.Number => _number;

        string IHerryPorterBook.Name => "Third";
    }
}