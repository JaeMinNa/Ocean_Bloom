using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGather : MonoBehaviour
{
    public float lineSize = 16f;
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
        if (Input.GetKey(KeyCode.Q))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, lineSize))
            {
                if (hit.collider.CompareTag("Plant"))
                {
                    Destroy(hit.collider.gameObject);
                    GameManager.I.SoundManager.StartSFX("Plant");
                }
            }
        }

    }
}