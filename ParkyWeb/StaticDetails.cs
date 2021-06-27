namespace ParkyWeb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public static class StaticDetails
    {
        public const string APIBaseURL = "https://localhost:44365/";
        public const string NationalParkAPIPath = APIBaseURL+"api/v1/nationalparks";
        public const string TrialAPIPath = APIBaseURL+"api/v1/trials";
    }
}
