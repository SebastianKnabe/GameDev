using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoamMovement : MonoBehaviour
{
    public float roamSpeed = 5f;
    public float roamRange = 5f;
    public float roamTimer = 5f;

    private Rigidbody2D rb;
    private Vector2 roamDirection;
    private Vector2 startingPoint;
    private float sightCheck;
    private float randomDirectionTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        startingPoint = transform.position;
        randomDirectionTimer = roamTimer;
    }

    private void FixedUpdate()
    {
        moveCharacter();
    }

    void moveCharacter()
    {
        Vector2 roamPosition = (Vector2)transform.position + (randomDirection() * roamSpeed * Time.deltaTime);

        //Der Gegner schaut ob er noch in der Nähe seines Startpunktes ist
        if ((roamPosition - startingPoint).magnitude < roamRange)
        {
            rb.MovePosition(roamPosition);
        }
        /*
         * Der Gegner ist in diesem Fall außerhalb seines Bereiches. Jetzt schaut er ob die neue roamPosition 
         * näher am Startbereich ist.
         */
        else if ((roamPosition - startingPoint).magnitude < ((Vector2)transform.position - startingPoint).magnitude)
        {
            rb.MovePosition(roamPosition);
        }
        /*
         * In diesem Fall hat der Gegner kein gültiges Ziel. Der Timer für die RandomDirection wird auf den maximal
         * Wert gesetzt, um eine neue Position zu erhalten.
         */
        else
        {
            randomDirectionTimer += roamTimer;
        }


    }

    private Vector2 randomDirection()
    {
        if (randomDirectionTimer > roamTimer)
        {
            roamDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            randomDirectionTimer = 0f;
        }
        else
        {
            randomDirectionTimer += Time.fixedDeltaTime;
        }
        return roamDirection;
    }

    //Zeichnet grob den Sichtradius des Gegners
    //TODO richtiger Kreis/Breich zeichnen
    private void OnDrawGizmos()
    {
        drawRoamRange();
    }

    private void drawRoamRange()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startingPoint + new Vector2(0, 1) * roamRange, startingPoint + new Vector2(1, 0) * roamRange);
        Gizmos.DrawLine(startingPoint + new Vector2(0, -1) * roamRange, startingPoint + new Vector2(-1, 0) * roamRange);
        Gizmos.DrawLine(startingPoint + new Vector2(-1, 0) * roamRange, startingPoint + new Vector2(0, 1) * roamRange);
        Gizmos.DrawLine(startingPoint + new Vector2(1, 0) * roamRange, startingPoint + new Vector2(0, -1) * roamRange);
    }
}