namespace MapReduce.Xml
{
    using System;
    using System.IO;

    using Microsoft.Hadoop.MapReduce;
    using Microsoft.Hadoop.WebClient.WebHCatClient;

    using YamlDotNet.RepresentationModel.Serialization;

    public class Program
    {
        public static void Main(string[] args)
        {
            // bug in the SDK requires these even though we're posting the job to HDInsight
            Environment.SetEnvironmentVariable("HADOOP_HOME", @"c:\hadoop");
            Environment.SetEnvironmentVariable("Java_HOME", @"c:\hadoop\jvm");

            var configfile = args.Length == 1 ? args[0] : "default.yaml";

            if (!File.Exists(configfile))
            {
                Console.WriteLine("Configuration file does not exists.");
                return;
            }

            Configuration config = null;

            var serializer = new Deserializer();

            using (var reader = new StreamReader(configfile))
            {
                config = serializer.Deserialize<Configuration>(reader);
            }

            if (config == null)
            {
                Console.WriteLine("Could not read configuration");
                return;
            }

            var hadoop = Hadoop.Connect(
                new Uri(config.ClusterUrl),
                config.ClusterUserName,
                config.HadoopUserName,
                config.ClusterPassword,
                config.BlobStorageUrl,
                config.BlobStorageKey,
                config.BlobContainerName,
                config.CreateContainerIfMissing);

            try
            {
                MapReduceResult result = hadoop.MapReduceJob.ExecuteJob(
                    typeof(MapReduceXmlJob), new[] { config.InputPath, config.OutputPath });

                using (var output = new StreamWriter("output.txt", false))
                {
                    output.WriteLine("Exit code");
                    output.WriteLine(result.Info.ExitCode);
                    output.WriteLine("stdout");
                    output.WriteLine(result.Info.StandardOut);
                    output.WriteLine("stderr");
                    output.WriteLine(result.Info.StandardError);
                    output.Flush();
                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }
    }
}