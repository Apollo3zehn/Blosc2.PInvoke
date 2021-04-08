using System;

namespace Blosc2.PInvoke
{
    public static class BloscConstants
    {
        public const int BLOSC2_MAX_FILTERS = 6;

        public const string NATIVE_DLL_NAME = "blosc2";

        public const string WindowsDLLPath = @"runtimes\win-x{0}\native";

        public static blosc2_cparams BLOSC2_CPARAMS_DEFAULTS = new blosc2_cparams()
        {
            compcode = CompressorCodes.BLOSC_BLOSCLZ,
            clevel = 5,
            use_dict = 0,
            typesize = 8,
            nthreads = 1,
            blocksize = 0,
            schunk = IntPtr.Zero,
            filters = new byte[] { 0, 0, 0, 0, 0, (byte)CompressorCodec.BLOSC_SHUFFLE },
            filters_meta = new byte[] { 0, 0, 0, 0, 0, 0 },
            prefilter = null,
            pparams = IntPtr.Zero
        };
    }
}