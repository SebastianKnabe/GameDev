using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Die FollowAi erlaubt es einen Gegner den Spieler zu verfolgen
 * sobald er im Sichtradius(SightRange) ist.
 * Ist der Gegner zuweit von seinem Startpunkt(startingPoint) entfernt,
 * kehrt er langsam in sein Startbereich zurück.
 */
public class FollowAI : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 10f;
    public float roamSpeed = 5f;
    public float sightRange = 5f;
    public float roamRange = 5f;
    public float roamTimer = 5f;
    public bool showRangeGizmo = false;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;
    private Vector2 roamDirection;
    private Vector2 startingPoint;
    private float sightCheck;
    private float randomDirectionTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        startingPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        sightCheck = direction.magnitude;
        movement = direction.normalized;
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        //Spieler ist im Sichtbereich
        if(sightCheck < sightRange)
        {
            Debug.Log("follow Player");
            rb.MovePosition((Vector2)transform.position + (direction * followSpeed * Time.deltaTime));
            flipSprite(direction);
        }
        else 
        {
            Debug.Log("roam");
            direction = (Vector2)transform.position + (randomDirection() * roamSpeed * Time.deltaTime);

            //Der Gegner schaut ob er noch in der Nähe seines Startpunktes ist
            if((direction - startingPoint).magnitude < roamRange)
            {
                rb.MovePosition(direction);
                flipSprite(direction);
            }
            /*
             * Der Gegner ist in diesem Fall außerhalb seines Bereiches. Jetzt schaut er ob die neue direction 
             * näher am Startbereich ist.
             */
            else if ((direction - startingPoint).magnitude < ((Vector2) transform.position - startingPoint).magnitude)
            {
                rb.MovePosition(direction);
                flipSprite(direction);
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
    }

    private Vector2 randomDirection()
    {
        if(randomDirectionTimer > roamTimer)
        {
            roamDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            randomDirectionTimer = 0f;
        } else
        {
            randomDirectionTimer += Time.fixedDeltaTime;
        }
        return roamDirection;
    }

    private void flipSprite(Vector2 direction)
    {
        if ((direction.x - transform.position.x) > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
  
    //Zeichnet grob den Sichtradius des Gegners
    //TODO richtiger Kreis/Breich zeichnen
    private void OnDrawGizmos()
    {
        if(showRangeGizmo)
        {
            drawRoamRange();
            drawSightRange();
        }
    }

    private void drawRoamRange()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startingPoint + new Vector2(0, 1) * roamRange, startingPoint + new Vector2(1, 0) * roamRange);
        Gizmos.DrawLine(startingPoint + new Vector2(0, -1) * roamRange, startingPoint + new Vector2(-1, 0) * roamRange);
        Gizmos.DrawLine(startingPoint + new Vector2(-1, 0) * roamRange, startingPoint + new Vector2(0, 1) * roamRange);
        Gizmos.DrawLine(startingPoint + new Vector2(1, 0) * roamRange, startingPoint + new Vector2(0, -1) * roamRange);
    }

    private void drawSightRange()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + new Vector3(0, 1) * sightRange, transform.position + new Vector3(1, 0) * sightRange);
        Gizmos.DrawLine(transform.position + new Vector3(0, -1) * sightRange, transform.position + new Vector3(-1, 0) * sightRange);
        Gizmos.DrawLine(transform.position + new Vector3(-1, 0) * sightRange, transform.position + new Vector3(0, 1) * sightRange);
        Gizmos.DrawLine(transform.position + new Vector3(1, 0) * sightRange, transform.position + new Vector3(0, -1) * sightRange);
    }
}
