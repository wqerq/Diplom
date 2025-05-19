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
            e.GetProperty(prop).GetString() ?? "";

        public static string[] GetStringArray(this JsonElement e, string prop) =>
            e.GetProperty(prop).EnumerateArray()
                               .Select(x => x.GetString() ?? "")
                               .ToArray();
    }
}

