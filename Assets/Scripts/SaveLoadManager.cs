using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager
{
    private const string FolderName = "/Saves";
    private const string Extention = ".gamesave";
    private const string TriggersFolderName = "/Triggers";
    private static readonly string _defaultPath = Application.persistentDataPath + FolderName;
    private static readonly BinaryFormatter _binaryFormatter = new();

    public static void Save(string localFolderName, string fileName, object objectForSerialization)
    {
        Directory.CreateDirectory(Application.persistentDataPath + FolderName + localFolderName);
        string path = _defaultPath + "/" + localFolderName + "/" + fileName + Extention;
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        _binaryFormatter.Serialize(stream, objectForSerialization);
        stream.Close();
    }

    public static object GetLoadOrDefault(string localFolderName, string fileName)
    {
        string path = _defaultPath + "/" + localFolderName + "/" + fileName + Extention;

        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            object result = _binaryFormatter.Deserialize(stream);
            stream.Close();
            return result;
        }
        else
        {
            Debug.Log("File is not exixt. Path: " + path);
            return null;
        }
    }

    public static void SetBool(string fileName, bool value)
    {
        var booleanSaveData = new BooleanSaveData(value);
        Save(TriggersFolderName, fileName, booleanSaveData);
    }

    public static bool GetBoolOrDefault(string fileName, bool defaultValue = false)
    {
        var booleanSaveData = (BooleanSaveData)GetLoadOrDefault(TriggersFolderName, fileName);
        return (booleanSaveData != null) ? booleanSaveData.Value : defaultValue;
    }

    public static void RemoveAllSaves()
    {
        if (Directory.Exists(_defaultPath))
            Directory.Delete(_defaultPath, true);
    }

    [System.Serializable]
    private class BooleanSaveData
    {
        public bool Value;

        public BooleanSaveData(bool value)
        {
            Value = value;
        }
    }
}
