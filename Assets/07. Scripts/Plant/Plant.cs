using UnityEngine;

public class Plant : MonoBehaviour
{
    public int PlantID; // 플랜트의 고유한 식별자
    public PlantBook PlantBook;

    void Start()
    {
                
    }

    void Update()
    {
        
    }

    void OnDestroy()
    {        
        switch (PlantID)
        {
            case 1:
                if(PlantID == 1)
                {
                    PlantBook.FindPlants[0] = 1;
                    PlantBook.GameClear();
                }
                break;
            case 2:
                if(PlantID == 2)
                {
                    PlantBook.FindPlants[1] = 1;
                    PlantBook.GameClear();
                }
                break;
            case 3:
                if(PlantID == 3)
                {
                    PlantBook.FindPlants[2] = 1;
                    PlantBook.GameClear();
                }
                break;
            case 4:
                if(PlantID == 4)
                {
                    PlantBook.FindPlants[3] = 1;
                    PlantBook.GameClear();
                }
                break;
            case 5:
                if(PlantID == 5)
                {
                    PlantBook.FindPlants[4] = 1;
                    PlantBook.GameClear();
                }
                break;
            case 6:
                if(PlantID == 6)
                {
                    PlantBook.FindPlants[5] = 1;
                    PlantBook.GameClear();
                }
                break;
            
            default:
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.Q))
            {    
                Destroy(gameObject);
                GameManager.I.SoundManager.StartSFX("Plant");   
            }
        }
    }
}