//using Microsoft.Unity.VisualStudio.Editor;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] seeds;
    //public List<GameObject> SeedUIs;
    //public List<GameObject> HarvestUIs;
    //public List<GameObject> ToolUIs;
    public List<GameObject> SeedHarvestToolUIs;
    public int SelectedSeedHarvestToolIndex;

    public GameObject goldUI;
    private TMP_Text goldText;

    //public GameObject selectItem;

    public GameObject currentSeed;
    //public int currentSeedIndex;
    //public int currentHarvestIndex;

    public GameObject scytheTool;
    public GameObject wateringCanTool;
    public GameObject currentTool;
     
    public List<Sprite> SelectedSeedHarvestToolSprites;
    public List<Sprite> SeedHarvestToolSprites; 

    public Sprite previousSelectedSprite;
    public int previousSelectedIndex;

    public GameObject marketUI;
    public GameObject marketOpenButtonUI;
    public GameObject marketCloseButtonUI;

    private float gold = 0;
    //private List<int> seedInventory = new List<int> { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 };
    //private List<int> harvestInventory = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    private List<int> Inventory = new List<int> { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


    public SaveLoadManager saveLoadManager;

    void Start()
    {
        saveLoadManager.LoadGame();
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

    public void SelectItem(int index)
    {
        Debug.Log(index);

        previousSelectedIndex = SelectedSeedHarvestToolIndex;
        previousSelectedSprite = SeedHarvestToolSprites[previousSelectedIndex];
        Image previousselectedImage = SeedHarvestToolUIs[previousSelectedIndex].GetComponent<Image>();
        previousselectedImage.sprite = previousSelectedSprite;

        SelectedSeedHarvestToolIndex = index;

        if (index>=0 && index<10 )
        {
            currentSeed = seeds[index] ;
        }
        else
        {
            currentSeed = null;
        }
        if (index == 20 || index == 21)
        {
            currentTool = SeedHarvestToolUIs[index];
        }
        else
        {
            currentTool = null;
        }
        //selectItem.transform.position = SeedUIs[seedNumber].transform.position - new Vector3(0, 20, 0);
        Image selectedImage = SeedHarvestToolUIs[index].GetComponent<Image>();
        selectedImage.sprite = SelectedSeedHarvestToolSprites[index];
        
    }

    //public void SelectSeed(int seedNumber)
    //{
    //    Debug.Log(seedNumber);
    //    currentSeedIndex = seedNumber;
    //    currentHarvestIndex = 0;
    //    currentSeed = (seedNumber > 0 && seedNumber <= seeds.Length) ? seeds[seedNumber - 1] : null;

    //    if (seedNumber >= 0 && seedNumber < SeedUIs.Count)
    //    {
    //        //selectItem.transform.position = SeedUIs[seedNumber].transform.position - new Vector3(0, 20, 0);
    //        Image selectedSeedImage =  SeedUIs[seedNumber].GetComponent<Image>();
    //        selectedSeedImage.sprite = selectedSeedSprites[seedNumber];
    //    }
    //}

    //public void SelectHarvest(int harvestNumber)
    //{
    //    Debug.Log(harvestNumber);
    //    currentHarvestIndex = harvestNumber;
    //    currentSeedIndex = 0;

    //    if (harvestNumber >= 0 && harvestNumber < HarvestUIs.Count)
    //    {
    //        Image selectedHarvestImage = HarvestUIs[harvestNumber].GetComponent<Image>();
    //        selectedHarvestImage.sprite = selectedHarvestSprites[harvestNumber];
    //    }
    //}
    //public void SelectTool(int toolIndex)
    //{
    //    currentTool = ToolUIs[toolIndex];

    //    if (currentTool != null)
    //    {
    //        Image currentToolImage = currentTool.GetComponentInChildren<Image>();
    //        currentToolImage.sprite = selectedToolSprites[toolIndex];
    //    }
    //}

    public void BuyItem()
    {
        if (gold > 0)
        {
            if (SelectedSeedHarvestToolIndex>=0 && SelectedSeedHarvestToolIndex<10)
            {
                if (gold >= SelectedSeedHarvestToolIndex+1)
                {
                    Inventory[SelectedSeedHarvestToolIndex]++;
                    goldAmount(-(SelectedSeedHarvestToolIndex+1));
                }
            }
            else if (SelectedSeedHarvestToolIndex >= 10 && SelectedSeedHarvestToolIndex < 20)
            {
                if (gold >= (SelectedSeedHarvestToolIndex-9) * 2.25f)
                {
                    Inventory[SelectedSeedHarvestToolIndex]++;
                    goldAmount(-(SelectedSeedHarvestToolIndex-9) * 2.25f);
                }
            }
            UpdateInventoryUI();
        }
    }

    public void SellItem()
    {
        if (SelectedSeedHarvestToolIndex >= 0 && SelectedSeedHarvestToolIndex < 10)
        { 
            if (Inventory[SelectedSeedHarvestToolIndex] > 0)
            {
                Inventory[SelectedSeedHarvestToolIndex]--;
                goldAmount((SelectedSeedHarvestToolIndex+1) * 0.75f);
            }
        }
        else if (SelectedSeedHarvestToolIndex >= 10 && SelectedSeedHarvestToolIndex < 20)
        {
            if (Inventory[SelectedSeedHarvestToolIndex] > 0)
            {
                Inventory[SelectedSeedHarvestToolIndex]--;
                goldAmount((SelectedSeedHarvestToolIndex-9) * 2f);
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
        if (currentSeed != null && Inventory[SelectedSeedHarvestToolIndex] > 0 && !field.IsPlanted()&&SelectedSeedHarvestToolIndex<10)
        {
            field.PlantSeed(currentSeed);
            Inventory[SelectedSeedHarvestToolIndex]--;
            UpdateInventoryUI();
            return true;
        }
        return false;
    }

    public void UpdateInventoryUI()
    {
        goldText.text = gold.ToString();
        for (int i = 0; i < 20; i++)
        {
            if (SeedHarvestToolUIs[i] != null)
            {
                TMP_Text countText = SeedHarvestToolUIs[i].GetComponentInChildren<TMP_Text>();
                countText.text = Inventory[i].ToString();
            }
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
                Inventory[plant.plantIndex-1]++;
                Inventory[plant.plantIndex+9]++;
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
        marketOpenButtonUI.SetActive(!marketOpenButtonUI.activeSelf);
        marketCloseButtonUI.SetActive(!marketCloseButtonUI.activeSelf);
        marketUI.SetActive(!marketUI.activeSelf);
    }
    
    void OnApplicationQuit()
    {
        saveLoadManager.SaveGame();
    }

    public void SaveGameData() {
        saveLoadManager.SaveGame();
    }

    public float GetGold()
    {
        return gold;
    }

    public void SetGold(float value)
    {
        gold = value;
        //UpdateInventoryUI();
    }

    //public List<int> GetSeedInventory()
    //{
    //    return new List<int>(seedInventory);
    //}

    //public void SetSeedInventory(List<int> inventory)
    //{
    //    seedInventory = inventory;
    //    //UpdateInventoryUI();
    //}

    //public List<int> GetHarvestInventory()
    //{
    //    return new List<int>(harvestInventory);
    //}

    //public void SetHarvestInventory(List<int> inventory)
    //{
    //    harvestInventory = inventory;
    //    //UpdateInventoryUI();
    //}   
    public List<int> GetInventory()
    {
        return new List<int>(Inventory);
    }
    public void SetInventory(List<int> inventory)
    {
        Inventory = inventory;
        //UpdateInventoryUI();
    }

    public GameObject GetPlantPrefab(int index)
    {
        switch (index)
        {
            case 1: return seeds[0];
            case 2: return seeds[1];
            case 3: return seeds[2];
            case 4: return seeds[3];
            case 5: return seeds[4];
            default: return null;
        }
    }
}
