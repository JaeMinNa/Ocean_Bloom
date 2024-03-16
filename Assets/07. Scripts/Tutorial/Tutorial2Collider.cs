using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial2Collider : MonoBehaviour
{
    private Tutorial2 _islandTutorial;

    private void Start()
    {
        _islandTutorial = transform.parent.gameObject.GetComponent<Tutorial2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(transform.name == "Area3" && !_islandTutorial.IsTutorialActive[3])
        {
            if (other.CompareTag("Player") && PlayerPrefs.GetInt("Tutorial2TreasureBox") != 1)
            {
                _islandTutorial.TutorialActive(3);
            }
        }
        if (transform.name == "Area4" && !_islandTutorial.IsTutorialActive[4])
        {
            if (other.CompareTag("Player")) _islandTutorial.TutorialActive(4);
        }
        else if (transform.name == "Area5" && !_islandTutorial.IsTutorialActive[5])
        {
            if (other.CompareTag("Player")) _islandTutorial.TutorialActive(5);
        }
        else if (transform.name == "Area6" && !_islandTutorial.IsTutorialActive[6])
        {
            if (other.CompareTag("Player") && PlayerPrefs.GetInt("Tutorial2Partner") != 1)
            {
                _islandTutorial.TutorialActive(6);
                _islandTutorial.Area6Collider.SetActive(false);
                GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().PartnerSpawnCount += 5;
                GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().PartnerSpawnCountText.text =
                    (GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().PartnerSpawnCount).ToString();
                PlayerPrefs.SetInt("Tutorial2Partner", 1);
            }
        }
    }
}
