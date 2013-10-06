using System;
using System.Runtime.InteropServices;

namespace Helpers
{
    /// <summary>A collection of useful methods about netwotk tasks</summary>
    public static class NetWorkHelper
    {
        [DllImport("wininet.dll")]
        extern static bool InternetGetConnectedState(out ConnectionStates lpdwFlags, int dwReserved);

        /// <summary>Show if internet collection is available</summary>
        public static bool IsInternetAvailable
        {
            get
            {
                ConnectionStates lpdwFlags;
                return InternetGetConnectedState(out lpdwFlags, 0);
            }
        }

        /// <summary>
        /// Get detailed information abut internet connection
        /// </summary>
        /// <returns>Current connection state (set of flags)</returns>
        public static ConnectionStates GetInternetConnectedState()
        {
            ConnectionStates flags;
            InternetGetConnectedState(out flags, 0);

            return flags;
        }
    }
}
