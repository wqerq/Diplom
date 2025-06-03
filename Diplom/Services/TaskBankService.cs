using Diplom.Helpers;
using Diplom.Models;
using Diplom.Models.Enums;
using Diplom.Models.TaskModels;
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
                .Where(task => task is not null)
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
            var types = e.GetIntArray("types").Select(i => (AphasiaType)i).ToArray();
            var levels = e.GetIntArray("levels").Select(i => (Severity)i).ToArray();

            return e.Get("kind") switch
            {
                "CompleteRow" => new CompleteRowTask(
                    id: e.Get("id"),
                    description: e.Get("description"),
                    row: e.GetStringArray("row"),
                    variants: e.GetStringArray("variants"),
                    correct: e.Get("correct"),
                    t: types, s: levels),

                "FindOdd" => new FindOddTask(
                    Id: e.Get("id"),
                    Description: e.Get("description"),
                    Images: e.GetStringArray("images"),
                    OddIndex: e.GetProperty("oddIndex").GetInt32(),
                    Types: types, Levels: levels),

                "Category" => new CategoryTask(
                    id: e.Get("id"),
                    description: e.Get("description"),
                    category: e.Get("category"),
                    images: e.GetStringArray("images"),
                    answers: e.GetStringArray("answers"),
                    t: types, s: levels),
                "YesNo" => new YesNoTask(
                    Id: e.Get("id"),
                    Description: e.Get("description"),
                    Question: e.Get("question"),
                    Image: e.Get("image"),
                    Answer: e.GetProperty("answer").GetBoolean(),
                    Types: types,
                    Levels: levels),
                "SentenceComplete" => new SentenceCompleteTask(
                    Id: e.Get("id"),
                    Description: e.Get("description"),
                    Sentence: e.Get("sentence"),
                    Opt1: e.Get("opt1"),
                    Opt2: e.Get("opt2"),
                    Opt3: e.Get("opt3"),
                    Answer: e.Get("answer"),
                    Types: types,
                    Levels: levels),
                "Audio" => new AudioTask(
                    Id: e.Get("id"),
                    Description: e.Get("description"),
                    Text: e.Get("text"),
                    Types: types,
                    Levels: levels
                    ),

                _ => throw new NotSupportedException()
            };
        }
    }
}
