using System.IO;
using UnityEngine;

public class SaveFileHandler : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private Inventory playerInventory;

    private string saveFilePath = "/save.txt";

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
        saveFile.SetPlayerInventory(playerInventory);

        string saveFileJson = JsonUtility.ToJson(saveFile);
        File.WriteAllText(Application.dataPath + saveFilePath, saveFileJson);

    }

    public void Load()
    {
        Debug.Log("Load Files");
        if (File.Exists(Application.dataPath + saveFilePath))
        {
            string saveFileJson = File.ReadAllText(Application.dataPath + saveFilePath);
            SaveFile loadedSaveFile = JsonUtility.FromJson<SaveFile>(saveFileJson);

            gameSettings.LoadSaveFile(loadedSaveFile);
            playerInventory.LoadInventory(loadedSaveFile);
        }
    }

    public void ResetSaveFile()
    {
        Debug.Log("Reset Files");
        SaveFile saveFile = new SaveFile();

        string saveFileJson = JsonUtility.ToJson(saveFile);
        File.WriteAllText(Application.dataPath + saveFilePath, saveFileJson);

        Load();
    }
}
