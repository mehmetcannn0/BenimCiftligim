using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Animal animal = hit.collider.GetComponent<Animal>();
                if (animal != null)
                {
                    animal.ChangeState(Animal.State.Walk);
                }
                else
                {
                    Plant plant = hit.collider.GetComponent<Plant>();
                    if (plant != null)
                    {
                        if (gameManager.currentTool == gameManager.scytheTool && plant.growthStage == 2)
                        {
                            Field field = plant.GetComponentInParent<Field>();
                            if (field != null)
                            {
                                gameManager.HarvestPlant(field);
                            }
                        }
                        else if (gameManager.currentTool == gameManager.wateringCanTool && plant.growthStage == 1)
                        {
                            Field field = plant.GetComponentInParent<Field>();
                            if (field != null)
                            {
                                gameManager.WaterPlant(field);
                            }
                        }
                     
                    }
                    else
                    {
                        Field field = hit.collider.GetComponent<Field>();
                        if (field != null)
                        {
                            if (gameManager.currentTool == gameManager.scytheTool && field.GetCurrentPlant() != null)
                            {
                                Plant fieldPlant = field.GetCurrentPlant().GetComponent<Plant>();
                                if (fieldPlant != null && fieldPlant.growthStage == 2)
                                {
                                    gameManager.HarvestPlant(field);
                                }
                            }
                            else
                            {
                                gameManager.PlantSeedAtField(field);
                            }
                        }
                    }
                }
            }
            else
            {
                gameManager.SelectSeed(0);
                gameManager.currentTool = null;
            }
        }
    }
}
