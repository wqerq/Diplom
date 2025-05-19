using Diplom.Helpers;
using Diplom.Models;
using Diplom.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Diplom.Services
{
    public sealed class TaskBankService
    {
        private readonly Dictionary<(AphasiaType, Severity), List<TaskBase>> _bank;

        public TaskBankService()
        {
            using var s = FileSystem.OpenAppPackageFileAsync("Data/tasks.json").Result;
            var raw = JsonSerializer.Deserialize<JsonElement[]>(s)!;

            _bank = raw.Select(Parse)
                       .GroupBy(t => (t.Type, t.Level))
                       .ToDictionary(g => g.Key, g => g.ToList());
        }

        public List<TaskBase> ListFor(AphasiaType t, Severity v) => _bank[(t, v)];

        private static TaskBase Parse(JsonElement e) => e.Get("kind") switch
        {
            "CompleteRow" => new CompleteRowTask(
                e.Get("id"), e.Get("description"),
                e.GetStringArray("rowImages"),
                e.Get("correctImage"),
                e.GetStringArray("distractorImages"),
                (AphasiaType)e.GetProperty("type").GetInt32(),
                (Severity)e.GetProperty("level").GetInt32()),

            "FindOdd" => new FindOddTask(
                e.Get("id"), e.Get("description"),
                e.GetStringArray("images"),
                e.GetProperty("oddIndex").GetInt32(),
                (AphasiaType)e.GetProperty("type").GetInt32(),
                (Severity)e.GetProperty("level").GetInt32()),

            _ => throw new NotSupportedException("unknown kind")
        };
    }
}
