using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Burst;

#if !NET_DOTS
using System.Reflection;
#endif

namespace Unity.Collections
{
    internal static class CollectionHelper
    {
        [StructLayout(LayoutKind.Explicit)]
        internal struct LongDoubleUnion
        {
            [FieldOffset(0)]
            public long longValue;
            [FieldOffset(0)]
            public double doubleValue;
        }

        public static int lzcnt(uint x)
        {
            if (x == 0)
                return 32;
            LongDoubleUnion u;
            u.doubleValue = 0.0;
            u.longValue = 0x4330000000000000L + x;
            u.doubleValue -= 4503599627370496.0;
            return 0x41E - (int)(u.longValue >> 52);
        }

        public static int log2_floor(int value)
        {
            return 31 - lzcnt((uint)value);
        }

        /// <summary>
        /// Returns the smallest power of two that is greater than or equal to the given integer
        /// </summary>
        public static int CeilPow2(int i)
        {
            i -= 1;
            i |= i >> 1;
            i |= i >> 2;
            i |= i >> 4;
            i |= i >> 8;
            i |= i >> 16;
            return i + 1;
        }

        /// <summary>
        /// Returns hash value of memory block. Function is using djb2 (non-cryptographic hash).
        /// </summary>
        public unsafe static uint hash(void *pointer, int bytes)
        {
            // djb2 - Dan Bernstein hash function
            // http://web.archive.org/web/20190508211657/http://www.cse.yorku.ca/~oz/hash.html
            byte* str = (byte*) pointer;
            ulong hash = 5381;
            while(bytes > 0)
            {
                ulong c = str[--bytes];
                hash = ((hash << 5) + hash) + c;
            }
            return (uint)hash;
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        [BurstDiscard]
        public static void CheckIsUnmanaged<T>()
        {
            if (!UnsafeUtilityEx.IsUnmanaged<T>())
                throw new ArgumentException($"{typeof(T)} used in native collection is not blittable or primitive");
        }

        internal static void WriteLayout(Type type)
        {
#if !NET_DOTS
            Console.WriteLine("   Offset | Bytes  | Name     Layout: {0}", type.Name);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                Console.WriteLine("   {0, 6} | {1, 6} | {2}"
                    , Marshal.OffsetOf(type, field.Name)
                    , Marshal.SizeOf(field.FieldType)
                    , field.Name
                    );
            }
#else
            _ = type;
#endif
        }
    }
}
