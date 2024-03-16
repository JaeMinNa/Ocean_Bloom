using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UI;

public class BoatController : MonoBehaviour
{
 
    public GameObject MovingShip;
    public GameObject wheel;
  public bool controlling;
  public PropellerBoats ship;
  bool forward = true;

    public GameObject moveui;

    public GameObject Player;

    
   
  public void OnTriggerEnter(Collider collider)
  {
    if(collider.transform.CompareTag("Player"))
    {

            moveui.SetActive(true);

      controlling = true;
            
      Debug.Log("플레이어 닿음");
    }
  }


    public void OnTriggerStay(Collider collider)
    {
        if(collider.transform.CompareTag("Player"))
        {
          

            if (controlling == true)
            {
                collider.transform.SetPositionAndRotation(wheel.transform.position, Player.transform.rotation);

                if(Input.GetKey(KeyCode.Backspace))
                {
                    controlling = false;
                }
            }

            //collider.transform.SetLocalPositionAndRotation(wheel.transform.position, wheel.transform.localRotation);
        }
    
    }


   

    public void OnTriggerExit(Collider collider)
  {
    if(collider.transform.CompareTag("Player"))
    {
      controlling = false;
            Player.transform.SetParent(MovingShip.transform);
            Debug.Log("플레이어 나감");
    }

        moveui.SetActive(false);
    }
   

  void Update()
  {
    

    if(controlling == true)
    {

      if (Input.GetKey(KeyCode.H))
      ship.RudderLeft();
    if (Input.GetKey(KeyCode.K))
      ship.RudderRight();

    if (forward)
    {
      if (Input.GetKey(KeyCode.N))
        ship.ThrottleUp();
      else if (Input.GetKey(KeyCode.J))
      {
        ship.ThrottleDown();
        ship.Brake();
      }
    }
    else
    {
      if (Input.GetKey(KeyCode.J))
        ship.ThrottleUp();
      else if (Input.GetKey(KeyCode.N))
      {
        ship.ThrottleDown();
        ship.Brake();
      }
    }

    if (!Input.GetKey(KeyCode.N) && !Input.GetKey(KeyCode.J))
      ship.ThrottleDown();

    if (ship.engine_rpm == 0 && Input.GetKeyDown(KeyCode.J) && forward)
    {
      forward = false;
      ship.Reverse();
    }
    else if (ship.engine_rpm == 0 && Input.GetKeyDown(KeyCode.N) && !forward)
    {
      forward = true;
      ship.Reverse();
    }

    }

    
  }

}
