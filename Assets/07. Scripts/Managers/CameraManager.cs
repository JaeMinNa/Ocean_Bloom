using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject ShipCamera;

    // �ʱ�ȭ
    public void Init()
    {

    }

    // �޸� ����
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
