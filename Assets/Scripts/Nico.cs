using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nico : MonoBehaviour, IDamageable, ICollisive
{
    HitEffects myHitEffects;
    BulletHellShooter myBulletHellShooter;
    [SerializeField] Transform player;
    [SerializeField] float moveSpeed = 5f;
    float rotateSpeed = 2f;

    void Awake()
    {
        myHitEffects = GetComponent<HitEffects>();
        myBulletHellShooter = GetComponent<BulletHellShooter>();
    }

    void Start()
    {
        InvokeRepeating("Shoot", 1f, 3f);
    }

    void Shoot() => myBulletHellShooter.InstantiateBullets(-this.transform.up);

    void FixedUpdate()
    {
        FollowPlayer();
        RotateSpriteToFollowMouse();
    }

    void FollowPlayer()
    {
        this.transform.position = Vector3.MoveTowards(
                    this.transform.position,
                    Vector3.Lerp(this.transform.position, player.position, moveSpeed * Time.deltaTime),
                    moveSpeed * Time.deltaTime);
    }

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

    public void TakeDamage(int value) 
    {
        myHitEffects.PlayHitExpossionEffect();
    }

    public void Collided()
    {
        TakeDamage(0);
    }
}
