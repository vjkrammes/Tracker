using System;
using System.Security.Cryptography;

namespace TrackerCommon
{
    public static class Salter
    {
        public static byte[] CreateSalt(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException("length");
            }
            byte[] ret = new byte[length];
            using var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(ret);
            return ret;
        }
    }
}
