using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace DeliveryPriceCalculatorAPI.Model
{
    public class PackageDelivery
    {
        public struct location
        {
            public int code { get; set; }
        }

        public struct package
        {
            public int height { get; set; }
            public int length { get; set; }
            public int weight { get; set; }
            public int width { get; set; }
        }

        public location from_location { get; set; }

        public location to_location { get; set; }

        public List<package> packages { get; set; }

    }
}