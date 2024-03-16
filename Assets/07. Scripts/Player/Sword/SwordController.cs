using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public Animator Animator { get; private set; }
    [SerializeField] private PlayerController _playerController;
    public GameObject _SwordCollider;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public void SwordColliderActive()
    {
        GameManager.I.SoundManager.StartSFX("PlayerSword");
        _SwordCollider.gameObject.SetActive(true);
        StartCoroutine(COSwordColliderInActive());
    }

    IEnumerator COSwordColliderInActive()
    {
        yield return new WaitForSeconds(0.5f);
        _SwordCollider.gameObject.SetActive(false);
    }
}
