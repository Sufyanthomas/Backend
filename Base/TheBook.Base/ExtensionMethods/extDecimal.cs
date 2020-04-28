using Pharmazer.Base.Tools;

namespace Pharmazer.Base.ExtensionMethods
{
    public static class extDecimal
    {
        public static string CurrencyFormat(this decimal Value)
        {
            return Utility.CurrencyFormat(Value);
        } 
    }
}
