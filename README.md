# Yet Another New Relic Perfmon Plugin

This project was born from the need to monitor an SSAS instance in the wonderful New Relic APM. [Nick Floyd's perfmon plugin] (https://github.com/nickfloyd/newrelic-perfmon-plugin) wouldn't work as it does not collect 64 bit counters and thus we decided to build a quick and dirty one.

## Prerequisites
+ Windows Server to locally collect Perfmon counters
+ .Net 3.5 Client Profile
+ A windows service wrapper like [NSSM] (https://nssm.cc/)

## Customize performance counters collected
The agent loads `config/counters.xml` file so you have to copy/rename/create your own counter file matching your needs.

As mentioned previously, it's a quick and dirty version and counters.xml is not currently validated with a XSD here is the way we parse it:
+ **Counters**: root node containing all metric groups
+ **MetricGroup**: a group of metrics, requires a Name attribute `<MetricGroup Name="OverallMemory"></MetricGroup>`
+ **Counter**: a performance counter with the following attributes
    + **Category**: the category of the performance counter (required)
    + **Name**: the performance counter name (required)
    + **Instance**: the instance of the performance counter (optional)
    + **MetricName**: the name of the metric reported in New Relic (required)
    + **Unit**: the unit in which the metric reported in New Relic (required)
    + **ConversionRatio**: an optional ratio to declare a 1024^ConversionRatio of the collected metric value

Here is a very simple counters.xml definition  
```xml
<?xml version="1.0"?>
<Counters>
    <MetricGroup Name="OverallMemory">
    <Counter Category="Memory" Name="Pages/sec" MetricName="System Idle" Unit="pages/s" />
    <Counter Category="Memory" Name="Committed Bytes" MetricName="Committed VRAM" Unit="Gigabytes" ConversionRatio="3" />
    <Counter Category="Memory" Name="% Committed Bytes in Use" MetricName="Committed VRAM usage" Unit="%" />
  </MetricGroup>
</Counters>
```
Out-of-the-box, this plugin collects various performance counters if you do want to monitor an SSAS instance. Two configuration files are available in config folder:
+ `Counters.SSAS.2012.xml`: SQL Server Analysis Services 2012 Performance Counters 
+ `Counters.SSAS.2014.xml`: SQL Server Analysis Services 2014 Performance Counters  


## Retrieve performance counter names and categories
A very simple way to retrieve them is to use typeperf command line: `typeperf -q`  
**Performance counters are unfortunately culture sensitive, the provided configuration files are for en-US version only.**

## New Relic dashboard configuration
Once the perfmon plugin up and running you need to create a custom dashboard in New Relic. Follow the official documentation available here:
+ [New Relic - Working with plugin dashboard] (https://docs.newrelic.com/docs/plugins/developing-plugins/structuring-your-plugin/working-plugin-dashboards) 
+ [New Relic - Summary metrics] (https://docs.newrelic.com/docs/plugins/developing-plugins/structuring-your-plugin/creating-summary-metrics-plugins)

## Troubleshooting
+ To output collected metrics set `log_level:debug` in `config\newrelic.json`
+ If you do not see the plugin in your dashboard, set your license_key in `config\plugin.json`
