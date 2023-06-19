using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DimensionManager : MonoBehaviour
{
    public event EventHandler<bool> OnDimensionChange;
    [SerializeField] float changeCooldown = 10f;
    [SerializeField] GameObject dimensionLight;
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
        dimensionLight.SetActive(false);
        yield return new WaitForSecondsRealtime(changeCooldown);
        isOnCooldown = false;
        dimensionLight.SetActive(true);
    }
}
