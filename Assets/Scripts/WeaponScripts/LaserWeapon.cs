using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : WeaponEntity
{
    // Start is called before the first frame update
    public LineRenderer lineRender;
    public float damage;
    public float range; 
    private static LineRenderer instance;
    private bool playShootingAudio;
    public EffectScript[] statusEffectOfTarget;
    public override void Start()
    {

        animator = Player.GetComponent<Animator>();
        spriteManager = Player.GetComponent<SpriteManager>();
        rb = Player.GetComponent<Rigidbody2D>();
        bullet = bulletPrefab.GetComponent<BulletScript>();
        crosshairScript = crosshair.GetComponent<CrosshairMouseScript>();
        lineRender = GetComponent<LineRenderer>();
        playShootingAudio = true;
        switched = true;
    }

    // Update is called once per frame
    public override void Update()
    {

        Vector3 crosshairPlayerDifference = crosshairScript.getCrosshairPlayerPosition();
        flipSprite(crosshairPlayerDifference);
        if (instance == null)
        {
            instance = Instantiate(lineRender);
        }

        if (Input.GetMouseButton(0) && !DialogueManager.dialogueMode )
        {
            if (!instance) { return; }
            instance.enabled = true;

            float distance = crosshairPlayerDifference.magnitude;
            float rotationZ = Mathf.Atan2(crosshairPlayerDifference.y, crosshairPlayerDifference.x) * Mathf.Rad2Deg;
            Vector3 direction = crosshairPlayerDifference / distance;
            direction.Normalize();

            instance.SetPosition(0, bulletStart.transform.position);
           
            RaycastHit2D[] hits = Physics2D.LinecastAll(bulletStart.transform.position, bulletStart.transform.position + (direction * range));
            Debug.DrawLine(bulletStart.transform.position, bulletStart.transform.position + (direction * range));
            if (getValidRange(rotationZ))
            {
                bool collided = false;
                foreach (RaycastHit2D hit in hits)
                {
                   if(collideWithGround(hit) || collideWithEnemy(hit))
                    {
                        collided = true;
                        break;
                    }
                }
                if (!collided)
                {
                    instance.SetPosition(1, bulletStart.transform.position + (direction * range));

                }
            }
            

        
            animator.SetTrigger("shooting");
            if (playShootingAudio)
            {
                audioSource.PlayOneShot(bulletSound);
                playShootingAudio = false;
            }
       
            weaponCooldownTimer = 0f;
        }
        else
        {
           
            instance.enabled = false;
            playShootingAudio = true;
        }
           

    }


    public bool collideWithGround(RaycastHit2D hit)
    {
        if (hit.collider.gameObject.CompareTag("Ground"))
        {
            instance.SetPosition(1, new Vector3(hit.point.x, hit.point.y, bulletStart.transform.position.z));
            return true;
        }
        return false;
    }

    public bool collideWithEnemy(RaycastHit2D hit)
    {
        if (hit.collider.gameObject.CompareTag("Enemy"))
        {
            instance.SetPosition(1, new Vector3(hit.point.x, hit.point.y, bulletStart.transform.position.z));
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.GetComponentInChildren<LaserDotScript>() == null)
            {
                hit.collider.gameObject.gameObject.GetComponent<EnemyEntity>().takeDamage(damage);

                foreach (EffectScript effect in statusEffectOfTarget)
                {
                    EffectScript instance = Instantiate(effect, hit.collider.gameObject.transform.position, Quaternion.identity);
                    instance.gameObject.transform.parent = hit.collider.gameObject.transform;
                    instance.InitEffect(hit.collider.gameObject);

                }

            }
            return true;

        }
        return false;
    }

    public bool getValidRange(float rotationZ)
    {
        return ((spriteManager.getFacingRight() && rotationZ <= 90 && rotationZ >= -90) || (!spriteManager.getFacingRight() && (rotationZ >= 90 || rotationZ <= -90)));

    }

    public override void onSwitch()
    {
        Destroy(instance.gameObject);
    }
}
