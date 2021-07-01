using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    [Range(1, 10)] public float smoothFactor;
    public PolygonCollider2D bound;
    public float cameraBound = 0.7f;

    private Vector2 cameraThreshold;
    private Rigidbody2D rb;
    private Camera camera;
    private PlayerMovement playerMovement;

    private void Start()
    {
        cameraThreshold = calculateThreshold();
        rb = target.GetComponent<Rigidbody2D>();
        camera = gameObject.GetComponent<Camera>();
        playerMovement = target.GetComponent<PlayerMovement>();
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

        //check InBounds, wenn target nicht inBound wird position nicht verändert
        if (checkCameraBoundsWidth(newPosition) == false)
        {
            newPosition.x = transform.position.x;
        }

        //check InBounds, wenn target nicht inBound wird position nicht verändert
        if (checkCameraBoundsHeight(newPosition) == false)
        {
            newPosition.y = transform.position.y;
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

    private bool checkCameraBoundsWidth(Vector2 targetPosition)
    {
        Rect aspect = Camera.main.pixelRect;
        /*
         * CameraSize.x = Width  / 2
         * CameraSize.y = Height / 2
         */
        Vector2 cameraSize = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        if (checkInBound(targetPosition - new Vector2(cameraSize.x * cameraBound, 0f)) &&
            checkInBound(targetPosition + new Vector2(cameraSize.x * cameraBound, 0f)))
        {
            return true;
        }

        return false;
    }

    private bool checkCameraBoundsHeight(Vector2 targetPosition)
    {
        Rect aspect = Camera.main.pixelRect;
        /*
         * CameraSize.x = Width  / 2
         * CameraSize.y = Height / 2
         */
        Vector2 cameraSize = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        if (checkInBound(targetPosition - new Vector2(0f, cameraSize.y * cameraBound)) &&
            checkInBound(targetPosition + new Vector2(0f, cameraSize.y * cameraBound)))
        {
            return true;
        }

        return false;
    }

    private bool checkInBound(Vector2 vector2)
    {
        Vector2 boundPoint = bound.ClosestPoint(vector2);
        //Debug.Log("Vector2: " + vector2.ToString());
        //Debug.Log("boundPoint: " + boundPoint.ToString());
        if ((boundPoint - vector2).magnitude == 0)
        {
            return true;
        }
        return false;
    }


    //Zeichnet das Rechteck aus calculateThreshold()
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border = calculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));

        Gizmos.color = Color.red;
        Rect aspect = Camera.main.pixelRect;
        Vector2 cameraSize = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        Gizmos.DrawWireCube(transform.position, new Vector3(cameraSize.x * 2, cameraSize.y * 2, 1));
    }

    public void resetPosition()
    {
        Vector2 backup = playerMovement.getBackupPosition();
        transform.position = new Vector3(backup.x, backup.y, -10f);
    }

    public void MoveCameraToSpawnPoint(Transform spawnPoint)
    {
        this.transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, -10);
    }
}
