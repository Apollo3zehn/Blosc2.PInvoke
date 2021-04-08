using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace Blosc2.PInvoke
{
    public static class Blosc
    {
        static Blosc()
        {
            // .NET Framework does not automatically load libraries from the native runtimes folder like .NET Core.
            // Therefore, if running .NET Framework, switch the current directory to the native runtime folder
            // before attempting to load the native HDF5 library.
            bool changedCurrentDir = false;
            var prevCurrentDir = Directory.GetCurrentDirectory();
            if (RuntimeInformation.FrameworkDescription.Contains("Framework"))
            {
                var dllDir = Path.Combine(prevCurrentDir, string.Format(BloscConstants.WindowsDLLPath, Environment.Is64BitProcess ? "64" : "86"));
                if (Directory.Exists(dllDir))
                {
                    Directory.SetCurrentDirectory(dllDir);
                    changedCurrentDir = true;
                }
            }
          
            try
            {
                Blosc.blosc_init();
            }
            finally
            {
                if (changedCurrentDir)
                    Directory.SetCurrentDirectory(prevCurrentDir);
            }
        }

        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void blosc_init();
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void blosc_destroy();
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc_compress(int clevel, int doshuffle, ulong typesize,
                                        ulong nbytes, IntPtr src, IntPtr dest,
                                        ulong destsize);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc_decompress(IntPtr src, IntPtr dest, ulong destsize);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc_getitem(IntPtr src, int start, int nitems, IntPtr dest);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void blosc_set_threads_callback(blosc_threads_callback callback, IntPtr callback_data);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc_get_nthreads();
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc_set_nthreads(int nthreads);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern IntPtr blosc_get_compressor();
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc_set_compressor([MarshalAs(UnmanagedType.LPStr)] string compname);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void blosc_set_delta(int dodelta);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc_compcode_to_compname(CompressorCodes compcode, ref IntPtr compname);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc_compname_to_compcode([MarshalAs(UnmanagedType.LPStr)] string compname);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern IntPtr blosc_list_compressors();
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern IntPtr blosc_get_version_string();
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc_get_complib_info([MarshalAs(UnmanagedType.LPStr)] string compname, ref IntPtr complib,
                                                ref IntPtr version);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc_free_resources();
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void blosc_cbuffer_sizes(IntPtr cbuffer, out ulong nbytes,
                                              out ulong cbytes, out ulong blocksize);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc_cbuffer_validate(IntPtr cbuffer, ulong cbytes,
                                                 out ulong nbytes);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void blosc_cbuffer_metainfo(IntPtr cbuffer, out ulong typesize,
                                                 IntPtr flags);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void blosc_cbuffer_versions(IntPtr cbuffer, IntPtr version,
                                                 IntPtr versionlz);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern IntPtr blosc_cbuffer_complib(IntPtr cbuffer);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern IntPtr blosc2_create_cctx(blosc2_cparams cparams);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern IntPtr blosc2_create_dctx(blosc2_dparams dparams);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void blosc2_free_ctx(IntPtr context);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_set_maskout(IntPtr ctx, IntPtr maskout, int nblocks);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_compress(int clevel, int doshuffle, int typesize,
                                         IntPtr src, int srcsize, IntPtr dest,
                                         int destsize);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_decompress(IntPtr src, int srcsize,
                                           IntPtr dest, int destsize);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_compress_ctx(
                IntPtr context, IntPtr src, int srcsize, IntPtr dest,
                int destsize);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_decompress_ctx(IntPtr context, IntPtr src,
                                               int srcsize, IntPtr dest, int destsize);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_getitem_ctx(IntPtr context, IntPtr src,
                                            int srcsize, int start, int nitems, IntPtr dest);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern IntPtr 
        blosc2_new_schunk(blosc2_cparams cparams, blosc2_dparams dparams, IntPtr frame);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_free_schunk(IntPtr schunk);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_schunk_append_chunk(IntPtr schunk, IntPtr chunk, bool copy);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_schunk_append_buffer(IntPtr schunk, IntPtr src, int nbytes);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_schunk_decompress_chunk(IntPtr schunk, int nchunk, IntPtr dest, int nbytes);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_schunk_get_chunk(IntPtr schunk, int nchunk, out IntPtr chunk,
                                                 IntPtr needs_free);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_schunk_get_cparams(IntPtr schunk, out IntPtr cparams);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_schunk_get_dparams(IntPtr schunk, out IntPtr dparams);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_schunk_reorder_offsets(IntPtr schunk, IntPtr offsets_order);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_has_metalayer(IntPtr schunk, IntPtr name);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_add_metalayer(IntPtr schunk, IntPtr name, IntPtr content,
                                              uint content_len);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_update_metalayer(IntPtr schunk, IntPtr name, IntPtr content,
                                                 uint content_len);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_get_metalayer(IntPtr schunk, IntPtr name, out IntPtr content,
                                              IntPtr content_len);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_update_usermeta(IntPtr schunk, IntPtr content,
                                                int content_len, blosc2_cparams cparams);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_get_usermeta(IntPtr schunk, out IntPtr content);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern IntPtr blosc2_new_frame([MarshalAs(UnmanagedType.LPStr)] string fname);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern long blosc2_schunk_to_frame(IntPtr schunk, IntPtr frame);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_free_frame(IntPtr frame);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern long blosc2_frame_to_file(IntPtr frame, IntPtr fname);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern IntPtr blosc2_frame_from_file(IntPtr fname);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern IntPtr blosc2_frame_from_sframe(IntPtr sframe, long len, bool copy);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern IntPtr blosc2_schunk_from_frame(IntPtr frame, bool copy);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void blosc_set_timestamp(IntPtr timestamp);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern double blosc_elapsed_nsecs(IntPtr start_time,
                                                IntPtr end_time);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern double blosc_elapsed_secs(IntPtr start_time,
                                               IntPtr end_time);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc_get_blocksize();
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void blosc_set_blocksize(ulong blocksize);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void blosc_set_schunk(IntPtr schunk);
        
            }
}