using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public GameObject[] DropItems;
    public int Hp = 5;
    [SerializeField] private int _itemMaxCount = 3;

    private void Update()
    {
        if (Hp <= 0)
        {
            DropItem();
            Destroy(gameObject);
        }
    }

    private void DropItem()
    {
        foreach (GameObject item in DropItems)
        {
            int randomIndex = Random.Range(1, _itemMaxCount + 1);

            for (int i = 0; i < randomIndex; i++)
            {
                Vector3 randomPosition = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(1f, 2f), Random.Range(-0.5f, 0.5f));
                Vector3 randomRotation = new Vector3(0, Random.Range(-90f, 90f), 0);
                Instantiate(item, transform.position + randomPosition, Quaternion.Euler(randomRotation));
            }
        }
    }
}
