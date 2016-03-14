namespace YANRPP
{
    using System.Collections.Generic;
    using NewRelic.Platform.Sdk;

    /// <summary>
    /// Factory designed to build Perfmon agents.
    /// </summary>
    public class PerfmonAgentFactory : AgentFactory
    {
        /// <summary>
        /// Creates an agent based on its configuration properties.
        /// </summary>
        /// <param name="properties">The configuration properties.</param>
        /// <returns>The newly instantiated agent.</returns>
        public override Agent CreateAgentWithConfiguration(IDictionary<string, object> properties)
        {
            string name = (string)properties["name"];
            var metrics = CustomMetricsBuilder.BuildFromXml();

            return new PerfmonAgent(name, metrics);
        }
    }
}
