using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipArea : MonoBehaviour
{
    [HideInInspector] public BoxCollider BoxCollider;

    private void Awake()
    {
        BoxCollider = GetComponent<BoxCollider>();
    }
}
