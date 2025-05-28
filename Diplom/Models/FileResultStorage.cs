using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public sealed class FileResultStorage : IResultStorage
    {
        const string FileName = "results.json";

        string FullPath => Path.Combine(FileSystem.AppDataDirectory, FileName);

        public IReadOnlyList<TestResult> LoadAll()
        {
            if (!File.Exists(FullPath)) return [];
            var json = File.ReadAllText(FullPath);
            return JsonSerializer.Deserialize<List<TestResult>>(json) ?? [];
        }

        public void Save(TestResult r)
        {
            var list = LoadAll().ToList();
            list.Add(r);
            var json = JsonSerializer.Serialize(list, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(FullPath, json);
        }
    }
}
