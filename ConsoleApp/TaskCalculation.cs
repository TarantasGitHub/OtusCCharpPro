using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class TaskCalculation : BaseCalculation
    {
        private readonly int _pageSize;
        public TaskCalculation(int pageSize = 100)
        {
            this._pageSize = pageSize;
        }

        protected override async Task<Int64> CalculateSum(IEnumerable<int> ints)
        {
            List<Task<Int64>> tasks = new List<Task<Int64>>();

            for (int i = 0; i < ints.Count(); i += _pageSize)
            {
                int from = i;
                int to = i + _pageSize;
                if (to > ints.Count())
                { to = ints.Count(); }

                tasks.Add(Task.Run<Int64>(() => PartCalculation(ints, from, to)));
            }

            await Task.WhenAll(tasks);
            return tasks.Select(t => t.Result).Sum();
        }

        private Int64 PartCalculation(IEnumerable<int> ints, int from, int to)
        {
            return ints.GetSum(from, to);
        }
    }
}
