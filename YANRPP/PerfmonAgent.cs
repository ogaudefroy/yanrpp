namespace YANRPP
{
    using System;
    using System.Collections.Generic;
    using NewRelic.Platform.Sdk;

    /// <summary>
    /// Core agent ; collects perfmon metrics and transfers them to New Relic.
    /// </summary>
    public class PerfmonAgent : Agent
    {
        private readonly string _name;
        private readonly IEnumerable<CustomMetric> _metrics;

        /// <summary>
        /// Initializes a new instance of the <see cref="PerfmonAgent"/> class.
        /// </summary>
        /// <param name="name">The instance name.</param>
        /// <param name="metrics"></param>
        public PerfmonAgent(string name, IEnumerable<CustomMetric> metrics)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            _name = name;
            _metrics = metrics;
        }

        /// <summary>
        /// Agent polling process.
        /// </summary>
        public override void PollCycle()
        {
            foreach(var counter in _metrics)
            {
                ReportMetric(counter.MetricName, counter.Unit, counter.NextValue);
            }
        }

        /// <summary>
        /// Returns the agent name under monitoring (ie. the SSAS instance being monitored).
        /// </summary>
        /// <returns>The agent name.</returns>
        public override string GetAgentName()
        {
            return _name;
        }

        /// <summary>
        /// Gets the agent unique identifier.
        /// </summary>
        public override string Guid => "com.ogaudefroy.yanrpp";

        /// <summary>
        /// Gets the client version.
        /// </summary>
        public override string Version => this.GetType().Assembly.GetName().Version.ToString();
    }
}
