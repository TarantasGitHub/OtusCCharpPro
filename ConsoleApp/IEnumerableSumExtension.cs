namespace ConsoleApp
{
    internal static class IEnumerableSumExtension
    {
        public static Int64 GetSum(this IEnumerable<int> ints)
        {
            var sum = 0L;
            foreach (var item in ints)
            {
                sum += item;
            }
            return sum;
        }

        public static Int64 GetSum(this IEnumerable<int> ints, int from, int to)
        {
            //Int64 index = 0;
            var sum = 0L;
            //var sw1 = new Stopwatch();
            //sw1.Start();
            if (ints is List<int> array)
            {
                //var sw = new Stopwatch();
                //sw.Start();
                for (int i = from; i < to; i++)
                {
                    sum += (Int64)array[i];
                }
                //sw.Stop();
                //Console.WriteLine("ints - массив, подсчет занял: {0}", sw.ElapsedMilliseconds);
            }
            else
            {
                //Console.WriteLine("ints - не массив");
                using (IEnumerator<int> e = ints.GetEnumerator())
                {
                    int take = to - from;
                    //var sw2 = new Stopwatch();
                    //sw2.Start();
                    while (from > 0 && e.MoveNext()) from--;
                    //sw2.Stop();
                    //Console.WriteLine("Skip занял: {0}", sw2.ElapsedMilliseconds);
                    if (from <= 0)
                    {
                        //sw2.Restart();
                        while (e.MoveNext() && take > 0)
                        {
                            sum += (Int64)e.Current;
                            take--;
                            //yield return e.Current;
                        }
                        //sw2.Stop();
                        //Console.WriteLine("Take занял: {0}", sw2.ElapsedMilliseconds);
                    }
                }
            }
            //sw1.Stop();
            //Console.WriteLine("GetSum занял: {0}", sw1.ElapsedMilliseconds);
            //foreach (var item in ints)
            //{
            //    if (index < from)
            //    {
            //        continue;
            //    }
            //    else if (index > to)
            //    {
            //        break;
            //    }
            //    else
            //    {
            //        sum += item;
            //    }
            //}
            return sum;
        }
    }
}
