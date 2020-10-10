using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;
using log4net;

namespace BehaviorSimulation
{
    public class XmlData
    {
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }      
        }

        static ILog log = LogManager.GetLogger(typeof(XmlData));
        string path = "xml";

        /// <summary>
        /// The main class for handling all xml data.
        /// </summary>
        public XmlData()
        {
            string basePath = Path.Combine(AssemblyDirectory, path);
            log.Info("Loading XML data");

            var xmls = Directory.EnumerateFiles(basePath, "*.xml", SearchOption.AllDirectories).ToArray();
            for (int i = 0; i < xmls.Length; i++)
            {
                //log.InfoFormat("Loaded XML file: {0}", xmls[i]);
                using (Stream stream = File.OpenRead(xmls[i]))
                    ProcessXml(XElement.Load(stream));
                    //Proccess the xml here.
            }
            log.InfoFormat("Loaded {0} xml files", xmls.Length);
        }

        /// <summary>
        /// Processes the xml files, and adds them depending on their root element.
        /// </summary>
        /// Note: Should probably check what kind of xml element we have to do with before trying to add it for different kinds of xml elements.
        /// <param name="root"></param>
        void ProcessXml(XElement root)
        {
            AddObjects(root);
        }

        /// <summary>
        /// Adds all xml elements that are specified as a Object.
        /// </summary>
        /// <param name="root"></param>
        public void AddObjects(XElement root)
        {
            foreach(var elem in root.XPathSelectElements("//Object"))
            {
                string name = elem.Attribute("name").Value;
                if (elem.Element("Class") == null)
                {
                    log.WarnFormat("The element {0} does not have a specified class.", name);
                    continue;
                }
                string clss = elem.Element("Class").Value;
                log.DebugFormat("Loaded Object: {0}, class: {1}", name, clss);
            }
        }
    }
}