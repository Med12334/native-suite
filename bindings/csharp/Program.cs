using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace NativeSuite
{
    public sealed class SafeEngineHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private SafeEngineHandle() : base(ownsHandle: true) { }

        protected override bool ReleaseHandle()
        {
            Console.WriteLine("[SafeHandle] Disposing native engine unmanaged memory...");
            NativeMethods.engine_destroy(handle);
            return true;
        }
    }

    internal static unsafe class NativeMethods
    {
        private const string LibName = "../../build/libnative_core.so";

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int engine_get_version();

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SafeEngineHandle engine_create();

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void engine_destroy(IntPtr engine);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong engine_hash_buffer(SafeEngineHandle engine, byte* data, nuint len);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong engine_get_metrics(SafeEngineHandle engine);
    }

    public sealed class NativeEngine : IDisposable
    {
        private readonly SafeEngineHandle _handle;

        public NativeEngine()
        {
            _handle = NativeMethods.engine_create();
            if (_handle.IsInvalid)
                throw new InvalidOperationException("Failed to allocate native engine.");
        }

        public static int Version => NativeMethods.engine_get_version();

        public unsafe ulong ComputeHash(ReadOnlySpan<byte> buffer)
        {
            if (_handle.IsClosed || _handle.IsInvalid)
                throw new ObjectDisposedException(nameof(NativeEngine));

            // Pin buffer in memory and pass raw byte* pointer (Zero Allocations!)
            fixed (byte* ptr = buffer)
            {
                return NativeMethods.engine_hash_buffer(_handle, ptr, (nuint)buffer.Length);
            }
        }

        public ulong TotalBytesProcessed => NativeMethods.engine_get_metrics(_handle);

        public void Dispose()
        {
            _handle.Dispose();
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine($"[C#] Engine Architecture v{NativeEngine.Version / 100}.0 Initialized");

            using (var engine = new NativeEngine())
            {
                string text = "High-performance zero-copy interop pipeline";
                byte[] bytes = Encoding.UTF8.GetBytes(text);

                // Execution 1
                ulong hash1 = engine.ComputeHash(bytes);
                Console.WriteLine($"[C#] Payload 1 (Hash: 0x{hash1:X16})");

                // Execution 2
                byte[] extra = Encoding.UTF8.GetBytes("Another native block");
                ulong hash2 = engine.ComputeHash(extra);
                Console.WriteLine($"[C#] Payload 2 (Hash: 0x{hash2:X16})");

                // Query C++ metrics state
                Console.WriteLine($"[C#] Total unmanaged bytes processed by C++: {engine.TotalBytesProcessed} bytes");
            }

            Console.WriteLine("[C#] Pipeline completed cleanly.");
        }
    }
}
