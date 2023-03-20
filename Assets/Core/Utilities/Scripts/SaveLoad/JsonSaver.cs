using System;
using System.IO;
using UnityEngine;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This class implements the IDataSave interface to save and load a JSON file,
    /// using the System.IO StreamReader or StreamWriter. This works with the SaveManager class.
    /// Each game will use its own unique System.Object containing save data, so it
    /// can be unique to each application.
    /// </summary>
    public class JsonSaver : IDataSaver
    {
        // This saves a System.Object containing game data into the specified saveFile filestream
        public void Save(object objectToSave, FileStream saveFile)
        {
            // Serializes the save data object into JSON
            string json = JsonUtility.ToJson(objectToSave);

            // Writes and automatically close the FileStream and StreamWriter
            using (StreamWriter streamWriter = new StreamWriter(saveFile, System.Text.Encoding.UTF8))
            {
                streamWriter.Write(json);
            }
        }

        // The typeToLoad varies per application so can differientiate save data files (e.g. PaddleBallSaveData)
        // or multiple types of data within the same application.
        public object Load(Type typeToLoad, FileStream saveFile)
        {
            // Reads and automatically closes the FileStream and StreamReader
            using (StreamReader streamReader = new StreamReader(saveFile, System.Text.Encoding.UTF8))
            {
                string json = streamReader.ReadToEnd();
                return JsonUtility.FromJson(json, typeToLoad);
            }
        }
    }
}