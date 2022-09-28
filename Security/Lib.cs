// See the LICENSE file in the project root for more information.
using System.Runtime.InteropServices;

namespace CTFChallenge.Security
{
    public static class Lib
    {
        [DllExport(CallingConvention = CallingConvention.StdCall)]
        public static int Add(int a, int b)
        {
            return a + b;
        }
    }
}
