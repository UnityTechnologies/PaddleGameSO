using UnityEngine;
using System.IO;

namespace GameSystemsCookbook
{
    /// <summary>
    /// Class for saving data to Application persistent path. Static for accessibility.
    /// </summary>
    public static class SaveManager
    {
        // Serializer to convert a save object into a new file format; defaults to JSON
        private static IDataSaver s_dataSaver = new JsonSaver();

        // These folder and subfolder names to keep everything organized (e.g. use a new subfolder for each game/app)
        private const string k_saveFolder = "/Saves/";
        private const string k_defaultSubFolder = "Default";
        private const string k_defaultFileName = "/Data.save";

        // Properties

        // The type of serializer (Json, XML, etc.)
        public static IDataSaver SaveDataSerializer
        {
            get => s_dataSaver;
            set => s_dataSaver = value;
        }

        // This method returns the full save path
        public static string GetSavePath(string subFolder = k_defaultSubFolder)
        {
            string savePath = Application.persistentDataPath + k_saveFolder;
            savePath += subFolder + "/";
            return savePath;
        }

        // Checks if save file already exists
        public static bool IsSaveFile(string fileName, string subFolder = k_defaultSubFolder)
        {
            string savePath = GetSavePath(subFolder);
            return File.Exists(savePath + fileName);
        }

        // Creates a directory, if necessary. Then saves a file using the saver of choice, closing the FileStream afterward.
        public static void Save(object objectToSave, string fileName = k_defaultFileName, string subFolder = k_defaultSubFolder)
        {
            string savePath = GetSavePath(subFolder);

            // Create the directory, if necessary
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            FileStream saveFile = File.Create(savePath + fileName);
            s_dataSaver.Save(objectToSave, saveFile);
            saveFile.Close();
        }

        // Loads a saved file from disk. Pass in the type, filename, and folder as parameters.
        public static object Load(System.Type typeToLoad, string fileName = k_defaultFileName, string subFolder = k_defaultSubFolder)
        {
            string savePath = GetSavePath(subFolder);

            if (File.Exists(savePath + fileName))
            {
                FileStream saveFile = File.Open(savePath + fileName, FileMode.Open);

                object data = s_dataSaver.Load(typeToLoad, saveFile);

                saveFile.Close();

                return data;
            }
            else
            {
                Debug.LogError("SaveManager: Save file not found at: " + savePath + fileName);
                return null;
            }
        }

        // Reads a text file from disk. Pass in the filename and subfolder as parameters
        public static string LoadTextFile(string fileName = k_defaultFileName, string subFolder = k_defaultSubFolder)
        {
            string savePath = GetSavePath(subFolder);

            if (File.Exists(savePath + fileName))
            {
                return File.ReadAllText(savePath + fileName);
            }

            Debug.LogError("SaveManager: Save file not found at: " + savePath + fileName);
            return string.Empty;
        }

        // Deletes a saved file from disk. Pass in the filename, and folder as parameters.
        public static void DeleteSave(string fileName, string subFolder = k_defaultSubFolder)
        {
            string savePath = GetSavePath(subFolder);

            if (File.Exists(savePath + fileName))
            {
                File.Delete(savePath + fileName);
            }
            else
            {
                Debug.LogError("SaveManager: Save file not found at: " + savePath + fileName);
            }
        }
    }
}