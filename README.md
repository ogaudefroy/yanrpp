# yanrpp-ssas
Yet Another New Relic Perfmon Plugin - SQL Server Analysis Edition  

This project was born from the need to monitor an SSAS instance in the marvelous New Relic APM. [Nick Floyd's perfmon plugin] (https://github.com/nickfloyd/newrelic-perfmon-plugin) however wouldn't work as it does not collect 64 bit counters.

## Prerequisites
+ Windows Server to locally or remotely collect Perfmon counters
+ .Net 3.5 Client Profile

## Customize performance counters collected
Out-of-the-box, this plugin collects various performance counters if you do want to monitor a SSAS instance. Two configuration files are available in config folder:
+ `Counters.2012.xml`: SQL Server Analysis Services 2012 Performance Counters 
+ `Counters.2014.xml`: SQL Server Analysis Services 2014 Performance Counters  

The agent loads `config/counters.xml` file so you have to copy/rename the file matching your edition.

**Performance counters are unfortunately culture sensitive, the provided configuration files are for en-US version only.**

## Troubleshooting
+ To output collected metrics set `log_level:debug` in `config\newrelic.json`
+ If you do not see the plugin in your dashboard, set your license_key in `config\plugin.json`