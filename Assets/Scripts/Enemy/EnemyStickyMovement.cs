using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStickyMovement : MonoBehaviour
{
    [SerializeField] LayerMask platformMask;
    [SerializeField] [Range(0, 1.5f)] private float moveSpeed = 1f;
    [SerializeField] private float rotationDuration = 1f;
    [SerializeField] private bool isPatrol = false;
    [SerializeField] private bool isStickToPlatform = false;

    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    private float rayCastOffset = 0.05f;
    private float currentRotation = 0f;
    private Vector3 checkGroundedVector = Vector3.down;
    private bool isRotating = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatrol)
        {

        }
        else if (isStickToPlatform)
        {
            if (isGrounded())
            {
                Move();
            }
            else if (!isRotating)
            {
                currentRotation = (currentRotation - 90f) % 360;
                StartCoroutine(RotateEnemy(currentRotation, rotationDuration));

                Debug.Log("currentRotation: " + currentRotation);
                Vector3 currentPosition = gameObject.transform.localPosition;
                gameObject.transform.localPosition = currentPosition + checkGroundedVector * 0.5f;

                switch (currentRotation)
                {
                    case -90f:
                        checkGroundedVector = Vector3.left;
                        break;
                    case -180f:
                        checkGroundedVector = Vector3.up;
                        break;
                    case -270f:
                        checkGroundedVector = Vector3.right;
                        break;
                    default:
                        checkGroundedVector = Vector3.down;
                        break;
                }
                Move();
            }
        }
    }

    private IEnumerator RotateEnemy(float targetAngle, float duration)
    {
        isRotating = true;
        moveSpeed /= 1.5f;
        if (targetAngle == -180 || targetAngle == -270)
        {
            targetAngle += 360;
        }
        Vector3 startLocalEulerAngles = gameObject.transform.localEulerAngles;
        Vector3 deltaLocalEulerAngles = new Vector3(0.0f, 0.0f, targetAngle - startLocalEulerAngles.z);
        Debug.Log("startLocalEulersAngles: " + startLocalEulerAngles.ToString() + "\n" +
                  "deltaLocalEulerAngles: " + deltaLocalEulerAngles.ToString() + "\n" +
                  "targetAngle: " + targetAngle
            );
        float timer = 0.0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            gameObject.transform.localEulerAngles = startLocalEulerAngles + deltaLocalEulerAngles * (timer / duration);
            yield return new WaitForEndOfFrame();
        }
        gameObject.transform.localEulerAngles = startLocalEulerAngles + deltaLocalEulerAngles;
        isRotating = false;
        moveSpeed *= 1.5f;
    }

    private void Move()
    {
        switch (currentRotation)
        {
            case -90f:
                rb.velocity = new Vector2(0f, -moveSpeed);
                break;
            case -180f:
                rb.velocity = new Vector2(-moveSpeed, 0f);
                break;
            case -270f:
                rb.velocity = new Vector2(0f, moveSpeed);
                break;
            default:
                rb.velocity = new Vector2(moveSpeed, 0f);
                break;
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(circleCollider.bounds.center, circleCollider.bounds.size * 1.05f, 0f, checkGroundedVector, rayCastOffset, platformMask);

        Debug.DrawRay(circleCollider.bounds.center, checkGroundedVector, Color.red);

        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }
}
