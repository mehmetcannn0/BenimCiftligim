using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public MusicManager musicManager;

    public GameObject[] seeds;
    public List<GameObject> SeedHarvestToolUI;
    public List<Sprite> SelectedSeedHarvestToolSprites;
    public List<Sprite> SeedHarvestToolSprites;

    public int SelectedSeedHarvestToolIndex;

    public List<GameObject> LocksUI;

    public GameObject goldUI;
    private TMP_Text goldText;
    public GameObject buyGoldUI;
    public GameObject sellGoldUI;

    public float buyPrice;
    public float sellPrice;

    public string harvestTime;
    public GameObject harvestTimeUI;
    public string waterTime;
    public GameObject waterTimeUI;
    private float waitingCoefficient = 60f;

    public Sprite previousSelectedSprite;
    public int previousSelectedIndex;

    public GameObject marketUI;
    public GameObject marketOpenButtonUI;
    public GameObject marketCloseButtonUI;

    private float gold = 0;

    private List<int> Inventory = new List<int> { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private List<int> FieldLocks = new List<int> { 1, 0, 0, 0 };

    public SaveLoadManager saveLoadManager;

    void Start()
    {
        saveLoadManager.LoadGame();
        goldText = goldUI.GetComponentInChildren<TMP_Text>();
        UpdateInventoryUI();
        UpdateFieldLocksUI();
        InvokeRepeating("SaveGameData", 20.0f, 20.0f);
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

        if (index != -1)
        {
            Image selectedImage = SeedHarvestToolUI[index].GetComponent<Image>();
            selectedImage.sprite = SelectedSeedHarvestToolSprites[index];

            if (SelectedSeedHarvestToolIndex != -1)
            {
                previousSelectedIndex = SelectedSeedHarvestToolIndex;
                SelectedSeedHarvestToolIndex = index;

                previousSelectedSprite = SeedHarvestToolSprites[previousSelectedIndex];
                Image previousselectedImage = SeedHarvestToolUI[previousSelectedIndex].GetComponent<Image>();
                previousselectedImage.sprite = previousSelectedSprite;
            }
            else
            {
                previousSelectedIndex = SelectedSeedHarvestToolIndex;
                SelectedSeedHarvestToolIndex = index;
            }
        }
        else
        {
            if (SelectedSeedHarvestToolIndex != -1)
            {
                previousSelectedIndex = SelectedSeedHarvestToolIndex;
                SelectedSeedHarvestToolIndex = index; //??

                previousSelectedSprite = SeedHarvestToolSprites[previousSelectedIndex];
                Image previousselectedImage = SeedHarvestToolUI[previousSelectedIndex].GetComponent<Image>();
                previousselectedImage.sprite = previousSelectedSprite;
            }
        }
        if (marketUI.activeSelf)
        {
            CalculatePriceAndTime();
        }
    }

    public void CalculatePriceAndTime()
    {
        if (SelectedSeedHarvestToolIndex != -1)
        {
            //seeds buy and sell prices
            if (SelectedSeedHarvestToolIndex >= 0 && SelectedSeedHarvestToolIndex < 10)
            {
                harvestTime = (((SelectedSeedHarvestToolIndex + 2) * ((SelectedSeedHarvestToolIndex + 1) * waitingCoefficient)) / 60) + " mins";
                waterTime = ((SelectedSeedHarvestToolIndex + 1) * waitingCoefficient / 60) + " mins";
                buyPrice = (SelectedSeedHarvestToolIndex + 1);
                sellPrice = (SelectedSeedHarvestToolIndex + 1) * 0.75f;

            }
            //harvests buy and sell prices
            else if (SelectedSeedHarvestToolIndex >= 10 && SelectedSeedHarvestToolIndex < 20)
            {
                harvestTime = (((SelectedSeedHarvestToolIndex + 2 - 10) * ((SelectedSeedHarvestToolIndex + 1 - 10) * waitingCoefficient)) / 60) + " mins";
                waterTime = ((SelectedSeedHarvestToolIndex + 1 - 10) * waitingCoefficient / 60) + " mins";
                buyPrice = ((SelectedSeedHarvestToolIndex - 9) * ((SelectedSeedHarvestToolIndex - 9) / 10f) * 6.5f) + 3;
                sellPrice = ((SelectedSeedHarvestToolIndex - 9) * ((SelectedSeedHarvestToolIndex - 9) / 10f) * 6) + 1;

            }
        }
        else
        {
            buyPrice = 0;
            sellPrice = 0;

        }

        UpdateBuyAndSellPriceUI();
    }

    public void UpdateBuyAndSellPriceUI()
    {
        TMP_Text price = buyGoldUI.GetComponentInChildren<TMP_Text>();
        price.text = buyPrice.ToString();
        price = sellGoldUI.GetComponentInChildren<TMP_Text>();
        price.text = sellPrice.ToString();

        TMP_Text time = harvestTimeUI.GetComponentInChildren<TMP_Text>();
        time.text = harvestTime;
        time = waterTimeUI.GetComponentInChildren<TMP_Text>();
        time.text = waterTime;
    }
        public void BuyItem()
    {
        if (gold >= buyPrice && SelectedSeedHarvestToolIndex != -1)
        {
            musicManager.LoseCoinAudioClip();
            Inventory[SelectedSeedHarvestToolIndex]++;
            goldAmount(-buyPrice);
        }
        UpdateInventoryUI();
    }

    public void BuyFields(int index)
    {
        if (gold >= index * 100)
        {
            musicManager.LoseCoinsAudioClip();
            goldAmount(-(index * 100));
            FieldLocks[index] = 1;
            UpdateFieldLocksUI();
        }
    }

    public void SellItem()
    {
        if (SelectedSeedHarvestToolIndex >= 0 && SelectedSeedHarvestToolIndex < 10)
        {
            if (Inventory[SelectedSeedHarvestToolIndex] > 0)
            {
                musicManager.GainCoinAudioClip();
                Inventory[SelectedSeedHarvestToolIndex]--;
                goldAmount(sellPrice);
            }
        }
        else if (SelectedSeedHarvestToolIndex >= 10 && SelectedSeedHarvestToolIndex < 20)
        {
            if (Inventory[SelectedSeedHarvestToolIndex] > 0)
            {
                musicManager.GainCoinAudioClip();
                Inventory[SelectedSeedHarvestToolIndex]--;
                goldAmount(sellPrice);
            }
        }
        UpdateInventoryUI();
    }

    public void goldAmount(float amount)
    {
        gold += amount;
        goldText.text = gold.ToString();
    }

    public bool PlantSeedAtField(Field field)
    {
        if (Inventory[SelectedSeedHarvestToolIndex] > 0 && !field.IsPlanted() && SelectedSeedHarvestToolIndex < 10)
        {
            musicManager.PlantAudioClip();
            field.PlantSeed(seeds[SelectedSeedHarvestToolIndex]);
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
            if (SeedHarvestToolUI[i] != null)
            {
                TMP_Text countText = SeedHarvestToolUI[i].GetComponentInChildren<TMP_Text>();
                countText.text = Inventory[i].ToString();
            }
        }


    }
    public void UpdateFieldLocksUI()
    {
        for (int i = 0; i < LocksUI.Count; i++)
        {
            if (LocksUI[i] != null)
            {
                LocksUI[i].gameObject.SetActive(FieldLocks[i] != 1);
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
                Inventory[plant.plantIndex - 1]++;
                Inventory[plant.plantIndex + 9]++;
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
        CalculatePriceAndTime();
    }

    void OnApplicationQuit()
    {
        saveLoadManager.SaveGame();
    }

    public void SaveGameData()
    {
        saveLoadManager.SaveGame();

    }

    public float GetGold()
    {
        return gold;
    }

    public void SetGold(float value)
    {
        gold = value;
    }

    public List<int> GetInventory()
    {
        return new List<int>(Inventory);
    }
    public void SetInventory(List<int> inventory)
    {
        Inventory = inventory;
    }
    public List<int> GetFieldLocks()
    {
        return new List<int>(FieldLocks);
    }
    public void SetFieldLocks(List<int> fieldLocks)
    {
        FieldLocks = fieldLocks;
    }
    public GameObject GetPlantPrefab(int index)
    {
        if (index >= 1 && index <= 10)
        {
            return seeds[index - 1];
        }
        return null;
    }
}
