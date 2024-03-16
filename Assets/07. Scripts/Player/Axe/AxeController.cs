using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public bool IsAttack;
    public Animator Animator { get; private set; }
    [SerializeField] private GameObject _axeCollider;
    [SerializeField] private GameObject _axe;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        IsAttack = false;
    }

    private void Update()
    {
        Debug.Log(IsAttack);
    }

    public void AxeColliderActive()
    {
        _axeCollider.gameObject.SetActive(true);
        StartCoroutine(COAxeColliderInActive());
    }

    IEnumerator COAxeColliderInActive()
    {
        yield return new WaitForSeconds(0.5f);
        _axeCollider.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        IsAttack = false;
    }
}
