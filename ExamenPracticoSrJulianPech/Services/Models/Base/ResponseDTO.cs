using System.Collections.Generic;

namespace Services.Models.Base
{
    public class ResponseDTO<T> : BaseResponse
    {
        public List<T> ListData { get; set; }

        public T Data { get; set; }
    }
}
