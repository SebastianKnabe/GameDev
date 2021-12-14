﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingeBulletWeapon : WeaponEntity
{
    public override void onSwitch()
    {
    }

    // Start is called before the first frame update
    public override void Start()
    {

        animator = Player.GetComponent<Animator>();
        spriteManager = Player.GetComponent<SpriteManager>();
        rb = Player.GetComponent<Rigidbody2D>();
        bullet = bulletPrefab.GetComponent<BulletScript>();
        crosshairScript = crosshair.GetComponent<CrosshairMouseScript>();
        switched = true;
    }

    // Update is called once per frame
    public override void Run()
    {
        Vector3 crosshairPlayerDifference = crosshairScript.getCrosshairPlayerPosition();
        float rotationZ = Mathf.Atan2(crosshairPlayerDifference.y, crosshairPlayerDifference.x) * Mathf.Rad2Deg;
        flipSprite(crosshairPlayerDifference);


        if (Input.GetMouseButton(0) && !DialogueManager.dialogueMode && weaponCooldownTimer > weaponCooldown)
        {
            float distance = crosshairPlayerDifference.magnitude;
            Vector2 direction = crosshairPlayerDifference / distance;
            direction.Normalize();
            fireBullet(direction, rotationZ);
            animator.SetTrigger("shooting");
            audioSource.PlayOneShot(bulletSound);
            weaponCooldownTimer = 0f;
        }
        
    }



    void fireBullet(Vector2 direction, float rotationZ)
    {
   

        GameObject b = Instantiate(bulletPrefab) as GameObject;
        b.transform.position = bulletStart.transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * bullet.bulletSpeed;
    }

}
