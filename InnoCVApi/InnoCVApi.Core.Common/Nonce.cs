using System;
using System.Globalization;

namespace InnoCVApi.Core.Common
{
    public class Nonce
    {
        public static string NewNonce()
        {
            return Guid.NewGuid().ToString(null, CultureInfo.InvariantCulture);
        }
    }
}