namespace MapReduce.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Microsoft.Hadoop.MapReduce;

    public class MinMaxAvgDoubleReducer : ReducerCombinerBase
    {
        public override void Reduce(string key, IEnumerable<string> values, ReducerCombinerContext context)
        {
            try
            {
                var doubleValues = values.Select(double.Parse).ToList();

                var max = doubleValues.Max();
                var min = doubleValues.Min();
                var avg = doubleValues.Average();

                context.EmitKeyValue(string.Format("{0}_max", key), max.ToString(CultureInfo.InvariantCulture));
                context.EmitKeyValue(string.Format("{0}_min", key), min.ToString(CultureInfo.InvariantCulture));
                context.EmitKeyValue(string.Format("{0}_avg", key), avg.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception x)
            {
                context.EmitLine(x.ToString());
            }
        }
    }
}