using System.Numerics;
using NUnit.Framework.Constraints;
using UnityEditor.Search;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class MarioController : MonoBehaviour
{
    public float acceleration = 3f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 8f;
    public float jumpBoostForce = 8f;
    [Header("Debug Stuff")]
    public bool isGrounded = true;
    Animator animator;
    public GameManager manager;
    public CameraMover cameraMover;
    
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void UpdateAnimation()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetBool("inAir", isGrounded);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalAmount = Input.GetAxis("Horizontal");
        rb.linearVelocity += Vector3.right * horizontalAmount * Time.deltaTime * acceleration;
        
        float horizontalSpeed = rb.linearVelocity.x;
        horizontalSpeed = Mathf.Clamp(horizontalSpeed, -maxSpeed, maxSpeed);
        
        Vector3 newVelocity = rb.linearVelocity;
        newVelocity.x = horizontalSpeed;
        rb.linearVelocity = newVelocity;
        
        //should also clamp vertical velocity
        
        //Test if character is on ground surface
        Collider c = GetComponent<Collider>();
        //float castDistance = c.bounds.extents.y;
        float castDistance = 0.2f;
        Vector3 startPoint = transform.position;
        
        isGrounded = Physics.Raycast(startPoint, Vector3.down, castDistance);

        Color color = isGrounded ? Color.green : Color.red;
        Debug.DrawLine(startPoint, startPoint + castDistance * Vector3.down, color, 0f, false);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
                rb.AddForce(Vector3.up * jumpImpulse, ForceMode.VelocityChange);
                isGrounded = false;
        }
        else if (Input.GetKey(KeyCode.Space) && !isGrounded)
        {
            if (rb.linearVelocity.y > 0)
            {
                rb.AddForce(Vector3.up * jumpBoostForce, ForceMode.Acceleration);  
                isGrounded = false;
            }
        }

        if (horizontalAmount == 0f)
        {
            Vector3 decayVelocity = rb.linearVelocity;
            decayVelocity.x *= 1f - Time.deltaTime * 4f;
            rb.linearVelocity = decayVelocity;
        }
        else
        {
            float yawRotation = horizontalAmount > 0f ? 90f : -90f;
            Quaternion rotation = Quaternion.Euler(0f, yawRotation, 0f);
            transform.rotation = rotation;
        }
        UpdateAnimation();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("?"))
        {
            float castDistance = 2f;
            Vector3 startPoint = transform.position;
            bool hitBottom = Physics.Raycast(startPoint, Vector3.up, castDistance);
            if (hitBottom && !isGrounded)
            {
                manager.IncreaseCoins();
            }
        } else if (collision.gameObject.CompareTag("Brick"))
        {
            float castDistance = 2f;
            Vector3 startPoint = transform.position;
            bool hitBottom = Physics.Raycast(startPoint, Vector3.up, castDistance);
            //Debug.DrawLine(startPoint, startPoint + castDistance * Vector3.up, Color.blue, 0f, false);

            if (hitBottom && !isGrounded)
            {
                Destroy(collision.gameObject);
                manager.IncreaseScore(100);
            }
        } else if (collision.gameObject.CompareTag("Instant Death"))
        {
            cameraMover.ResetCamera();
            transform.position = new Vector3(10f, 2.05f, 0f);
            Debug.Log("Player lost");
            manager.ResetGame();
        } else if (collision.gameObject.CompareTag("Goal"))
        {
            cameraMover.ResetCamera();
            transform.position = new Vector3(10f, 2.05f, 0f);
            Debug.Log("Player won");
            manager.ResetGame();
        }
    }
}
