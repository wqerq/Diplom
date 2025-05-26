using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Diplom.Helpers
{
    public static class JsonEx
    {
        public static string Get(this JsonElement e, string prop) =>
        e.TryGetProperty(prop, out var p) ? p.GetString() ?? "" : "";

        public static string[] GetStringArray(this JsonElement e, string prop) =>
            e.TryGetProperty(prop, out var p) && p.ValueKind == JsonValueKind.Array
                ? p.EnumerateArray().Select(x => x.GetString() ?? "").ToArray()
                : Array.Empty<string>();
        public static int[] GetIntArray(this JsonElement e, string prop) =>
            e.TryGetProperty(prop, out var p) && p.ValueKind == JsonValueKind.Array
                ? p.EnumerateArray().Select(x => x.GetInt32()).ToArray()
                : Array.Empty<int>();
    }
}

