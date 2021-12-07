using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWebApp
{
    public static class GetUrlSD
    {
        public static string APIBaseUrl = "https://localhost:22012/";
        public static string NationalParkAPIPath = APIBaseUrl + "api/v1/nationalparks";
        public static string TrailAPIPath = APIBaseUrl + "api/v1/trails";
    }
}
