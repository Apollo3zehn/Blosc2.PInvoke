using System;

namespace Blosc2.PInvoke
{
    public delegate int blosc2_prefilter_fn(ref blosc2_prefilter_params @params);
    public delegate int blosc_threads_callback(IntPtr callback_data, IntPtr dojob, int numjobs, UIntPtr jobdata_elsize, IntPtr jobdata);

    public struct blosc2_dparams
    {
        public int nthreads;
        //!< The number of threads to use internally (1).

        public IntPtr schunk;
        //!< The associated schunk, if any (NULL).
    }

    public struct blosc2_prefilter_params
    {
        public IntPtr user_data;  // user-provided info (optional)
        public IntPtr @out;  // the output buffer
        public int out_size;  // the output size (in bytes)
        public int out_typesize;  // the output typesize
        public int out_offset; // offset to reach the start of the output buffer
        public int tid;  // thread id
        public IntPtr ttmp;  // a temporary that is able to hold several blocks for the output and is private for each thread
        public UIntPtr ttmp_nbytes;  // the size of the temporary in bytes
        public IntPtr ctx;  // the decompression context
    }

    public struct blosc2_cparams
    {
        public CompressorCodes compcode;
        //!< The compressor codec.

        public byte clevel;
        //!< The compression level (5).

        public int use_dict;
        //!< Use dicts or not when compressing (only for ZSTD).

        public byte typesize;
        //!< The type size (8).

        public short nthreads;
        //!< The number of threads to use internally (1).

        public int blocksize;
        //!< The requested size of the compressed blocks (0; meaning automatic).

        public IntPtr schunk;
        //!< The associated schunk, if any (NULL).

        public byte[] filters;
        //!< The (sequence of) filters.

        public byte[] filters_meta;
        //!< The metadata for filters.

        public blosc2_prefilter_fn prefilter;
        //!< The prefilter function.

        public IntPtr pparams;
        //!< The prefilter parameters.
    }

    public enum CompressorCodec : byte
    { 
        BLOSC_NOSHUFFLE = 0,   //!< no shuffle (for compatibility with Blosc1)
        BLOSC_NOFILTER = 0,    //!< no filter
        BLOSC_SHUFFLE = 1,     //!< byte-wise shuffle
        BLOSC_BITSHUFFLE = 2,  //!< bit-wise shuffle
        BLOSC_DELTA = 3,       //!< delta filter
        BLOSC_TRUNC_PREC = 4,  //!< truncate precision filter
        BLOSC_LAST_FILTER = 5,  //!< sentinel
    };

    public enum CompressorCodes : byte
    {
        BLOSC_BLOSCLZ = 0,
        BLOSC_LZ4 = 1,
        BLOSC_LZ4HC = 2,
        BLOSC_SNAPPY = 3,
        BLOSC_ZLIB = 4,
        BLOSC_ZSTD = 5,
        BLOSC_LIZARD = 6,
        BLOSC_MAX_CODECS = 7,  //!< maximum number of reserved codecs
    }
}