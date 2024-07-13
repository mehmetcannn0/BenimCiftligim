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

 
    //public GameObject emptyUI;  

    public List<GameObject> SeedUIs;

    public GameObject selectItem;

    public GameObject currentSeed; // �u an se�ili olan tohum
    public int currentSeedIndex; 



    public Dictionary<GameObject, int> seedInventory = new Dictionary<GameObject, int>();

    void Start()
    {
        // Ba�lang�� tohum miktarlar�n� ayarla
        seedInventory[seed1] = 10;
        seedInventory[seed2] = 10;
        seedInventory[seed3] = 10;
        seedInventory[seed4] = 10;
        seedInventory[seed5] = 10;
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
                selectItem.transform.position = SeedUIs[seedNumber].transform.position;
                break;
            case 1:
                currentSeed = seed1;
                selectItem.transform.position = SeedUIs[seedNumber].transform.position;
                break;
            case 2:
                currentSeed = seed2;
                selectItem.transform.position = SeedUIs[seedNumber].transform.position;
                break;
            case 3:
                currentSeed = seed3;
                selectItem.transform.position = SeedUIs[seedNumber].transform.position;
                break;
            case 4:
                currentSeed = seed4;
                selectItem.transform.position = SeedUIs[seedNumber].transform.position;


                break; 
            case 5:
                currentSeed = seed5;
                selectItem.transform.position = SeedUIs[seedNumber].transform.position;
                break;
            default:
                currentSeed = null;
                selectItem.transform.position = SeedUIs[seedNumber].transform.position;

                break;
        }
    }


    public bool PlantSeedAtField(Field field)
    {
        if (currentSeed != null && seedInventory[currentSeed] > 0 && !field.IsPlanted())
        {
            field.PlantSeed(currentSeed);
            seedInventory[currentSeed]--;
            UpdateInventoryUI();
            return true;
        }
        return false;
    }

    public void UpdateInventoryUI()
    {
        //SeedUIs[currentSeedIndex].transform.FindChild("Text").GetComponent<TextMeshPro>().text = seedInventory[seed1];

      

        for (int i = 1; i < SeedUIs.Count; i++)
        {
            TMP_Text seedText = SeedUIs[i].GetComponentInChildren<TMP_Text>();

            
            switch (i)
            {
                case 0:
                    seedText.text = "Empty";
                    break;
                case 1:
                    seedText.text = seedInventory[seed1].ToString();
                    break;
                case 2:
                    seedText.text = seedInventory[seed2].ToString();
                    break;
                case 3:
                    seedText.text = seedInventory[seed3].ToString();
                    break;
                case 4:
                    seedText.text = seedInventory[seed4].ToString();
                    break;
                case 5:
                    seedText.text = seedInventory[seed5].ToString();
                    break;
                default:

                    break;
            }
        }
    }

 
}
