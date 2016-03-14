namespace YANRPP
{
    using System;
    using NewRelic.Platform.Sdk;

    class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                Runner runner = new Runner();
                runner.Add(new PerfmonAgentFactory());
                runner.SetupAndRun();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("An error occured : {0}", e.Message);
                Console.Error.WriteLine(e.StackTrace);
                return -1;
            }
            return 0;
        }
    }
}
