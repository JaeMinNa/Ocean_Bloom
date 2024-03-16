using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject WoodPrefab;
    public int Hp = 12;
    public int WoodDropCount = 3;
    public int Index;

    private void Start()
    {
        Index = WoodDropCount;
    }

    private void Update()
    {
        if(Index <= 0)
        {
            DropWood();
            Index = WoodDropCount;
        }
        if(Hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void DropWood()
    {
        int randomIndex = Random.Range(0, 4);
        Vector3 randomPosition = new Vector3(2f, 3.5f, 2f);

        if (randomIndex == 0) randomPosition = new Vector3(2f, 3.5f, 2f);
        else if(randomIndex == 1) randomPosition = new Vector3(2f, 3.5f, -2f);
        else if (randomIndex == 2) randomPosition = new Vector3(-2f, 3.5f, 2f);
        else if (randomIndex == 3) randomPosition = new Vector3(-2f, 3.5f, -2f);
        
        Vector3 randomRotation = new Vector3(Random.Range(-90f, 90f), Random.Range(-90f, 90f), Random.Range(-90f, 90f));

        Instantiate(WoodPrefab, transform.position + randomPosition, Quaternion.Euler(randomRotation));
    }
}
