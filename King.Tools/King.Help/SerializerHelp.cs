using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace King.Help
{
    /// <summary>
    /// Content:序列化相关
    /// Time:2015.11.11
    /// Author:王达国
    /// </summary>
    public class SerializerHelp
    {
        public SerializerHelp()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// Content:泛型序列化集合对象到字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objToXml"></param>
        /// <returns></returns>
        public static string Serializer<T>(T objToXml)
        {
            System.IO.StringWriter writer = new System.IO.StringWriter();
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(objToXml.GetType());
            serializer.Serialize(writer, objToXml);
            return writer.GetStringBuilder().ToString();
        }
        /// <summary>
        /// Content:泛型反序列化字符串到对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sXml"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Collection<T> DeSerializerCollection<T>(string sXml, Type type)
        {
            XmlReader reader = XmlReader.Create(new StringReader(sXml));
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(type);

            object obj = serializer.Deserialize(reader);
            return (Collection<T>)obj;
        }

        /// <summary>
        /// Content:对象序列化XML
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string SerializeToXml(object o)
        {
            XmlSerializer serializer = new XmlSerializer(o.GetType(), "");
            MemoryStream w = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(w, Encoding.Default);
            writer.Formatting = Formatting.None;
            serializer.Serialize((XmlWriter)writer, o);
            writer.Close();
            return Encoding.Default.GetString(w.ToArray())
            .Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "")
            .Replace("<?xml version=\"1.0\" encoding=\"gb2312\"?>", "");
        }

        /// <summary>
        /// Content:XML反序列化对象
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object DeserializeFromXml(string s, Type t)
        {
            XmlSerializer serializer = new XmlSerializer(t);
            object obj2 = null;
            XmlTextReader xmlReader = new XmlTextReader(s, XmlNodeType.Element, null);
            obj2 = serializer.Deserialize(xmlReader);
            xmlReader.Close();
            return obj2;
        }
    }
}