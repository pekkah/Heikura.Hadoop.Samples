namespace MapReduce.Xml
{
    using System;

    using Microsoft.Hadoop.MapReduce;

    public class MapReduceXmlJob : HadoopJob<MapSensorsXml, MinMaxAvgDoubleReducer>
    {
        private readonly string _inputPath;

        private readonly string _outputPath;

        public MapReduceXmlJob(string inputPath, string outputPath)
        {
            /* design flaw in the Microsoft Hadoop SDK?
             * 
             * Arguments for the job should be read from the context - 
             * not as constructor parameters.
             * 
             * Now the reflection code in the job executor is passing
             * arguments to the Activator.CreateInstance too :(
             * 
             * ***************************************************/
            _inputPath = inputPath;
            _outputPath = outputPath;
        }

        public override HadoopJobConfiguration Configure(ExecutorContext context)
        {
            var configuration = new HadoopJobConfiguration();

            if (context.Arguments.Length != 2)
            {
                throw new ArgumentOutOfRangeException("context", "Arguments length should be 2");
            }

            // arguments are read from the context not from the constructor parameters
            var inputPath = context.Arguments[0];
            var outputPath = context.Arguments[1];

            // enable XML record processing when streaming
            configuration.AdditionalStreamingArguments.Add(
                "-inputreader \"StreamXmlRecord,begin=message,end=/message\"");

            configuration.InputPath = inputPath;
            configuration.OutputFolder = outputPath;
            configuration.DeleteOutputFolder = true; // DANGER!!

            return configuration;
        }
    }
}