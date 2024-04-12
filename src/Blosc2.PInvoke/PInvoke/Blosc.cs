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
                Blosc.blosc2_init();
            }
            finally
            {
                if (changedCurrentDir)
                    Directory.SetCurrentDirectory(prevCurrentDir);
            }
        }

        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void blosc2_init();
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void blosc2_destroy();
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc1_compress(int clevel, int doshuffle, ulong typesize,
                                         ulong nbytes, IntPtr src, IntPtr dest,
                                         ulong destsize);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc1_decompress(IntPtr src, IntPtr dest, ulong destsize);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc1_getitem(IntPtr src, int start, int nitems, IntPtr dest);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_getitem(IntPtr src, int srcsize, int start, int nitems,
                                        IntPtr dest, int destsize);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void blosc2_set_threads_callback(blosc_threads_callback callback, IntPtr callback_data);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern short blosc2_get_nthreads();
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern short blosc2_set_nthreads(short nthreads);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern IntPtr blosc1_get_compressor();
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc1_set_compressor([MarshalAs(UnmanagedType.LPStr)] string compname);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void blosc2_set_delta(int dodelta);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_compcode_to_compname(CompressorCodes compcode, ref IntPtr compname);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_compname_to_compcode([MarshalAs(UnmanagedType.LPStr)] string compname);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern IntPtr blosc2_list_compressors();
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern IntPtr blosc2_get_version_string();
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_get_complib_info([MarshalAs(UnmanagedType.LPStr)] string compname, ref IntPtr complib,
                                                 ref IntPtr version);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_free_resources();
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void blosc1_cbuffer_sizes(IntPtr cbuffer, out ulong nbytes,
                                               out ulong cbytes, out ulong blocksize);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_cbuffer_sizes(IntPtr cbuffer, out int nbytes,
                                              out int cbytes, out int blocksize);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc1_cbuffer_validate(IntPtr cbuffer, ulong cbytes,
                                                 out ulong nbytes);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void blosc1_cbuffer_metainfo(IntPtr cbuffer, out ulong typesize,
                                                  IntPtr flags);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void blosc2_cbuffer_versions(IntPtr cbuffer, IntPtr version,
                                                  IntPtr versionlz);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern IntPtr blosc2_cbuffer_complib(IntPtr cbuffer);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_register_io_cb(IntPtr io);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern IntPtr blosc2_get_io_cb(byte id);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_register_tuner(IntPtr tuner);
        
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
        public static extern int blosc2_ctx_get_cparams(IntPtr ctx, IntPtr cparams);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_ctx_get_dparams(IntPtr ctx, IntPtr dparams);
        
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
        public static extern int blosc2_chunk_zeros(blosc2_cparams cparams, int nbytes,
                                            IntPtr dest, int destsize);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_chunk_nans(blosc2_cparams cparams, int nbytes,
                                           IntPtr dest, int destsize);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_chunk_repeatval(blosc2_cparams cparams, int nbytes,
                                                IntPtr dest, int destsize, IntPtr repeatval);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_chunk_uninit(blosc2_cparams cparams, int nbytes,
                                             IntPtr dest, int destsize);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_getitem_ctx(IntPtr context, IntPtr src,
                                            int srcsize, int start, int nitems, IntPtr dest,
                                            int destsize);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern IntPtr blosc2_schunk_new(IntPtr storage);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern IntPtr blosc2_schunk_copy(IntPtr schunk, IntPtr storage);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern IntPtr blosc2_schunk_from_buffer(IntPtr cframe, long len, bool copy);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void blosc2_schunk_avoid_cframe_free(IntPtr schunk, bool avoid_cframe_free);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern IntPtr blosc2_schunk_open([MarshalAs(UnmanagedType.LPStr)] string urlpath);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern IntPtr blosc2_schunk_open_offset([MarshalAs(UnmanagedType.LPStr)] string urlpath, long offset);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern IntPtr blosc2_schunk_open_udio([MarshalAs(UnmanagedType.LPStr)] string urlpath, IntPtr udio);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern long blosc2_schunk_to_buffer(IntPtr schunk, out IntPtr cframe, IntPtr needs_free);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern long blosc2_schunk_to_file(IntPtr schunk, [MarshalAs(UnmanagedType.LPStr)] string urlpath);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern long blosc2_schunk_append_file(IntPtr schunk, [MarshalAs(UnmanagedType.LPStr)] string urlpath);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_schunk_free(IntPtr schunk);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern long blosc2_schunk_append_chunk(IntPtr schunk, IntPtr chunk, bool copy);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern long blosc2_schunk_update_chunk(IntPtr schunk, long nchunk, IntPtr chunk, bool copy);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern long blosc2_schunk_insert_chunk(IntPtr schunk, long nchunk, IntPtr chunk, bool copy);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern long blosc2_schunk_delete_chunk(IntPtr schunk, long nchunk);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern long blosc2_schunk_append_buffer(IntPtr schunk, IntPtr src, int nbytes);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_schunk_decompress_chunk(IntPtr schunk, long nchunk, IntPtr dest, int nbytes);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_schunk_get_chunk(IntPtr schunk, long nchunk, out IntPtr chunk,
                                                 IntPtr needs_free);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_schunk_get_lazychunk(IntPtr schunk, long nchunk, out IntPtr chunk,
                                                     IntPtr needs_free);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_schunk_get_slice_buffer(IntPtr schunk, long start, long stop, IntPtr buffer);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_schunk_set_slice_buffer(IntPtr schunk, long start, long stop, IntPtr buffer);
        
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
        public static extern long blosc2_schunk_frame_len(IntPtr schunk);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern long blosc2_schunk_fill_special(IntPtr schunk, long nitems,
                                                        int special_value, int chunksize);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_meta_add(IntPtr schunk, IntPtr name, IntPtr content,
                                         int content_len);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_meta_update(IntPtr schunk, IntPtr name, IntPtr content,
                                            int content_len);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_vlmeta_exists(IntPtr schunk, IntPtr name);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_vlmeta_add(IntPtr schunk, IntPtr name,
                                           IntPtr content, int content_len,
                                           IntPtr cparams);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_vlmeta_update(IntPtr schunk, IntPtr name,
                                              IntPtr content, int content_len,
                                              IntPtr cparams);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_vlmeta_get(IntPtr schunk, IntPtr name,
                                           out IntPtr content, IntPtr content_len);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_vlmeta_delete(IntPtr schunk, IntPtr name);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_vlmeta_get_names(IntPtr schunk, out IntPtr names);
        
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
        public static extern int blosc1_get_blocksize();
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void blosc1_set_blocksize(ulong blocksize);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void blosc1_set_splitmode(int splitmode);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern IntPtr blosc2_frame_get_offsets(IntPtr schunk);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_register_codec(IntPtr codec);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_register_filter(IntPtr filter);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_remove_dir(IntPtr path);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_remove_urlpath(IntPtr path);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_rename_urlpath([MarshalAs(UnmanagedType.LPStr)] string old_urlpath, [MarshalAs(UnmanagedType.LPStr)] string new_path);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void blosc2_unidim_to_multidim(byte ndim, IntPtr shape, long i, IntPtr index);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern void blosc2_multidim_to_unidim(IntPtr index, sbyte ndim, IntPtr strides, IntPtr i);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(BloscConstants.NATIVE_DLL_NAME)]
        public static extern int blosc2_get_slice_nchunks(IntPtr schunk, IntPtr start, IntPtr stop, out IntPtr chunks_idx);
        
            }
}