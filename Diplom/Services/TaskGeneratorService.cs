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

        public TaskBase Next(AphasiaType t, Severity s)
        {
            var key = (t, s);

            if (!_cache.TryGetValue(key, out var q) || q.Count == 0)
            {
                var shuffled = _bank.ListFor(t, s)
                                    .OrderBy(_ => Random.Shared.Next())
                                    .ToList();
                q = new Queue<TaskBase>(shuffled);
                _cache[key] = q;
            }

            return q.Dequeue();
        }
    }

}
