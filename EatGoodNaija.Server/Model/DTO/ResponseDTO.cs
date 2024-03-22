namespace EatGoodNaija.Server.Model.DTO
{
    public class ResponseDTO<T>
    {
        public int StatusCode { get; set; }
        public string DisplayMessage { get; set; }
        public List<string> ErrorMessage { get; set; }
        public T Result { get; set; }
    }
}
