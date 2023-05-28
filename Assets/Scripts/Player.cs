using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
using System;

public class Player : MonoBehaviour, IDamageable, ICollisive, IDimensionTraveler
{
    [SerializeField] Transform healthBarPrefab;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float rotateSpeed = 5f;
    [SerializeField] int maxHealth = 30;
    [SerializeField] Animator assAnimator;
    [SerializeField] Sprite[] shipSprites;
    [SerializeField] Light2D[] myFreeFormLights;
    [SerializeField] GameObject shield;
    string[] layer = new string[] {"MainDimensionPlayer", "OtherDimensionPlayer"};
    Color[] colors = new Color[] {Color.blue, Color.red};
    Animator myAnimator;
    SpriteRenderer mySpriteRenderer;
    DimensionManager dimensionManager;
    CameraShake cameraShake;
    HitEffects hitEffects;
    HealthSystem healthSystem;
    Shooter shooter;
    Vector2 inputValue = Vector2.zero;
    Rigidbody2D myRigidBody;
    public bool IsMainDimension { get; private set; }
    BulletHellShooter bulletHellShooter;

    #region Unity Methods
    void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        shooter = GetComponent<Shooter>();
        hitEffects = GetComponent<HitEffects>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
        dimensionManager = FindObjectOfType<DimensionManager>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        bulletHellShooter = GetComponent<BulletHellShooter>();
    }

    void Start()
    {
        Transform healthBarTransform = Instantiate(healthBarPrefab, new Vector3(0, 0.1f), Quaternion.identity);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthSystem = new HealthSystem(maxHealth);

        healthBar.Setup(healthSystem, this.transform);
        healthBar.OnHealthReachesZero += HealthBar_OnHealthReachesZero;
        dimensionManager.OnDimensionChange += DimensionManager_OnDimensionChange;
    }

    void DimensionManager_OnDimensionChange(object sender, bool value)
    {
        DimensionChecker();
    }

    void FixedUpdate()
    {
        Move();
        RotateSpriteToFollowMouse();
    }

    void OnMove(InputValue value) => inputValue = value.Get<Vector2>();

    void OnFire(InputValue value) => shooter.IsFiring = value.isPressed;

    #endregion

    void RotateSpriteToFollowMouse()
    {
        Vector3 playerPos = Camera.main.WorldToScreenPoint(this.transform.position);
        Vector3 mousePos = Input.mousePosition - playerPos;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.RotateTowards(
            this.transform.rotation,
            Quaternion.Euler(0, 0, angle + 270),
            rotateSpeed);
    }

    void Move()
    {
        myRigidBody.velocity += inputValue * moveSpeed;
        if (myRigidBody.velocity.sqrMagnitude > 100f)
        {
            assAnimator.SetTrigger("Boosting");
        }
    }

    public void Heal() => healthSystem.Heal(20);

    public void FireSpeed()
    {
        StartCoroutine(IncreaseFireRateRoutine());
    }

    IEnumerator IncreaseFireRateRoutine()
    {
        shooter.BuffFireCooldown();
        yield return new WaitForSecondsRealtime(10f);
        shooter.ResetFireCooldown();
    }

    void HealthBar_OnHealthReachesZero(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
        SceneManager.LoadScene(2);
    }

    #region Interfaces Methods
    public void TakeDamage(int value) => Damaged(value);

    public void Collided() => Damaged(5);

    void Damaged(int value)
    {
        healthSystem?.Damage(value);
        hitEffects?.PlayHitExpossionEffect();
        cameraShake?.Play();
    }

    public void DimensionChecker() 
    {
        IsMainDimension = dimensionManager.mainDimension;
        bulletHellShooter.InstantiateBullets(-transform.up);
        myAnimator.SetBool("isMainDimension", IsMainDimension);
        myAnimator.SetTrigger("ChangeDimension");
        int index = IsMainDimension ? 0 : 1;
        foreach (Light2D freeFormLight in myFreeFormLights)
        {
            freeFormLight.color = colors[index];
        }
        this.gameObject.layer = shield.layer = LayerMask.NameToLayer(layer[index]);
    }

    #endregion

    void OnDisable()
    {
        dimensionManager.OnDimensionChange -= DimensionManager_OnDimensionChange;
    }

    public void shieldOn() => shield.SetActive(true);

}
