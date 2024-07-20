using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public GameManager gameManager;

    public void SaveGame()
    {
        GameData gameData = new GameData();
        gameData.gold = gameManager.GetGold(); 
        gameData.Inventory = gameManager.GetInventory();

        List<FieldData> fields = new List<FieldData>();
        Field[] fieldObjects = FindObjectsOfType<Field>();
        foreach (Field field in fieldObjects)
        {
            if (field.IsPlanted())
            {
                FieldData fieldData = new FieldData();
                Plant plant = field.GetCurrentPlant().GetComponent<Plant>();
                fieldData.plantIndex = plant.plantIndex;
                fieldData.growthStage = plant.growthStage;
                fieldData.isWatered = plant.isWatered;
                fieldData.timer = plant.timer;
                fieldData.position = field.transform.position;
                fields.Add(fieldData);
            }
        }
        gameData.fields = fields;

        string json = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString("GameData",json);
        PlayerPrefs.Save(); 
    }

    public void LoadGame()
    { 
        if (PlayerPrefs.HasKey("GameData"))
        { 
            string json = PlayerPrefs.GetString("GameData");
            GameData gameData = JsonUtility.FromJson<GameData>(json);

            gameManager.SetGold(gameData.gold); 
            gameManager.SetInventory(gameData.Inventory);

            foreach (FieldData fieldData in gameData.fields)
            {
                Field field = FindFieldAtPosition(fieldData.position);
                if (field != null)
                {
                    GameObject plantPrefab = gameManager.GetPlantPrefab(fieldData.plantIndex);
                    field.PlantSeed(plantPrefab);
                    Plant plant = field.GetCurrentPlant().GetComponent<Plant>();
                    plant.growthStage = fieldData.growthStage;
                    plant.isWatered = fieldData.isWatered;
                    plant.timer = fieldData.timer;
                }
            }
            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("Save file not found");
        }
    }

    private Field FindFieldAtPosition(Vector3 position)
    {
        Field[] fields = FindObjectsOfType<Field>();
        foreach (Field field in fields)
        {
            if (field.transform.position == position)
            {
                return field;
            }
        }
        return null;
    }
}
