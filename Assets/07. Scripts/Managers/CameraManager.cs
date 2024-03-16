using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject ShipCamera;

    // 초기화
    public void Init()
    {

    }

    // 메모리 해제
    public void Release()
    {

    }

    public void ChangeShipCamera()
    {
        ShipCamera.SetActive(true);
        MainCamera.GetComponent<Camera>().enabled = false;
    }

    public void ChangeMainCamera()
    {
        MainCamera.GetComponent<Camera>().enabled = true;
        ShipCamera.SetActive(false);
    }
}
