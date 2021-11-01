using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStickyMovement : MonoBehaviour
{
    [SerializeField] LayerMask platformMask;
    [SerializeField] [Range(0, 5f)] private float moveSpeed = 1f;
    [SerializeField] private bool isPatrol = false;
    [SerializeField] private bool isStickToPlatform = false;

    private Vector3 checkGroundedVector = Vector3.down;
    private Vector3 checkFacingWallVector = Vector3.right;

    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    private float rayCastOffset = 0.25f;
    private float currentRotation = 0f;
    private float rotationDuration = 0.5f;
    private bool isRotating = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        if (moveSpeed > 2f)
        {
            rotationDuration = 1f / moveSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DebugRaycasts();
        CheckFacingWall();
        if (isPatrol)
        {

        }
        else if (isStickToPlatform)
        {
            if (isRotating)
            {
                //Rotiert langsam                
                Move();
            }
            else
            {
                //Steht auf Platform
                if (IsGrounded() && !CheckFacingWall())
                {
                    Move();
                }
                //Kante
                else if (!IsGrounded() && !CheckFacingWall())
                {
                    currentRotation = (currentRotation - 90f) % 360;
                    StartCoroutine(RotateEnemy(currentRotation, rotationDuration));

                    Debug.Log("currentRotation: " + currentRotation);
                    Vector3 currentPosition = gameObject.transform.localPosition;
                    gameObject.transform.localPosition = currentPosition + checkGroundedVector * 0.5f;

                    CheckVectors();
                    Move();
                }
                //Wand Lauf Richtung
                else if (CheckFacingWall() && IsGrounded())
                {
                    currentRotation = (currentRotation + 90f);
                    StartCoroutine(RotateEnemy(currentRotation, rotationDuration));
                    currentRotation = currentRotation % 360;

                    Debug.Log("currentRotation: " + currentRotation);
                    Vector3 currentPosition = gameObject.transform.localPosition;
                    gameObject.transform.localPosition = currentPosition - checkGroundedVector * 0.5f;

                    CheckVectors();
                    Move();
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }
            }
        }
    }

    private IEnumerator RotateEnemy(float targetAngle, float duration)
    {
        isRotating = true;
        moveSpeed /= 2f;
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
        moveSpeed *= 2f;
    }

    private void Move()
    {
        float checkRotation = currentRotation;
        if (checkRotation < 0f)
        {
            checkRotation += 360;
        }

        switch (checkRotation)
        {
            case 270f:
                rb.velocity = new Vector2(0f, -moveSpeed);
                break;
            case 180f:
                rb.velocity = new Vector2(-moveSpeed, 0f);
                break;
            case 90f:
                rb.velocity = new Vector2(0f, moveSpeed);
                break;
            default:
                rb.velocity = new Vector2(moveSpeed, 0f);
                break;
        }
    }

    private void CheckVectors()
    {
        float checkRotation = currentRotation;
        if (checkRotation < 0f)
        {
            checkRotation += 360;
        }

        switch (checkRotation)
        {
            case 270f:
                checkGroundedVector = Vector3.left;
                checkFacingWallVector = Vector3.down;
                break;
            case 180f:
                checkGroundedVector = Vector3.up;
                checkFacingWallVector = Vector3.left;
                break;
            case 90f:
                checkGroundedVector = Vector3.right;
                checkFacingWallVector = Vector3.up;
                break;
            default:
                checkGroundedVector = Vector3.down;
                checkFacingWallVector = Vector3.right;
                break;
        }
    }

    private bool CheckFacingWall()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(circleCollider.bounds.center, checkFacingWallVector, 1f, platformMask);

        Debug.Log("checkWallRight: " + raycastHit.collider);
        return raycastHit.collider != null;
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(circleCollider.bounds.center, circleCollider.bounds.size * 1.05f, 0f, checkGroundedVector, rayCastOffset, platformMask);

        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    private void DebugRaycasts()
    {
        //Magenta = Facing Wall Check
        Debug.DrawRay(circleCollider.bounds.center, checkFacingWallVector, Color.magenta);
        //Rot = Groundcheck
        Debug.DrawRay(circleCollider.bounds.center, checkGroundedVector, Color.red);
    }
}
