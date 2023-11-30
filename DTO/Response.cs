using System;

namespace DTO
{
    public class Response<T>
    {
        public string ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public T Data { get; set; }
    }
}
