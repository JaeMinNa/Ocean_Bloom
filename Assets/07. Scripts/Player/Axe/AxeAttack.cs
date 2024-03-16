using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Tree"))
        {
            GameManager.I.SoundManager.StartSFX("AxeTree");
            collision.GetComponent<Tree>().Hp--;
            collision.GetComponent<Tree>().Index--;
        }
        else if(collision.CompareTag("TreasureBox"))
        {
            GameManager.I.SoundManager.StartSFX("AxeTree");
            collision.GetComponent<TreasureBox>().Hp--;
        }
    }
}
