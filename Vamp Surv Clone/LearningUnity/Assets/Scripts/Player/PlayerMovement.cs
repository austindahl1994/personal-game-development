using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //movement class for player


    public Rigidbody2D rb;
    public playerStats stats;

    Vector2 movement;
    Vector2 mousePos;
    private float moveSpeed;
    private float tempMoveSpeed;

    public Camera cam;
    private void Start()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        cam = Camera.main;
        moveSpeed = stats.speed;
    }

    void Update()
    {
        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        Vector2 lookDir = mousePos - rb.position;
    }

    public void updateSpeed(float speedChange) {
        moveSpeed = moveSpeed * speedChange;
    }

    public void slowPlayer() {
        StartCoroutine(slowDown());
    }

    public IEnumerator slowDown() {
        updateSpeed(0.50f);
        yield return new WaitForSeconds(3f);
        moveSpeed = stats.speed;
    }

}
