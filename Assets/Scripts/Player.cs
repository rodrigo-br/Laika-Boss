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
    Rigidbody2D myRigidBody;

    void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        shooter = GetComponent<Shooter>();
    }

    void FixedUpdate()
    {
        Move();
        RotateSpriteToFollowMouse();
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

    void Move() => myRigidBody.velocity += inputValue * moveSpeed;

    void OnMove(InputValue value) => inputValue = value.Get<Vector2>();

    void OnFire(InputValue value) => shooter.IsFiring = value.isPressed;
}
