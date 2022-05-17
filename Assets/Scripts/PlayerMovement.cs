using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float power = 10f;

    [SerializeField] private Vector2 maxPower;
    [SerializeField] private Vector2 minPower;

    [SerializeField] private LayerMask jumpableGround;

    private Rigidbody2D rb;

    private LineTrajectory tl;

    private Collider2D col;

    private Camera camera;

    private Vector2 force;

    private Vector3 startPoint;
    private Vector3 endPoint;


    private bool addForce = false;


    void Start()
    {
        camera = Camera.main;
        rb = this.GetComponent<Rigidbody2D>();
        tl = this.GetComponent<LineTrajectory>();
        col = this.GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsGrounded())
        {
            startPoint = camera.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 15;

        }

        if (Input.GetMouseButton(0) && IsGrounded())
        {
            Vector3 currentPoint = camera.ScreenToWorldPoint(Input.mousePosition);
            currentPoint.z = 15;
            tl.RenderLine(startPoint, currentPoint);
            
        }

        if (Input.GetMouseButtonUp(0) && IsGrounded())
        {
            endPoint = camera.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 15;

            force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));

            addForce = true;

            tl.EndLine();
        }
    }

    private void FixedUpdate()
    {
        if (addForce)
        {
            rb.AddForce(force * power, ForceMode2D.Impulse);
            addForce = false;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size,0f,Vector2.down, .1f, jumpableGround);
    }
}
