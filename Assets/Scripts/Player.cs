using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float rotateSpeed = 5f;
    Shooter shooter;
    Vector2 inputValue = Vector2.zero;
    Vector2 deltaPosition;
    Rigidbody2D myRigidBody;
    Vector2 minBounds;
    Vector2 maxBounds;
    Camera cam;

    void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        shooter = GetComponent<Shooter>();
    }

    void Start()
    {
        cam = Camera.main;
        minBounds = cam.ViewportToWorldPoint(Vector2.zero);
        maxBounds = cam.ViewportToWorldPoint(Vector2.one);
    }

    void FixedUpdate()
    {
        Move();
        RotateSpriteToFollowMouse();
    }

    void RotateSpriteToFollowMouse()
    {
        Vector3 playerPos = cam.WorldToScreenPoint(this.transform.position);
        Vector3 mousePos = Input.mousePosition - playerPos;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.RotateTowards(
            this.transform.rotation,
            Quaternion.Euler(0, 0, angle + 270),
            rotateSpeed);
    }

    void Move() => myRigidBody.velocity += inputValue * moveSpeed;

    void OnMove(InputValue value)
    {
        inputValue = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.IsFiring = value.isPressed;
        }
    }
}
