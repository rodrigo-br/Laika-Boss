using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DimensionManager : MonoBehaviour
{
    public event EventHandler<bool> OnDimensionChange;
    [SerializeField] float changeCooldown = 5f;
    public bool mainDimension { get; private set; } = true;
    bool isOnCooldown = false;

    void Update()
    {
        if (!isOnCooldown && Input.GetKeyDown("e"))
        {
            mainDimension = !mainDimension;
            StartCoroutine(CooldownRoutine());
            OnDimensionChange?.Invoke(this, mainDimension);
        }
    }

    IEnumerator CooldownRoutine()
    {
        isOnCooldown = true;
        yield return new WaitForSecondsRealtime(changeCooldown);
        isOnCooldown = false;
    }
}
