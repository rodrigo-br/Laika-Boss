using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IDimensionTraveler
{
    [SerializeField] GameObject projectilePrefab;
    [Tooltip("Use a multiple of .2")][SerializeField] float fireCooldown = 0.2f;
    [SerializeField] bool isIA = true;
    [SerializeField] bool isMainDimension = true;
    public bool IsMainDimension { get => isMainDimension; }
    float fireCooldownDefaultValue;
    bool finishfireCooldown = true;
    bool isFiring;
    DimensionManager dimensionManager;
    public bool IsFiring
    {
        get { return isFiring; }
        set
        {
            isFiring = value;
            if (isFiring && finishfireCooldown)
            {
                StartCoroutine(FireCoroutine());
            }
        }
    }

    void Awake()
    {
        dimensionManager = FindObjectOfType<DimensionManager>();
    }

    void Start()
    {
        dimensionManager.OnDimensionChange += DimensionManager_OnDimensionChange;
        fireCooldownDefaultValue = fireCooldown;
        DimensionChecker();
    }

    void OnDisable()
    {
        dimensionManager.OnDimensionChange -= DimensionManager_OnDimensionChange;
    }

    void DimensionManager_OnDimensionChange(object sender, bool value)
    {
        DimensionChecker();
    }

    IEnumerator FireCoroutine()
    {
        finishfireCooldown = false;
        while (isFiring && !PauseManager.IsPaused)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, transform.rotation);
            if (!isIA)
            {
                instance.layer = LayerMask.NameToLayer(dimensionManager.mainDimension ? "MainDimensionPlayer" : "OtherDimensionPlayer");
            }
            yield return new WaitForSecondsRealtime(fireCooldown);
        }
        finishfireCooldown = true;
    }

    public void BuffFireCooldown() => fireCooldown /= 2f;

    public void ResetFireCooldown() => fireCooldown *= 2f;

    public void DimensionChecker() 
    {
        if (isIA)
        {
            IsFiring = (IsMainDimension == dimensionManager.mainDimension);
        }
    }
}
