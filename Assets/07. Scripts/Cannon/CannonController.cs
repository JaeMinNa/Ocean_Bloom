using UnityEngine;
using System.Collections.Generic;

public class CannonController : MonoBehaviour
{
    public Cannon Cannon;
    public GameObject Player;
    public bool CannonControlling;

    void Update()
    {
        if (CannonControlling)
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                Cannon.Fire();
            }

            Cannon.AdjustRotation();
            Cannon.UpdateCannonUI();
        }
    }
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag("Player"))
        {

            CannonControlling = true;
        }
    }

    public void OnTriggerStay(Collider collider)
    {
        if (collider.transform.CompareTag("Player"))
        {
            //if (CannonControlling == true)
            //{
            //    collider.transform.SetPositionAndRotation(Cannon.transform.position, Player.transform.rotation);

            //    if (Input.GetKey(KeyCode.Backspace))
            //    {
            //        CannonControlling = false;
            //    }
            //}
        }
    }
    public void OnTriggerExit(Collider collider)
    {
        if (collider.transform.CompareTag("Player"))
        {
            CannonControlling = false;
            //Player.transform.SetParent(null);
        }

    }
}
