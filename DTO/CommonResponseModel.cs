using System.Collections.Generic;

namespace DTO
{
    public class CommonResponseModel<T>
    {
        public int Code { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public IDictionary<string, string>? ModelStateError { get; set; }
    }

    public class CommonTopupResponseModel<T>
    {
        public string Status { get; set; }
        public int Code { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public IDictionary<string, string>? ModelStateError { get; set; }
    }
}
