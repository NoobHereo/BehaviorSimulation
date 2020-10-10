using System;
using System.IO;
using System.Threading;
using System.Globalization;

using log4net;
using log4net.Config;

namespace BehaviorSimulation
{
    class Program
    {
        static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo("log4net.config"));

            log.Info("XML configurated...");

            while (true)
                Console.ReadLine();
        }
    }
}
