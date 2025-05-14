using System.IO;
using UnityEngine;

public class SaveAndLoad {
    [SerializeField] private string fileName = "SaveData.txt";

    public void Save(BaseSaveData dataToSave) {
        string path;
        if (Application.isEditor)
            path = Application.dataPath + "/" + fileName;
        else
            path = Application.persistentDataPath + "/" + fileName;

        StreamWriter writer = new(path, false);
        writer.Write(JsonUtility.ToJson(dataToSave));
        writer.Dispose();
        writer.Close();
    }

    public BaseSaveData Load() {
        string path;
        if (Application.isEditor) 
            path = Application.dataPath + "/" + fileName;
        else 
            path = Application.persistentDataPath + "/" + fileName;

        if (File.Exists(path)) {
            StreamReader reader = new(path);

            BaseSaveData loadedData = JsonUtility.FromJson<BaseSaveData>(reader.ReadToEnd());
            reader.Dispose();
            reader.Close();

            return loadedData;
        }
        else 
            return null;
    }
}

[System.Serializable]
public abstract class BaseSaveData {

}