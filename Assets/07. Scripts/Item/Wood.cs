using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<ResourceCollector>().CollectResource();
            GameManager.I.SoundManager.StartSFX("Wood");
            Destroy(gameObject);
        }
    }
}
