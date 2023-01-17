using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager
{
    private const string FolderName = "/Saves";
    private const string Extention = ".gamesave";
    private static readonly string _defaultPath = Application.persistentDataPath + FolderName;
    private static readonly BinaryFormatter _binaryFormatter = new();

    public static void Save<T>(string localFolderName, string fileName, T objectForSerialization)
    {
        Directory.CreateDirectory(Application.persistentDataPath + FolderName + "/" + localFolderName);
        string path = _defaultPath + "/" + localFolderName + "/" + fileName + Extention;
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        _binaryFormatter.Serialize(stream, objectForSerialization);
        stream.Close();
    }

    public static T GetLoadOrDefault<T>(string localFolderName, string fileName)
    {
        string path = _defaultPath + "/" + localFolderName + "/" + fileName + Extention;
        Debug.Log("Saves path: " + path);

        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            object result = _binaryFormatter.Deserialize(stream);
            stream.Close();

            if (result is T tResult)
                return tResult;
            else
                throw new System.InvalidCastException();
        }
        return default;
    }

    public static void RemoveAllSaves()
    {
        if (Directory.Exists(_defaultPath))
            Directory.Delete(_defaultPath, true);
    }
}