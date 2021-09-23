using System.IO;
using UnityEngine;

public class SaveFileHandler : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;

    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    public void Save()
    {
        Debug.Log("Save Files");
        SaveFile saveFile = new SaveFile();

        saveFile.SetGameSettings(gameSettings);

        string saveFileJson = JsonUtility.ToJson(saveFile);
        File.WriteAllText(Application.dataPath + "/save.txt", saveFileJson);

    }

    public void Load()
    {
        Debug.Log("Load Files");
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            string saveFileJson = File.ReadAllText(Application.dataPath + "/save.txt");
            SaveFile loadedSaveFile = JsonUtility.FromJson<SaveFile>(saveFileJson);

            gameSettings.LoadSaveFile(loadedSaveFile);
        }
    }
}
