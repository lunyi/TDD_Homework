namespace Day1Homework.Controller.Messages.Product
{
    public class GetPagedSumResponse
    {
        public bool Valid { get; set; }
        public string Message { get; set; }
        public int[] Result { get; set; }
    }
}