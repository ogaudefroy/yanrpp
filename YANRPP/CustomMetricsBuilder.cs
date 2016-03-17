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
                    var metricName = counter.ParentNode.Attributes["Name"].Value + "/" +
                                     counter.Attributes["MetricName"].Value;
                    var unit = counter.Attributes["Unit"].Value;

                    _logger.Info("Building CustomMetric Category: {0}, Counter: {1}, Metric: {2}, Unit: {3}", category, counterName, metricName, unit);

                    var perfCounter = new PerformanceCounter(category, counterName);
                    result.Add(new CustomMetric(perfCounter, metricName, unit));
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
