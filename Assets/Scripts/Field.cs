using UnityEngine;

public class Field : MonoBehaviour
{
    public bool isPlanted = false;
    public GameObject currentPlant = null;
     
    public bool IsPlanted()
    {
        return isPlanted;
    }

    public GameObject GetCurrentPlant()
    {
        return currentPlant;
    }

    public void PlantSeed(GameObject plant)
    {
        isPlanted = true;
        currentPlant = Instantiate(plant, transform.position, Quaternion.identity,transform);
    }

    public void ClearField()
    {
        isPlanted = false;
        if (currentPlant != null)
        {
            Destroy(currentPlant);
        }
        currentPlant = null;
    }
    public void Harvest()
    {
        if (currentPlant != null)
        {
            Destroy(currentPlant);
            currentPlant = null;
            isPlanted = false;
        }
    }
}
