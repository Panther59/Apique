using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace DataLibrary
{
    public static class XMLHelper
    {
        public static string SerializeToXml<T>(T obj)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(T));

                TextWriter writer = new StringWriter();
                serializer.Serialize(writer, obj);

                return writer.ToString();

            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
                throw ex;
            }
        }

        public static T DeserializeFromXml<T>(string xml)
        {
            try
            {
                T result = default(T);
                if (!string.IsNullOrEmpty(xml))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(T));
                    using (TextReader tr = new StringReader(xml))
                    {
                        result = (T)ser.Deserialize(tr);
                    }
                }
                return result;
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
