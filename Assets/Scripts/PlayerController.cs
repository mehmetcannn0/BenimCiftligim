using UnityEngine;
using UnityEngine.UI;

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
                if (gameManager.SelectedSeedHarvestToolIndex == 22)
                {
                    gameManager.SelectItem(-1);
                }
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
        Field field = plant.GetComponentInParent<Field>();
        if (field != null)
        {
            if (gameManager.SelectedSeedHarvestToolIndex == 20 && plant.growthStage == 2)
            {
                musicManager.HarvestAudioClip();
                gameManager.HarvestPlant(field);

            }
            else if (gameManager.SelectedSeedHarvestToolIndex == 21 && plant.growthStage == 1)
            {
                gameManager.WaterPlant(field);

            }
            else if (gameManager.SelectedSeedHarvestToolIndex == 22 && plant.growthStage == 1)
            {
                musicManager.PlantAudioClip();
                field.ClearField();
            }
        }

    }

    private void HandleField(Field field)
    {
        if (gameManager.SelectedSeedHarvestToolIndex == 20 && field.GetCurrentPlant() != null)
        {
            Plant fieldPlant = field.GetCurrentPlant().GetComponent<Plant>();
            if (fieldPlant != null && fieldPlant.growthStage == 2)
            {
                musicManager.HarvestAudioClip();
                gameManager.HarvestPlant(field);
            }
        }
        else if (gameManager.SelectedSeedHarvestToolIndex >= 0 && gameManager.SelectedSeedHarvestToolIndex <= 19)
        {
            gameManager.PlantSeedAtField(field);
        }
    }
}
