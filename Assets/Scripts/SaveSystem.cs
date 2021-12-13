using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SaveRecord(GameManager gM)
    {
        //BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/record.bruh";
        //FileStream stream = new FileStream(path, FileMode.Create);

        //RecordData data = new RecordData(gM);

        //formatter.Serialize(stream, data);
        //stream.Close();

        File.WriteAllText(path, gM._maxScore.ToString());
    }

    public static int LoadRecord()
    {
        string path = Application.persistentDataPath + "/record.bruh";
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "0");

            Debug.LogError("Save file not found in " + path);
            //return null;
        }


        //BinaryFormatter formatter = new BinaryFormatter();
        //FileStream stream = new FileStream(path, FileMode.Open);

        //RecordData data = formatter.Deserialize(stream) as RecordData;
        //stream.Close();

        return int.Parse(File.ReadAllText(path));
        ;
    }
}
