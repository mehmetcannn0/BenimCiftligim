using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] seeds;
    public List<GameObject> SeedUIs;
    public List<GameObject> HarvestUIs;

    public GameObject goldUI;
    private TMP_Text goldText;

    public GameObject selectItem;

    public GameObject currentSeed;
    public int currentSeedIndex;
    public int currentHarvestIndex;

    public GameObject scytheTool;
    public GameObject wateringCanTool;
    public GameObject currentTool;

    public GameObject marketUI;

    private float gold = 0;
    private List<int> seedInventory = new List<int> { 0, 1, 1, 1, 1, 1 };
    private List<int> harvestInventory = new List<int> { 0, 0, 0, 0, 0, 0 };

    void Start()
    {
        goldText = goldUI.GetComponentInChildren<TMP_Text>();
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
        currentSeed = (seedNumber > 0 && seedNumber <= seeds.Length) ? seeds[seedNumber - 1] : null;

        if (seedNumber >= 0 && seedNumber < SeedUIs.Count)
        {
            selectItem.transform.position = SeedUIs[seedNumber].transform.position - new Vector3(0, 20, 0);
        }
    }

    public void SelectHarvest(int harvestNumber)
    {
        Debug.Log(harvestNumber);
        currentHarvestIndex = harvestNumber;
        currentSeedIndex = 0;

        if (harvestNumber >= 0 && harvestNumber < HarvestUIs.Count)
        {
            selectItem.transform.position = HarvestUIs[harvestNumber].transform.position - new Vector3(0, 20, 0);
        }
    }

    public void BuyItem()
    {
        if (gold > 0)
        {
            if (currentSeedIndex != 0)
            {
                if (gold >= currentSeedIndex)
                {
                    seedInventory[currentSeedIndex]++;
                    goldAmount(-currentSeedIndex);
                }
            }
            else if (currentHarvestIndex != 0)
            {
                if (gold >= currentHarvestIndex * 2.25f)
                {
                    harvestInventory[currentHarvestIndex]++;
                    goldAmount(-currentHarvestIndex * 2.25f);
                }
            }
            UpdateInventoryUI();
        }
    }

    public void SellItem()
    {
        if (currentSeedIndex != 0)
        {
            if (seedInventory[currentSeedIndex] > 0)
            {
                seedInventory[currentSeedIndex]--;
                goldAmount(currentSeedIndex * 0.75f);
            }
        }
        else if (currentHarvestIndex != 0)
        {
            if (harvestInventory[currentHarvestIndex] > 0)
            {
                harvestInventory[currentHarvestIndex]--;
                goldAmount(currentHarvestIndex * 2f);
            }
        }
        UpdateInventoryUI();
    }

    public void goldAmount(float amount)
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

            if (i == 0)
            {
                seedText.text = "Empty";
            }
            else
            {
                seedText.text = seedInventory[i].ToString();
                harvestText.text = harvestInventory[i].ToString();
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
                seedInventory[plant.plantIndex]++;
                harvestInventory[plant.plantIndex]++;
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

    public void ToggleMarketUI()
    {
        marketUI.SetActive(!marketUI.activeSelf);
    }
}
