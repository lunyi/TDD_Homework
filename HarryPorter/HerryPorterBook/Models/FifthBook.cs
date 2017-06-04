namespace HerryPorterBook
{
    public class FifthBook : IHerryPorterBook
    {
        private readonly int _number;
        public FifthBook(int number = 0)
        {
            _number = number;
        }

        int IHerryPorterBook.Number => _number;
        string IHerryPorterBook.Name => "Fifth";
    }
}