using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject seed1;
    public GameObject seed2;
    public GameObject seed3;
    public GameObject seed4;
    public GameObject seed5;

    public List<GameObject> SeedUIs;
    public List<GameObject> HarvestUIs;

    public GameObject goldUI;
    private TMP_Text goldText;

    public GameObject selectItem;

    public GameObject currentSeed;
    public int currentSeedIndex;
    public int currentHarvestIndex;

    public GameObject scytheTool;
    public GameObject wateringCanTool; // Sulama aracı
    public GameObject currentTool;

    public GameObject marketUI;


    public int gold=0;
    public List<int> seedInventory = new List<int> { 0, 10, 10, 10, 10, 10 };
    public List<int> harvestInventory = new List<int> { 0, 0, 0, 0, 0, 0 };

    void Start()
    {
       goldText =   goldUI.GetComponentInChildren<TMP_Text>();
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
        currentHarvestIndex = 0;
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
    public void Selectharvest(int harvestNumber)
    {
        Debug.Log(harvestNumber);
        currentHarvestIndex = harvestNumber;
        currentSeedIndex = 0;

     

        if (harvestNumber >= 0 && harvestNumber < HarvestUIs.Count)
        {
            selectItem.transform.position = HarvestUIs[harvestNumber].transform.position;
        }
    }

    public void BuyItem()
    {
        // gold --
        if (gold >0)
        {
        if (currentSeedIndex !=0) //buy seed
            {
                seedInventory[currentSeedIndex]++;
                goldAmount(-1);

            }
            else if (currentHarvestIndex != 0 ) //buy harvest
            {
                harvestInventory[currentHarvestIndex]++;
                goldAmount(-1);


            }  
                UpdateInventoryUI();
        }
    
      
    } 
    public void SellItem()
    {
        // gold ++
        if (currentSeedIndex !=0) //sell seed
        {
            if (seedInventory[currentSeedIndex]>0)
            {
                seedInventory[currentSeedIndex]--;
                goldAmount(+1);

            }

        }
        else if (currentHarvestIndex != 0 ) //sell harvest
        {
            if (harvestInventory[currentHarvestIndex]>0)
            {
                harvestInventory[currentHarvestIndex]--;
                goldAmount(+1);

            }

        }
        UpdateInventoryUI();

    }
    public void goldAmount(int amount)
    {
        gold += amount;
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
        goldText.text = gold.ToString();

        for (int i = 0; i < SeedUIs.Count; i++)
        {
            TMP_Text seedText = SeedUIs[i].GetComponentInChildren<TMP_Text>();
            TMP_Text harvestText = HarvestUIs[i].GetComponentInChildren<TMP_Text>();

            switch (i)
            {
                case 0:
                    seedText.text = "Empty";
                    break;
                case 1:
                    seedText.text = seedInventory[1].ToString();
                    harvestText.text = harvestInventory[1].ToString();
                    break;
                case 2:
                    seedText.text = seedInventory[2].ToString();
                    harvestText.text = harvestInventory[2].ToString();
                    break;
                case 3:
                    seedText.text = seedInventory[3].ToString();
                    harvestText.text = harvestInventory[3].ToString();
                    break;
                case 4:
                    seedText.text = seedInventory[4].ToString();
                    harvestText.text = harvestInventory[4].ToString();
                    break;
                case 5:
                    seedText.text = seedInventory[5].ToString();
                    harvestText.text = harvestInventory[5].ToString();
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
            selectItem.transform.position = tool.transform.position;
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
                seedInventory[plant.plantIndex] += 1;
                harvestInventory[plant.plantIndex] += 1;
                UpdateInventoryUI();
                field.ClearField();
            }
        }
    }

    public void WaterPlant(Field field)
    {
        if (field.IsPlanted() && field.GetCurrentPlant() != null)
        {
            Plant plant = field.GetCurrentPlant().GetComponent<Plant>();
            if (plant != null && plant.growthStage == 1)
            {
                plant.WaterPlant();
            }
        }
    }
}
