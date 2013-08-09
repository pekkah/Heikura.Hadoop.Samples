namespace MapReduce.Xml
{
    using System;
    using System.Linq;
    using System.Xml.Linq;

    using Microsoft.Hadoop.MapReduce;

    public class MapSensorsXml : MapperBase
    {
        public override void Map(string inputLine, MapperContext context)
        {
            try
            {
                var doc = XDocument.Parse(inputLine);

                var sensors =
                    doc.Descendants("sensor")
                       .Select(
                           element => new
                                          {
                                              name = (string)element.Attribute("name"),
                                              value = element.Value
                                          });

                foreach (var sensor in sensors)
                {
                    context.EmitKeyValue(sensor.name, sensor.value);
                }
            }
            catch (Exception x)
            {
                context.EmitLine(x.ToString());
            }
        }
    }
}