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

            _bank = raw
                .Select(Parse)
                .SelectMany(task =>
                {
                    var tArr = task.Types.Length == 0 ? Enum.GetValues<AphasiaType>() : task.Types;
                    var lArr = task.Levels.Length == 0 ? Enum.GetValues<Severity>() : task.Levels;

                    return from t in tArr
                           from l in lArr
                           select new { t, l, task };
                })
                .GroupBy(x => (x.t, x.l))
                .ToDictionary(g => g.Key, g => g.Select(x => x.task).ToList());

        }

        public List<TaskBase> ListFor(AphasiaType t, Severity v) => _bank[(t, v)];

        private static TaskBase Parse(JsonElement e)
        {
            var types = e.GetIntArray("types")
                          .Select(i => (AphasiaType)i).ToArray();
            var levels = e.GetIntArray("levels")
                          .Select(i => (Severity)i).ToArray();

            return e.Get("kind") switch
            {
                "CompleteRow" => new CompleteRowTask(
                    e.Get("id"), e.Get("description"),
                    e.GetStringArray("rowImages"),
                    e.Get("correctImage"),
                    e.GetStringArray("distractorImages"),
                    types, levels),

                "FindOdd" => new FindOddTask(
                    e.Get("id"), e.Get("description"),
                    e.GetStringArray("images"),
                    e.GetProperty("oddIndex").GetInt32(),
                    types, levels),

                _ => throw new NotSupportedException($"unknown kind {e.Get("kind")}")
            };
        }
    }
}
