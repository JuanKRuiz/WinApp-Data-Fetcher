using System;

namespace Helpers
{
    /// <summary>Network Connection states</summary>
    [Flags]
    public enum ConnectionStates : int
    {
        Modem = 0x1,
        LAN = 0x2,
        Proxy = 0x4,
        RasInstalled = 0x10,
        Offline = 0x20,
        Configured = 0x40,
    }
}
