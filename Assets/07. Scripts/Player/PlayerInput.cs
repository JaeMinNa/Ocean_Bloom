using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputActions InputActions { get; private set; }
    public PlayerInputActions.PlayerActions PlayerActions { get; private set; }	// �츮�� ���� Actions

    private void Awake()
    {
        InputActions = new PlayerInputActions();

        PlayerActions = InputActions.Player;
    }

    // Input ����� ��
    private void OnEnable()
    {
        InputActions.Enable();
    }

    // Input ����� ��
    private void OnDisable()
    {
        InputActions.Disable();
    }

}