namespace YANRPP
{
    using System.Diagnostics;
    using System.IO;
    using System.Xml;
    using System.Collections.Generic;

    public static class CustomMetricsBuilder
    {
        public static IEnumerable<CustomMetric> BuildFromXml()
        {
            var document = LoadXDocument();
            var result = new List<CustomMetric>();

            foreach (XmlNode counter in document.SelectNodes("//Counter"))
            {
                var category = counter.Attributes["Category"].Value;
                var counterName = counter.Attributes["Name"].Value;
                var metricName = counter.ParentNode.Attributes["Name"].Value + "/" + counter.Attributes["MetricName"].Value;
                var unit = counter.Attributes["Unit"].Value;

                var perfCounter = new PerformanceCounter(category, counterName);
                result.Add(new CustomMetric(perfCounter, metricName, unit));
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
