using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinimapUI : MonoBehaviour
{
    [SerializeField] private Camera minimapCamera;
    //[SerializeField] private float zoomIn = 1; // ī�޶��� orthographicSize �ּҰ�
    //[SerializeField] private float zoomMax = 30; // ī�޶��� orthographicSize �ִ밪
    //[SerializeField] private float zoomOneStep = 1; //1ȸ ���Ҷ� �����Ǵ� ��ġ
    [SerializeField] private TextMeshProUGUI textMapName;

    private void Awake()
    {
        //�� �̸��� ���� �� �̸����� ����(���ϴ� �̸�����)
        //textMapName.text = SceneManager.GetActiveScene().name;
    }

    //public void ZoomIn()
    //{
    //    //ī�޶��� orthographicSize ���� ���ҽ��� ī�޶� ���̴� �繰�� ũ�� Ȯ��
    //    minimapCamera.orthographicSize = Mathf.Max(minimapCamera.orthographicSize - zoomOneStep, zoomIn);
    //}

    //public void ZoomOut()
    //{
    //    //ī�޶��� orthographicSize ���� �������� ī�޶� ���̴� �繰�� ũ�� ���
    //    minimapCamera.orthographicSize = Mathf.Max(minimapCamera.orthographicSize + zoomOneStep, zoomIn);
    //}
}
