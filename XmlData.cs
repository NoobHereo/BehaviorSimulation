﻿using System;
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

        void ProcessXml(XElement root)
        {
            AddObjects(root);
        }

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
                log.InfoFormat("Loaded Object: {0}, class: {1}", name, clss);
            }
        }
    }
}