using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace DataLibrary
{
    public static class JSONHelper
    {
		public static readonly JsonSerializerSettings JsonSettings;
        static JSONHelper()
        {
            JsonSettings = new Newtonsoft.Json.JsonSerializerSettings();
            JsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            JsonSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            JsonSettings.Formatting = Newtonsoft.Json.Formatting.None;
            JsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }

        public static string SerializeToJson<T>(T obj)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj, JsonSettings);
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
                throw ex;
            }
        }

        public static T DeserializeFromJson<T>(string json)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json, JsonSettings);
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
                return default(T);
            }
        }

        public static string FormatXml(string xml)
        {
            try
            {
                var stringBuilder = new StringBuilder();

                var element = XElement.Parse(xml);

                var settings = new XmlWriterSettings();
                settings.OmitXmlDeclaration = true;
                settings.Indent = true;
                settings.NewLineOnAttributes = true;

                using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
                {
                    element.Save(xmlWriter);
                }

                return stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
                return null;
            }
        }
    }
}
