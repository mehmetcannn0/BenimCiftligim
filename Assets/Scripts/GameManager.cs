using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject seed1; // Tohum 1 GameObject'i
    public GameObject seed2; // Tohum 2 GameObject'i
    public GameObject seed3; // Tohum 3 GameObject'i
    public GameObject seed4; // Tohum 4 GameObject'i
    public GameObject seed5; // Tohum 5 GameObject'i

    public List<GameObject> SeedUIs;
    public GameObject selectItem;

    public GameObject currentSeed; // Şu an seçili olan tohum
    public int currentSeedIndex;

    public GameObject scytheTool; // Tırpan GameObject'i
    public GameObject currentTool; // Şu an seçili olan tool

    // Envanter miktarlarını takip eden bir liste
    public List<int> seedInventory = new List<int> {111, 10, 10, 10, 10, 10 };

    void Start()
    {
        UpdateInventoryUI();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectSeed(int seedNumber)
    {
        Debug.Log(seedNumber);
        currentSeedIndex = seedNumber;

        switch (seedNumber)
        {
            case 0:
                currentSeed = null;
                break;
            case 1:
                currentSeed = seed1;
                break;
            case 2:
                currentSeed = seed2;
                break;
            case 3:
                currentSeed = seed3;
                break;
            case 4:
                currentSeed = seed4;
                break;
            case 5:
                currentSeed = seed5;
                break;
            default:
                currentSeed = null;
                break;
        }

        if (seedNumber >= 0 && seedNumber < SeedUIs.Count)
        {
            selectItem.transform.position = SeedUIs[seedNumber].transform.position;
        }
    }

    public bool PlantSeedAtField(Field field)
    {
        if (currentSeed != null && seedInventory[currentSeedIndex] > 0 && !field.IsPlanted())
        {
            field.PlantSeed(currentSeed);
            seedInventory[currentSeedIndex]--;
            UpdateInventoryUI();
            return true;
        }
        return false;
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < SeedUIs.Count; i++)
        {
            TMP_Text seedText = SeedUIs[i].GetComponentInChildren<TMP_Text>();

            switch (i)
            {
                case 0:
                    seedText.text = "Empty";
                    break;
                case 1:
                    seedText.text = seedInventory[1].ToString();
                    break;
                case 2:
                    seedText.text = seedInventory[2].ToString();
                    break;
                case 3:
                    seedText.text = seedInventory[3].ToString();
                    break;
                case 4:
                    seedText.text = seedInventory[4].ToString();
                    break;
                case 5:
                    seedText.text = seedInventory[5].ToString();
                    break;
                default:
                    seedText.text = "0";
                    break;
            }
        }
    }

    public void SelectTool(GameObject tool)
    {
        currentTool = tool;

        if (tool != null)
        {
            selectItem.transform.position = tool.transform.position; // Tool seçimi UI'da göstermek için
        }
    }

    public void HarvestPlant(Field field)
    {
        if (field.IsPlanted() && field.GetCurrentPlant() != null)
        {
            Plant plant = field.GetCurrentPlant().GetComponent<Plant>();
            if (plant != null && plant.growthStage == 2)
            {
                plant.Harvest();
                // Envantere 2 tohum ekleyin
                seedInventory[plant.plantIndex] += 2;

                //if (currentSeedIndex > 0 && currentSeedIndex < seedInventory.Count)
                //{
                //    seedInventory[currentSeedIndex] += 2;
                //}
                UpdateInventoryUI();
                field.ClearField();
            }
        }
    }
}
