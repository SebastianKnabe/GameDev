﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class WeaponEntity : MonoBehaviour
{
    public GameObject Player;
    public GameObject crosshair;
    [HideInInspector] public Image weaponImage;
    [HideInInspector] public Image cooldownImage;
    public GameObject bulletPrefab;
    public GameObject bulletStart;
    public float spriteFlipTreshold = 5f;
    public AudioSource audioSource;
    [HideInInspector] public SpriteManager spriteManager;
    [HideInInspector] public BulletScript bullet;
    [HideInInspector] public Animator animator;
    [SerializeField] public AudioClip bulletSound;
    public float weaponCooldownTimer = 0f;
    public float weaponCooldown;
    [HideInInspector] public Rigidbody2D rb;

    public CrosshairMouseScript crosshairScript;

    [HideInInspector] public bool switched = false;
    
    public abstract void Start();
    public abstract void Update();

    public abstract void onSwitch();

    public void flipSprite(Vector3 crosshairPlayerDifference)
    {
        bool facingRight = spriteManager.getFacingRight();
        if (crosshairPlayerDifference.x > spriteFlipTreshold && !facingRight && rb.velocity.magnitude == 0)
        {
            spriteManager.rotateSprite();
        }
        else if (crosshairPlayerDifference.x < -spriteFlipTreshold && facingRight && rb.velocity.magnitude == 0)
        {
            spriteManager.rotateSprite();
        }
    }
}