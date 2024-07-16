using System.Collections.Generic;
using UnityEngine; 

[System.Serializable]
public class GameData
{
    public float gold;
    public List<int> seedInventory;
    public List<int> harvestInventory;
    public List<FieldData> fields;
}

[System.Serializable]
public class FieldData
{
    public int plantIndex;
    public int growthStage;
    public int isWatered;
    public float timer;
    public Vector3 position;
}
