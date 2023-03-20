using System.IO;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This interface converts an object into a saveable file format (e.g. Json, XML, etc.). 
    /// This supports customizable formats so each application can have a unique set of data
    /// or allows you to store multiple data files within the same application.
    /// </summary>
    public interface IDataSaver
    {
        // Stores a System.object into a save file
        void Save(object objectToSave, FileStream saveFile);

        // Returns a System.object from a save file and System.Type (allows for different formats)
        object Load(System.Type objectType, FileStream saveFile);
    }
}