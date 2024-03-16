using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public GameObject Ship;

    [Header("ShipUI")]
    public GameObject ShipControlUI;

    public void Init()
    {

    }

    public void Release()
    {

    }

    public void ShipSetPosition(Vector3 position)
    {
        Ship.transform.position = position;
    }

    //public void ShipSetRotation(Vector3 position)
    //{
    //    Ship.transform.rotation = Quaternion.Euler(position);
    //}

    public void CanShipMove()
    {
        GameManager.I.ShipManager.Ship.GetComponent<ShipJaeMin>().Rigidbody.freezeRotation = false;
        GameManager.I.ShipManager.Ship.GetComponent<ShipJaeMin>().Rigidbody.constraints = RigidbodyConstraints.None;
    }

    public void CanNotShipMove()
    { 

        GameManager.I.ShipManager.Ship.GetComponent<ShipJaeMin>().Rigidbody.freezeRotation = true;
        GameManager.I.ShipManager.Ship.GetComponent<ShipJaeMin>().Rigidbody.constraints |= RigidbodyConstraints.FreezePositionX;
        GameManager.I.ShipManager.Ship.GetComponent<ShipJaeMin>().Rigidbody.constraints |= RigidbodyConstraints.FreezePositionZ;
    }
}
