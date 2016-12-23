# Yet Another New Relic Perfmon Plugin

This project was born from the need to monitor an SSAS instance in the marvelous New Relic APM. [Nick Floyd's perfmon plugin] (https://github.com/nickfloyd/newrelic-perfmon-plugin) wouldn't work as it does not collect 64 bit counters and thus we decided to build a quick and dirty one.

## Prerequisites
+ Windows Server to locally or remotely collect Perfmon counters
+ .Net 3.5 Client Profile
+ A windows service wrapper like [NSSM] (https://nssm.cc/)

## Customize performance counters collected
Out-of-the-box, this plugin collects various performance counters if you do want to monitor a SSAS instance. Two configuration files are available in config folder:
+ `Counters.2012.xml`: SQL Server Analysis Services 2012 Performance Counters 
+ `Counters.2014.xml`: SQL Server Analysis Services 2014 Performance Counters  

The agent loads `config/counters.xml` file so you have to copy/rename the file matching your edition.

**Performance counters are unfortunately culture sensitive, the provided configuration files are for en-US version only.**

## New Relic dashboard configuration
Once the perfmon plugin up and running you need to create a custom dashboard in New Relic. Follow the official documentation available here:
+ [New Relic - Working with plugin dashboard] (https://docs.newrelic.com/docs/plugins/developing-plugins/structuring-your-plugin/working-plugin-dashboards) 
+ [New Relic - Summary metrics] (https://docs.newrelic.com/docs/plugins/developing-plugins/structuring-your-plugin/creating-summary-metrics-plugins)

## Troubleshooting
+ To output collected metrics set `log_level:debug` in `config\newrelic.json`
+ If you do not see the plugin in your dashboard, set your license_key in `config\plugin.json`
