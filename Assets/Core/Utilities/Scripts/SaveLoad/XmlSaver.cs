using System;
using System.IO;
using System.Xml.Serialization;

namespace GameSystemsCookbook
{
    /// <summary>
    /// Helper class that serializes a save data object into XML format
    /// and saves it. Or loads a saved XML file and deserializes it.
    /// </summary>
    public class XmlSaver : IDataSaver
    {
        public string SerializeObject(object objectToSave)
        {
            XmlSerializer serializer = new XmlSerializer(objectToSave.GetType());

            using (StringWriter stringWriter = new StringWriter())
            {
                serializer.Serialize(stringWriter, objectToSave);
                return stringWriter.ToString();
            }
        }

        public object DeserializeXML(Type typeToLoad, string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeToLoad);

            using (StringReader stringReader = new StringReader(xml))
            {
                return serializer.Deserialize(stringReader);
            }
        }

        public void Save(object objectToSave, FileStream saveFile)
        {
            string xml = SerializeObject(objectToSave);

            using (StreamWriter streamWriter = new StreamWriter(saveFile, System.Text.Encoding.UTF8))
            {
                streamWriter.Write(xml);
            }
        }

        // typeToLoad will vary per application (e.g. PaddleBallSaveData, QuizSaveData)
        public object Load(Type typeToLoad, FileStream saveFile)
        {
            using (StreamReader streamReader = new StreamReader(saveFile, System.Text.Encoding.UTF8))
            {
                string xml = streamReader.ReadToEnd();
                return DeserializeXML(typeToLoad, xml);
            }
        }

    }
}