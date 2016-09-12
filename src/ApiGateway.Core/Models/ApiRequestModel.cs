using System;

namespace ApiGateway.Core.Models
{
    public class ApiRequestModel
    {
        public string ApiName { get; set; }
        public string AccessKey { get; set; }
        public string Body { get; set; }
        public DateTime Timestamp { get; set; }
        public string Sign { get; set; }
    }
}
