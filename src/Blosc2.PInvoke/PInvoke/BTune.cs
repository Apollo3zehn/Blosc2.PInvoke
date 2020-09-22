using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Blosc2.PInvoke
{
    public class BTune
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void btune_init(IntPtr config, IntPtr cctx, IntPtr dctx);
    }
}