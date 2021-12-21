using System.Collections.Generic;

namespace PaymentService.Domain.Models
{
    public class ServiceInfo
    {
        public string Name { get; set; }
        public IEnumerable<string> Methods { get; set; }
        public double Cost { get; set; }
        public int Duration { get; set; }
        public string Currency { get; set; }
        public string Url { get; set; }
        public string Key_method { get; set; }
    }
}