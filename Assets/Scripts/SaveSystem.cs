using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SaveRecord(GameManager gM)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/record.bruh";
        FileStream stream = new FileStream(path, FileMode.Create);

        RecordData data = new RecordData(gM);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static RecordData LoadRecord()
    {
        string path = Application.persistentDataPath + "/record.bruh";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            RecordData data = formatter.Deserialize(stream) as RecordData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
