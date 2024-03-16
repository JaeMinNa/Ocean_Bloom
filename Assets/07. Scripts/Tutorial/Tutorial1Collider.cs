using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1Collider : MonoBehaviour
{
    private Tutorial1 _firstIslandTutorial;

    private void Start()
    {
        _firstIslandTutorial = transform.parent.gameObject.GetComponent<Tutorial1>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (transform.name == "Area1" && !_firstIslandTutorial.IsTutorialActive[1])
        {
            if (other.CompareTag("Player")) _firstIslandTutorial.TutorialActive(1);
        }
        else if (transform.name == "Area2" && !_firstIslandTutorial.IsTutorialActive[2])
        {
            if (other.CompareTag("Player")) _firstIslandTutorial.TutorialActive(2);
        }
        else if (transform.name == "Area3" && !_firstIslandTutorial.IsTutorialActive[3])
        {
            if (other.CompareTag("Player")) _firstIslandTutorial.TutorialActive(3);
        }
        else if (transform.name == "Area4" && !_firstIslandTutorial.IsTutorialActive[4])
        {
            if (other.CompareTag("Player")) _firstIslandTutorial.TutorialActive(4);
        }
    }
}
