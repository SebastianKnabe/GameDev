using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    [Range(1, 10)] public float smoothFactor;

    private Vector2 cameraThreshold;
    private Rigidbody2D rb;

    private void Start()
    {
        cameraThreshold = calculateThreshold();
        rb = target.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Follow();
        
    }

    void Follow()
    {
        Vector2 targetPosition = target.transform.position;
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * targetPosition.x);
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * targetPosition.y);

        Vector3 newPosition = transform.position;

        //Sobald der Spieler zuweit von der Mitte entfernt ist, wird die Position angepasst
        if (Mathf.Abs(xDifference) >= cameraThreshold.x)
        {
            newPosition.x = targetPosition.x;
        }

        if (Mathf.Abs(yDifference) >= cameraThreshold.y)
        {
            newPosition.y = targetPosition.y;
        }

        //Falls der Spieler zu schnell ist, wird die Camera mit der Geschwindigkeit des Spielers gezeogen
        float cameraMoveSpeed = rb.velocity.magnitude > smoothFactor ? rb.velocity.magnitude : smoothFactor;

        transform.position = Vector3.MoveTowards(transform.position, newPosition, cameraMoveSpeed * Time.fixedDeltaTime);
    }

    //Berechnet ein Rechteck in dem die Camera nicht dem Spieler folgt
    private Vector3 calculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= offset.x;
        t.y -= offset.y;
        return t;
    }

    //Zeichnet das Rechteck aus calculateThreshold()
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border = calculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x *2, border.y *2, 1));
    }

    public void MoveCameraToSpawnPoint()
    {
        this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z - 2);
    }
}
