using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ApiResponse
    {
        public int StatusCode { get; } // trả về trạng thái code là gì vd 200 ,400 ,404, 400 ...

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; } // mesage hiển thị lỗi gì

        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            // nếu ta không chuyền mesage vào thì cứ theo mã lỗi chyền vào thì nó có các mesage mặc định ở dưới
            switch (statusCode)
            {
                case 404:
                    return "Resource not found";

                case 500:
                    return "An unhandled error occurred";

                default:
                    return null;
            }
        }
    }
}
