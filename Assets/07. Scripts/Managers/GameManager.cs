using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GameManager은 Manager을 관리하는 하나의 역할만 함


    public static GameManager I;

    [field: SerializeField] public DataManager DataManager { get; private set; }
    [field: SerializeField] public SoundManager SoundManager { get; private set; }
    [field : SerializeField] public PlayerManager PlayerManager { get; private set; }
    [field: SerializeField] public ObjectPoolManager ObjectPoolManager { get; private set; }
    [field: SerializeField] public CameraManager CameraManager { get; private set; }
    [field: SerializeField] public ShipManager ShipManager { get; private set; }
    [field: SerializeField] public ScenesManager SceneManager { get; private set; }


    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        DataManager.Init();
        SoundManager.Init();
        PlayerManager.Init();
        ObjectPoolManager.Init();
        CameraManager.Init();
        ShipManager.Init();
        SceneManager.Init();
    }

    private void Release()
    {
        DataManager.Release();
        SoundManager.Release();
        PlayerManager.Release();
        ObjectPoolManager.Release();
        CameraManager.Release();
        ShipManager.Release();
        SceneManager.Release();
    }
}
