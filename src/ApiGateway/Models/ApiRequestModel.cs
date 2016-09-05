using System;

namespace ApiGateway.Models
{
    public class ApiRequestModel
    {
        public string AccessKey { get; set; }
        public string ApiName { get; set; }
        public DateTime Timestamp { get; set; }
        public string Body { get; set; }
        public string Sign { get; set; }
    }
}
