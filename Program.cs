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
        internal static XmlData XmlData;

        static void Main(string[] args)
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo("log4net.config"));
            log.Info("Simulator initialized!");
            XmlData = new XmlData();

            while (true)
                Console.ReadLine();
        }
    }
}
