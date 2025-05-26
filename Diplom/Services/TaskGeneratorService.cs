using Diplom.Models;
using Diplom.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Services
{
    public sealed class TaskGeneratorService
    {
        private readonly TaskBankService _bank;
        private readonly Dictionary<(AphasiaType, Severity), Queue<TaskBase>> _cache = [];

        public TaskGeneratorService(TaskBankService bank) => _bank = bank;

        public TaskBase Next(AphasiaType type, Severity level)
        {
            var list = _bank.ListFor(type, level);

            if (list.Count == 0)
                throw new InvalidOperationException(
                    $"Для {type}/{level} нет ни одного упражнения (проверьте tasks.json)");

            var key = (type, level);
            if (!_cache.TryGetValue(key, out var q) || q.Count == 0)
            {
                q = new Queue<TaskBase>(
                        list.OrderBy(_ => Random.Shared.Next()));
                _cache[key] = q;
            }

            return q.Dequeue();
        }


    }

}
