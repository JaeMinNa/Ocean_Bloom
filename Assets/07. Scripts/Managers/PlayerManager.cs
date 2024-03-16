using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public GameObject Player;
    
    public void Init()
    {

    }

    public void Release()
    {

    }

    public void PlayerSetPosition(Vector3 position)
    {
        Player.transform.position = position;
    }

    public void PlayerSetRotation(Vector3 position)
    {
        Player.transform.rotation = Quaternion.Euler(position);
    }
}
