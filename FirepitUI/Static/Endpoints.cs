using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitUI.Static
{
    public static class Endpoints
    {
        public static string BaseUrl = "https://localhost:44358/";
        public static string PersonEndpoint = $"{BaseUrl}api/person/";
        public static string BeverageEndpoint = $"{BaseUrl}api/beverages/";
        public static string MeetingEndpoint = $"{BaseUrl}api/meeting/";
        public static string QuoteEndpoint = $"{BaseUrl}api/quote/";
        public static string LoginEndpoint = $"{BaseUrl}api/user/login/";
        public static string RegisterEndpoint = $"{BaseUrl}api/user/register/";

        public static string ChatEndpoint = $"{BaseUrl}api/chat/getchats/";

        public static string AllBevTypesEndpoint = $"{BaseUrl}api/beverages/allbeveragetypes";
        public static string BevTypeEndpoint = $"{BaseUrl}api/beverages/beveragetype/";

        public static string UpdateUserMeetingEndpoint = $"{BaseUrl}api/meeting/updateusermeeting/";
    }
}
