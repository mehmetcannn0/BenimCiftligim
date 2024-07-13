using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Týklandý");
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Animal animal = hit.collider.GetComponent<Animal>();
                if (animal != null)
                {
                    Debug.Log("Týklandý: Animal");
                    // Hayvan durum deðiþikliði
                    animal.ChangeState(Animal.State.Walk);
                }
                else
                {
                    Plant plant = hit.collider.GetComponent<Plant>();
                    if (plant != null)
                    {
                        Debug.Log("Týklandý: Plant -->"+ plant.growthStage.ToString());

                        // Bitki büyütme veya hasat etme
                        if (gameManager.currentTool == gameManager.scytheTool && plant.growthStage == 2)
                        {
                            //plant.Harvest();
                            Debug.Log("hasat zamaný");
                            Field field =  plant.GetComponentInParent<Field>();
                            if (field != null)
                            {
                                gameManager.HarvestPlant(field);
                            }
                            }
                        //else if (plant.growthStage == 0)
                        //{
                        //    plant.PlantSeed();
                        //}
                        else if (plant.growthStage == 1)
                        {
                            plant.Grow();
                        }
                    }
                    else
                    {
                        Field field = hit.collider.GetComponent<Field>();
                        if (field != null)
                        {
                            Debug.Log("Týklandý: Field");

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
                Debug.Log("Týklandý: Ground");
                gameManager.SelectSeed(0); // Bu satýrýn amacýný belirtmek gerek
                gameManager.currentTool = null;
            }
        }
    }
}
