using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateModel : MonoBehaviour
{

    [Header("shared attack attributes")]
    public float shootingCooldown;
    public float timeSinceLastShot;
    public float attackSpeed;


    [HideInInspector]
    public Transform player;

    [Header("shared player attributes")]
    public float runningSpeed;
    public float attackDistance;
    public bool playerInRange;
    public bool playerInSight;


    [Header("shared obstacle object")]
    public GameObject wallObjectsParent;


    [Header("shared archer attributes")]
    public float circleRadius = 0.2f;
    public Transform groundCheck;
    public Vector2 boxSize;
    public Transform wallCheck;
    [HideInInspector]
    public bool checkingWall;
    [HideInInspector]
    public bool isGrounded = false;
    [HideInInspector]
    public Vector3 middlePoint;


    private Animator animator;
    private int layerMask = 0;

    void Start()
    {
        // to avoid that the enemy has cooldown before the first shot
        //layerMask = LayerMask.NameToLayer("Platform");
        layerMask = 1 << 8;
        timeSinceLastShot = shootingCooldown;
        playerInRange = false;
        playerInSight = false;
   
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    public bool getPlayerInRange()
    {
        return playerInRange;
    }
    public bool getPlayerInSight()
    {
        return playerInSight;

    }
    public Transform getPlayerTransform()
    {
        return player;
    }

    public Animator getAnimator()
    {
        return animator;
    }

    public int getLayerMask()
    {
        return layerMask;
    }

}
