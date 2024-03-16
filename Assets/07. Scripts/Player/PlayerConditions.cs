using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class Condition
{
    [HideInInspector]
    public float CurValue;
    public float MaxValue;
    public float StartValue;
    public float RegenRate;
    public Image HP_Bar;

    public void Add(float amount)
    {
        CurValue = Mathf.Min(CurValue + amount, MaxValue);
    }

    public void Subtract(float amount)
    {
        CurValue = Mathf.Max(CurValue - amount, 0.0f);
    }
    public void Regenerate()
    {        
        CurValue = Mathf.Min(CurValue + RegenRate * Time.deltaTime, MaxValue);
    }

    public float GetPercentage()
    {
        return CurValue / MaxValue;
    }
}

public class PlayerConditions : MonoBehaviour
{
    public Condition Health;    

    void Start()
    {
        // health�� startValue�� �ܺο��� ������ hp ������ �ʱ�ȭ
        Health.StartValue = GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CurrentHp;
        Health.CurValue = Health.StartValue;
    }

    void Update()
    {
        if (Health.CurValue == 0.0f)
            Die();

        Health.Regenerate();
        Health.HP_Bar.fillAmount = Health.GetPercentage();
    }

    public void Heal(float amount)
    {
        Health.Add(amount);
    }

    public void Die()
    {
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().Die();
    }    
}
