namespace POC_RavenDb_Docker.DTOs.Responses
{
    public class ResultDto<T>
    {
        public string Id { get; set; }
        public T Data { get; set; }
    }
}
