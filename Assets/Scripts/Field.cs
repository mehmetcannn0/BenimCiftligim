using UnityEngine;

public class Field : MonoBehaviour
{
    public bool isPlanted = false;
    public GameObject currentPlant = null;

    public float timer = 0f;

    void Update()
    {
        if (currentPlant != null && !isPlanted)
        {
            if (timer >= 1.5f)
            {
                Destroy(currentPlant);
                currentPlant = null;
                timer = 0f;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

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
        currentPlant = Instantiate(plant, transform.position, Quaternion.identity, transform);
    }

    public void ClearField()
    {
        isPlanted = false;
    }
}
