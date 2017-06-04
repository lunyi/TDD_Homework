namespace HerryPorterBook
{
    public class FourthBook : IHerryPorterBook
    {
        private readonly int _number;
        public FourthBook(int number = 0)
        {
            _number = number;
        }

        int IHerryPorterBook.Number => _number;
        string IHerryPorterBook.Name => "Fourth";
    }
}