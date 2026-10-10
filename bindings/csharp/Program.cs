using System;
using System.Runtime.InteropServices;
using System.Text;

internal static partial class Native
{
    private const string LibName = "../../build/libnative_core.so";

    [DllImport(LibName, EntryPoint = "engine_get_version")]
    public static extern int GetVersion();

    [DllImport(LibName, EntryPoint = "engine_create")]
    public static extern IntPtr CreateEngine();

    [DllImport(LibName, EntryPoint = "engine_destroy")]
    public static extern void DestroyEngine(IntPtr engine);

    [DllImport(LibName, EntryPoint = "engine_compute")]
    public static extern uint Compute(IntPtr engine, byte[] data, nuint len);
}

class Program
{
    static void Main()
    {
        Console.WriteLine("[C#] Calling into C ABI bridge...");
        int version = Native.GetVersion();
        Console.WriteLine($"[C#] Native Engine Version: {version}");

        Console.WriteLine("[C#] Allocating C++ Engine instance...");
        IntPtr engine = Native.CreateEngine();

        if (engine == IntPtr.Zero)
        {
            Console.WriteLine("[C#] Failed to allocate native engine!");
            return;
        }

        byte[] payload = Encoding.UTF8.GetBytes("Hello from C# through C++ down to C!");
        Console.WriteLine($"[C#] Passing {payload.Length} bytes across native boundary...");

        uint checksum = Native.Compute(engine, payload, (nuint)payload.Length);
        Console.WriteLine($"[C#] Checksum calculated by Layer 1 (C): {checksum}");

        Console.WriteLine("[C#] Releasing native memory...");
        Native.DestroyEngine(engine);
        Console.WriteLine("[C#] Pipeline completed successfully.");
    }
}
