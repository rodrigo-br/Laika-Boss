using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, ICollisive, IDimensionTraveler
{
    [SerializeField] float rotateSpeed = 5f;
    [SerializeField] GameObject drop;
    [SerializeField] int dropChance = 10;
    [SerializeField] bool isMainDimension = true;
    public bool IsMainDimension { get => isMainDimension ; }
    DimensionManager dimensionManager;
    HealthSystem healthSystem;
    HitEffects hitEffects;
    Player player;
    public bool isActive { get; private set; }

    #region Unity Methods

    void Awake()
    {
        hitEffects = GetComponent<HitEffects>();
        player = FindObjectOfType<Player>();
        dimensionManager = FindObjectOfType<DimensionManager>();
    }

    void Start()
    {
        healthSystem = new HealthSystem(20);

        healthSystem.OnHealthChange += HealthSystem_OnHealthChanged;
        if (player == null) { return; }
        Vector2 target = player.transform.position - this.transform.position;
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0, 0, angle + 270);
        DimensionChecker();
        dimensionManager.OnDimensionChange += DimensionManager_OnDimensionChange;
    }

    void DimensionManager_OnDimensionChange(object sender, bool value)
    {
        DimensionChecker();
    }

    void LateUpdate()
    {
        if (player == null || !isActive)
        {
            return ;
        }
        Vector2 target = player.transform.position - this.transform.position;
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.RotateTowards(
            this.transform.rotation,
            Quaternion.Euler(0, 0, angle + 270),
            rotateSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ICollisive collisive = other.GetComponent<ICollisive>();
        if (collisive != null)
        {
            collisive.Collided();
            this.Collided();
        }
    }

    void OnDisable()
    {
        if (healthSystem != null)
        {
            healthSystem.OnHealthChange -= HealthSystem_OnHealthChanged;
        }
        if (dimensionManager != null)
        {
            dimensionManager.OnDimensionChange -= DimensionManager_OnDimensionChange;
        }
    }

    #endregion

    #region Interfaces Methods
    public void TakeDamage() => Damaged(10);

    public void Collided() => Damaged(20);

    public void DimensionChecker() 
    {
        isActive = (IsMainDimension == dimensionManager.mainDimension);
    }

    void Damaged(int value)
    {
        healthSystem.Damage(value);
        hitEffects.PlayHitExpossionEffect();
    }

    #endregion

    void HealthSystem_OnHealthChanged(object sender, int health)
    {
        if (health <= 0)
        {
            int value = Random.Range(0, 100);
            if (value < dropChance)
            {
                Instantiate(drop, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
