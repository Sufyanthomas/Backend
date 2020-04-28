using System;

namespace Pharmazer.Base.ExtensionMethods
{
    public static class extDateTime
    {
        public static string ShowDate(this DateTime Value)
        {
            return Value.ToString("dd.MM.yyyy") ;
        }
        public static string ShowTime(this DateTime Value)
        {
            return Value.ToString("HH:mm");
        }
        public static string ShowDateTime(this DateTime Value)
        {
            return Value.ToString("dd.MM.yyyy HH:mm");
        }
    } 
}
