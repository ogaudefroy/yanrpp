namespace YANRPP
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Xml;
    using System.Collections.Generic;
    using NewRelic.Platform.Sdk.Utils;

    public static class CustomMetricsBuilder
    {
        private static Logger _logger = Logger.GetLogger("CustomMetricsBuilder");

        public static IEnumerable<CustomMetric> BuildFromXml()
        {
            var document = LoadXDocument();
            var result = new List<CustomMetric>();

            foreach (XmlNode counter in document.SelectNodes("//Counter"))
            {
                try
                {
                    var category = counter.Attributes["Category"].Value;
                    var counterName = counter.Attributes["Name"].Value;
                    var metricName = counter.ParentNode.Attributes["Name"].Value + "/" + counter.Attributes["MetricName"].Value;
                    var unit = counter.Attributes["Unit"].Value;
                    var instanceElement = counter.Attributes["Instance"];

                    short? ratio = null;
                    var ratioElement = counter.Attributes["ConversionRatio"];
                    if (ratioElement != null)
                    {
                        ratio = Convert.ToInt16(ratioElement.Value);
                    }
                    string instance = null;
                    if (instanceElement != null)
                    {
                        instance = instanceElement.Value;
                    }

                    var perfCounter = instance == null
                        ? new PerformanceCounter(category, counterName)
                        : new PerformanceCounter(category, counterName, instance);

                    result.Add(new CustomMetric(perfCounter, metricName, unit, ratio));
                }
                catch (Exception e)
                {
                    _logger.Error("Unable to build metric, the following error occured: {0}{1}{2}", e.Message, Environment.NewLine, e.StackTrace);
                }
            }

            return result;
        }

        private static XmlDocument LoadXDocument()
        {
            var document = new XmlDocument();
            var currentDirectory = Path.GetDirectoryName(typeof(CustomMetricsBuilder).Assembly.Location);
            var configFile = Path.Combine(currentDirectory, "config/Counters.xml");
            document.Load(configFile);
            return document;
        }
    }
}
