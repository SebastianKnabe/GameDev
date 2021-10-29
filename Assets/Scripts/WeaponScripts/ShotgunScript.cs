using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : WeaponEntity
{
  
    [SerializeField] private int NumberOfProjectiles = 3;
    [Range(0, 360)]
    [SerializeField] private float SpreadAngle = 20;
    
    public override void Start()
    {

        animator = Player.GetComponent<Animator>();
        spriteManager = Player.GetComponent<SpriteManager>();
        rb = Player.GetComponent<Rigidbody2D>();
        bullet = bulletPrefab.GetComponent<BulletScript>();
        crosshairScript = crosshair.GetComponent<CrosshairMouseScript>();

    }

    // Update is called once per frame
    public override void Update()
    {
        Vector3 crosshairPlayerDifference = crosshairScript.getCrosshairPlayerPosition();
        float rotationZ = Mathf.Atan2(crosshairPlayerDifference.y, crosshairPlayerDifference.x) * Mathf.Rad2Deg;
        flipSprite(crosshairPlayerDifference);

        float angleStep = SpreadAngle / NumberOfProjectiles;
        float aimingAngle = rotationZ;
        float centeringOffset = (SpreadAngle / 2) - (angleStep / 2); //offsets every projectile so the spread is                                                                                                                         //centered on the mouse cursor
  
        if (Input.GetMouseButton(0) && !DialogueManager.dialogueMode && weaponCooldownTimer > weaponCooldown)
        {

            for (int i = 0; i < NumberOfProjectiles; i++)
        {
                float currentBulletAngle = angleStep * i;
                float rotation =rotationZ + currentBulletAngle - centeringOffset;
                float distance = crosshairPlayerDifference.magnitude;
                Vector2 direction = crosshairPlayerDifference / distance;
                direction.Normalize();
                if(i != (NumberOfProjectiles - 1) / 2) { 
                 direction += new Vector2(0, Random.RandomRange(-0.25f, 0.25f));
                }
                fireBullet(direction, rotation);
            }

            animator.SetTrigger("shooting");
            audioSource.PlayOneShot(bulletSound);
            weaponCooldownTimer = 0f;
        }
        
    }



    void fireBullet(Vector2 direction, float rotationZ)
    {
        GameObject b = Instantiate(bulletPrefab,  bulletStart.transform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ)) as GameObject;
        //b.transform.position = bulletStart.transform.position;
        //b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * bullet.bulletSpeed;
    }

    
}


/* another approach, maybe usefull for an enemy 
 * 
 * using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : WeaponEntity
{
    public float startAngle;
    public float endAngle;
    public int bulletCount;
    
    public override void Start()
    {

        animator = Player.GetComponent<Animator>();
        spriteManager = Player.GetComponent<SpriteManager>();
        rb = Player.GetComponent<Rigidbody2D>();
        bullet = bulletPrefab.GetComponent<BulletScript>();
        crosshairScript = crosshair.GetComponent<CrosshairMouseScript>();

    }

    // Update is called once per frame
    public override void Update()
    {
        Vector3 crosshairPlayerDifference = crosshairScript.getCrosshairPlayerPosition();
        float rotationZ = Mathf.Atan2(crosshairPlayerDifference.y, crosshairPlayerDifference.x) * Mathf.Rad2Deg;
        flipSprite(crosshairPlayerDifference);

        

        float angleStep = (endAngle-startAngle)/bulletCount;
        float angle = startAngle;
        if (Input.GetMouseButton(0) && !DialogueManager.dialogueMode && weaponCooldownTimer > weaponCooldown)
        {

            for(int i = 0; i< bulletCount + 1; i++){

                float distance = crosshairPlayerDifference.magnitude;
                Vector2 direction = (crosshairPlayerDifference) / distance;
                direction.Normalize();

                float dirX = transform.position.x + Mathf.Sin((angle*Mathf.PI) / 180f);
                float dirY = transform.position.y + Mathf.Cos((angle*Mathf.PI) / 180f);

                Vector3 moveVec = new Vector3(dirX,dirY);
                Vector2 bulDir = (moveVec - transform.position).normalized;

                fireBullet(bulDir, rotationZ);


                angle+=angleStep;

                //float offSet = startRotation + offSetSteps*i;
                //float distance = crosshairPlayerDifference.magnitude;
                //Vector2 direction = (crosshairPlayerDifference) / distance;
                //direction.y -= offSet;
                //direction.Normalize();
                //fireBullet(direction, rotationZ);
               // newRoast = Quaternion.Euler(gunPoint.eulerAngles.x,gunPoint.eulerAngles.y,gunPoint.eulerAngles.z + addedOffset);
              //  Instantiate(projectile,gunPoint.position,newRot);
            }
            animator.SetTrigger("shooting");
            audioSource.PlayOneShot(bulletSound);
            weaponCooldownTimer = 0f;
        }
        
    }



    void fireBullet(Vector2 direction, float rotationZ)
    {
        GameObject b = Instantiate(bulletPrefab,  bulletStart.transform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ)) as GameObject;
        //b.transform.position = bulletStart.transform.position;
        //b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * bullet.bulletSpeed;
    }

    
}
/*/