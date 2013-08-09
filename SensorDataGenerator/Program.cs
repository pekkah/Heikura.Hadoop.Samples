namespace SensorDataGenerator
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Xml;

    public class Program
    {
        private static readonly Random Random = new Random();

        public static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                return;
            }

            // first agument is the sample count
            var sampleCount = int.Parse(args[0]);

            // second argument is the output path
            var outputDirectory = args[1];

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            for (int i = 0; i < sampleCount; i++)
            {
                Console.WriteLine("Generating sample: {0}", i);
                GenerateSample(i, outputDirectory);
            }

            Console.WriteLine("Done!");
        }

        private static void GenerateSample(int sampleNumber, string outputDirectory)
        {
            var filePath = Path.Combine(
                outputDirectory, string.Concat("sensors", sampleNumber.ToString(CultureInfo.InvariantCulture), ".xml"));

            using (var fileStream = new FileStream(filePath, FileMode.CreateNew))
            {
                using (var writer = new XmlTextWriter(fileStream, Encoding.ASCII))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("sensors");

                    writer.WriteStartElement("sensor");
                    writer.WriteAttributeString("name", "temperature");
                    writer.WriteValue((Random.NextDouble() - 0.2d) * 50d);
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    writer.WriteEndDocument();

                    writer.Flush();
                }
            }
        }
    }
}