using System.Collections.Generic;
using System.IO;
using UnityEngine;
using DG.Tweening;


public class SaveLoadManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject saveUI;


    public void SaveGame()
    {
        GameData gameData = new GameData();
        gameData.gold = gameManager.GetGold(); 
        gameData.Inventory = gameManager.GetInventory();
        gameData.FieldLocks = gameManager.GetFieldLocks();

        List<PlantData> plants = new List<PlantData>();
        Field[] fieldObjects = FindObjectsOfType<Field>();
        foreach (Field field in fieldObjects)
        {
            if (field.IsPlanted())
            {
                PlantData plantData = new PlantData();
                Plant plant = field.GetCurrentPlant().GetComponent<Plant>(); 
                plantData.plantIndex = plant.plantIndex;
                plantData.growthStage = plant.growthStage;
                plantData.isWatered = plant.isWatered;
                plantData.timer = plant.timer;
                plantData.timestamp = plant.timestamp;
                plantData.position = field.transform.position;
                plants.Add(plantData);
            }
        }
        gameData.plants = plants;

        string json = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString("GameData",json);
        PlayerPrefs.Save();
        Debug.Log("Game Saved");

        // Ýlk animasyon: 0.4'ten 1'e
        saveUI.transform.DOScale(0.50f, 1f).OnComplete(() =>
        {
            // Ýlk animasyon tamamlandýðýnda, ikinci animasyonu baþlat
            saveUI.transform.DOScale(0.4f, 1f);
        });

    }

    public void LoadGame()
    { 
        if (PlayerPrefs.HasKey("GameData"))
        { 
            string json = PlayerPrefs.GetString("GameData");
            GameData gameData = JsonUtility.FromJson<GameData>(json);

            gameManager.SetGold(gameData.gold); 
            gameManager.SetInventory(gameData.Inventory);
            gameManager.SetFieldLocks(gameData.FieldLocks);

            foreach (PlantData plantData in gameData.plants)
            {
                Field field = FindFieldAtPosition(plantData.position);
                if (field != null)
                {
                    GameObject plantPrefab = gameManager.GetPlantPrefab(plantData.plantIndex);
                    field.PlantSeed(plantPrefab);
                    Plant plant = field.GetCurrentPlant().GetComponent<Plant>();
                    plant.growthStage = plantData.growthStage;
                    plant.isWatered = plantData.isWatered;
                    plant.timer = plantData.timer;
                    plant.timestamp = plantData.timestamp;
                }
            }
            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("Save key not found");
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
