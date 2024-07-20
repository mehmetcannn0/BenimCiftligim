using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public MusicManager musicManager;


    void Update()
    {
       
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !gameManager.marketUI.activeSelf)
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero); 
            if (hit.collider != null)
            {
                HandleHit(hit.collider);
            }
            else
            { 
                gameManager.currentTool = null;
            }
        }
    }

    private void HandleHit(Collider2D collider)
    {
        Animal animal = collider.GetComponent<Animal>();
        if (animal != null)
        { 
            // hayvan sesi
            animal.ChangeState(Animal.State.Walk);
        }
        else
        {
            Plant plant = collider.GetComponent<Plant>();
            if (plant != null)
            { 
                HandlePlant(plant);
            }
            else
            {
                Field field = collider.GetComponent<Field>();
                if (field != null)
                { 
                    HandleField(field);
                }
            }
        }
    }

    private void HandlePlant(Plant plant)
    {
        if (gameManager.currentTool == gameManager.scytheTool && plant.growthStage == 2)
        {
            Field field = plant.GetComponentInParent<Field>();
            if (field != null)
            {
                musicManager.HarvestAudioClip();
                gameManager.HarvestPlant(field);
            }
        }
        else if (gameManager.currentTool == gameManager.wateringCanTool && plant.growthStage == 1)
        {
            Field field = plant.GetComponentInParent<Field>();
            if (field != null)
            {  
                musicManager.WateringAudioClip();
                gameManager.WaterPlant(field);
            }
        }
    }

    private void HandleField(Field field)
    {
        if (gameManager.currentTool == gameManager.scytheTool && field.GetCurrentPlant() != null)
        {
            Plant fieldPlant = field.GetCurrentPlant().GetComponent<Plant>();
            if (fieldPlant != null && fieldPlant.growthStage == 2)
            {
                musicManager.HarvestAudioClip();
                gameManager.HarvestPlant(field);
            }
        }
        else
        {
            gameManager.PlantSeedAtField(field);
        }
    }
}
