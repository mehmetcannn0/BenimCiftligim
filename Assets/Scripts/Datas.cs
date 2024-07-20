using System.Collections.Generic;
using UnityEngine; 

[System.Serializable]
public class GameData
{
    public float gold; 
    public List<int> Inventory;
    public List<int> FieldLocks;
    public List<PlantData> plants;
}

[System.Serializable]
public class PlantData
{
    public int plantIndex;
    public int growthStage;
    public int isWatered;
    public float timer;
    public long timestamp;

    public Vector3 position;
}
