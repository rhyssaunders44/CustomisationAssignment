using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LoadSaveCharacter : MonoBehaviour
{
    static public LoadSaveCharacter instance;
    string filePath;
    public static FileInfo[] info;
    private string[][] subStrings;

    private void Awake()
    {

        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        filePath = Application.persistentDataPath + "/";

        DirectoryInfo dir = new DirectoryInfo(filePath);
        info = dir.GetFiles("*.*");

        subStrings = new string[info.Length][];

        //magical wizardry that i made up at very early in the morning
        if (info.Length != 0)
        {
            for (int i = 0; i < info.Length; i++)
            {
                subStrings[i] = info[i].ToString().Split('_','.');
            }

            foreach (string[] name in subStrings)
            {
                DataMaster.saveList.Add(name[1]);
            }
        }
    }

    public void SaveGame(GameData saveData, string saveName)
    {
        FileStream dataStream = new FileStream(filePath + "_" + saveName + ".data", FileMode.Create);

        BinaryFormatter converter = new BinaryFormatter();
        converter.Serialize(dataStream, saveData);
        dataStream.Close();
    }

    public GameData LoadGame(string loadName)
    {
        if (File.Exists(filePath + "_" + loadName + ".data") && loadName != "")
        {
            FileStream dataStream = new FileStream(filePath + "_" + loadName + ".data", FileMode.Open);

            BinaryFormatter converter = new BinaryFormatter();
            GameData saveData = converter.Deserialize(dataStream) as GameData;

            dataStream.Close();
            return saveData;
        }
        else
        {
            Debug.LogError("Save file not found in " + filePath + "_" + loadName + ".data");
            return null;
        }
    }
}
