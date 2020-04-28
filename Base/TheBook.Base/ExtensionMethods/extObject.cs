using System;

namespace Pharmazer.Base.ExtensionMethods
{
    public static class extObject
    { 
        public static bool isNull(this object Object) 
        {
            if (Object == null)
            {
                return true;
            }
            else
            {
                return false;
            } 
        }
        public static byte ToByte(this object obj)
        {
            return obj == DBNull.Value ? default(byte) : Convert.ToByte(obj);
        }
        public static int ToInt32(this object obj)
        {
             return obj == DBNull.Value ? 0: Convert.ToInt32(obj);
        }
        public static bool ToBoolean(this object obj)
        {
            return obj == DBNull.Value ? false : Convert.ToBoolean(obj);
        }
        public static DateTime ToDateTime(this object obj)
        {
            return obj == DBNull.Value ? new DateTime() : Convert.ToDateTime(obj);
        }
        public static decimal ToDecimal(this object obj)
        {
            return obj == DBNull.Value ? 0 : Convert.ToDecimal(obj);
        }
        public static double ToDouble(this object obj)
        {
            return obj == DBNull.Value ? 0 : Convert.ToDouble(obj);
        }
        public static byte? ToNullableByte(this object obj)
        {
            return obj == null || obj == DBNull.Value ? new byte?() : Convert.ToByte(obj);
        }
        public static int? ToNullableInt32(this object obj)
        {
            return obj == null || obj == DBNull.Value ? new int?() : Convert.ToInt32(obj);
        }
        public static bool? ToNullableBoolean(this object obj)
        {
            return obj == null || obj == DBNull.Value ? new bool?() : Convert.ToBoolean(obj);
        }
        public static DateTime? ToNullableDateTime(this object obj)
        {
            return obj == null || obj == DBNull.Value ? new DateTime?() : Convert.ToDateTime(obj);
        }
        public static decimal? ToNullableDecimal(this object obj)
        {
            return obj == null || obj == DBNull.Value ? new decimal?() : Convert.ToDecimal(obj);
        }
        public static Double? ToNullableDouble(this object obj)
        {
            return obj == null || obj == DBNull.Value ? new Double?() : Convert.ToDouble(obj);
        }
        public static object ToDBValue(this object obj)
        {
            return obj ?? DBNull.Value;
        }
        public static bool IsDBNull(this object obj)
        {
            return obj == DBNull.Value;
        }
    }
}
