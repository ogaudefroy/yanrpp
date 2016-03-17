namespace YANRPP
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Adapter translating a windows performance counter in a new relic counter.
    /// </summary>
    public class CustomMetric
    {
        private readonly PerformanceCounter _counter;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomMetric" /> class.
        /// </summary>
        /// <param name="counter">The underlying performance counter.</param>
        /// <param name="metricName">The metric name.</param>
        /// <param name="unit">The metric unit.</param>
        public CustomMetric(PerformanceCounter counter, string metricName, string unit)
        {
            if (counter == null)
            {
                throw new ArgumentNullException(nameof(counter));
            }
            if (string.IsNullOrEmpty(metricName))
            {
                throw new ArgumentNullException(nameof(metricName));
            }
            if (string.IsNullOrEmpty(unit))
            {
                throw new ArgumentNullException(nameof(unit));
            }
            _counter = counter;
            this.Unit = unit;
            this.MetricName = metricName;
        }

        /// <summary>
        /// Gets the New Relic metric name.
        /// </summary>
        public string MetricName { get; private set; }

        /// <summary>
        /// Gets the New Relic metric unit.
        /// </summary>
        public string Unit { get; private set; }

        /// <summary>
        /// Gets the New Relic metric next value.
        /// </summary>
        public float NextValue => _counter.NextValue();
    }
}
