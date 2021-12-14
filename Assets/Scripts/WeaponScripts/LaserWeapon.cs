using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : WeaponEntity
{
    // Start is called before the first frame update
    public LineRenderer lineRender;
    public float damage;
    public float range;
    public int reflectionTime = 3;
    private static LineRenderer instance;
    private bool playShootingAudio;
    public EffectScript[] statusEffectOfTarget;

    //private bool reflectMode = false;
    //private bool reflectModeButtonPressed = false;
    public int maxReflectionCount = 5;
    public float maxStepDistance = 200;

    private LinkedList<Vector3> laserPoints;
    private float consumptionsSpeed;
    private float regainingSpeed;
    private float waitTime;
    private bool usedFuel;
    private bool isShooting;

    //@TODO check boundaries
    private float lastDirection, lastRotationZ;

    
    public override void Start()
    {

        animator = Player.GetComponent<Animator>();
        spriteManager = Player.GetComponent<SpriteManager>();
        rb = Player.GetComponent<Rigidbody2D>();
        bullet = bulletPrefab.GetComponent<BulletScript>();
        crosshairScript = crosshair.GetComponent<CrosshairMouseScript>();
        lineRender = GetComponent<LineRenderer>();
        laserPoints = new LinkedList<Vector3>();
        playShootingAudio = true;
        switched = true;
        usedFuel = false;
        isShooting = false;
        regainingSpeed = 5.0f;
        consumptionsSpeed = 10.0f;
        waitTime = 30.0f;
        //cooldownImage.fillAmount = 100;
    }

    // Update is called once per frame

    private void addPoint(Vector3 dir)
    {
       laserPoints.AddLast(dir);
    }
    private void updateLaser()
    {
        int index = 0;
        instance.positionCount = laserPoints.Count;
        foreach (var element in laserPoints)
        {
            instance.SetPosition(index, element);
            index++;
        }
    }
    //private void DrawPredictedReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining)
    //{
    //    if (reflectionsRemaining == 0)
    //    {
    //        return;
    //    }


    //    addPoint(bulletStart.transform.position);
    //    RaycastHit2D[] hits = Physics2D.LinecastAll(position, position + (direction * range));
    //    bool collided = false;
    //    foreach (RaycastHit2D hit in hits)
    //    {
    //        if (hit.collider.gameObject.CompareTag("Ground"))
    //        {
    //            Vector3 pos = hit.point;
    //            Vector3 dir = Vector3.Reflect(position + (direction * range), hit.normal);
    //            addPoint(dir);
    //            Debug.Log("hit : " + hit.collider.gameObject.tag);
    //            DrawPredictedReflectionPattern(position, direction, reflectionsRemaining - 1);
    //            break;
    //        }
    //    }
    //    if (!collided)
    //    {
    //        position += direction * range;
    //        addPoint(position);
    //        updateLaser();

    //    }

    //}
    public override void Run()
    {

        Vector3 crosshairPlayerDifference = crosshairScript.getCrosshairPlayerPosition();
        flipSprite(crosshairPlayerDifference);
        if (instance == null)
        {
            instance = Instantiate(lineRender);
        }

        //if(Input.GetMouseButton(1) && !reflectModeButtonPressed && !DialogueManager.dialogueMode)
        //{
        //    reflectMode = !reflectMode;
        //    //instance.positionCount = reflectMode ? 3 : 2;
        //    reflectModeButtonPressed = true;
        //    Debug.Log(reflectMode);
        //}
        //if(Input.GetMouseButtonUp(1))
        //{
        //    reflectModeButtonPressed = false;

        //}

        if (Input.GetMouseButton(0) && !DialogueManager.dialogueMode && cooldownImage.fillAmount > 0 && !usedFuel)
        {
            if (!instance) { return; }
            instance.enabled = true;
            
            laserPoints.Clear();
            instance.positionCount = 0;


            float distance = crosshairPlayerDifference.magnitude;
            float rotationZ = Mathf.Atan2(crosshairPlayerDifference.y, crosshairPlayerDifference.x) * Mathf.Rad2Deg;
            Vector3 direction = crosshairPlayerDifference / distance;
            direction.Normalize();



            //instance.SetPosition(0, bulletStart.transform.position);
            addPoint(bulletStart.transform.position);
            RaycastHit2D[] hits = Physics2D.LinecastAll(bulletStart.transform.position, bulletStart.transform.position + (direction * range));
            //Debug.DrawLine(bulletStart.transform.position, bulletStart.transform.position + (direction * range));

            if (getValidRange(rotationZ))
            {
               
             //DrawPredictedReflectionPattern(bulletStart.transform.position, transform.position + (direction * range), maxReflectionCount);                
                bool collided = false;
                foreach (RaycastHit2D hit in hits)
                {
                   if(collideWithGround(hit, direction) || collideWithEnemy(hit))
                    {
                        collided = true;
                        break;
                    }
                }
                if (!collided)
                {
                    //instance.SetPosition(1, bulletStart.transform.position + (direction * range));
                    addPoint(bulletStart.transform.position + (direction * range));

                }


                updateLaser();

                isShooting = true;
                animator.SetTrigger("shooting");
                if (playShootingAudio)
                {
                    audioSource.PlayOneShot(bulletSound);
                    playShootingAudio = false;
                }
            }

        }
        else
        {   
            instance.enabled = false;
            isShooting = false;
            playShootingAudio = true;
        
        }
           

    }


    public bool collideWithGround(RaycastHit2D hit, Vector3 direction)
    {
        if (hit.collider.gameObject.CompareTag("Ground"))
        {
           //instance.SetPosition(1, new Vector3(hit.point.x, hit.point.y, bulletStart.transform.position.z));
           addPoint(new Vector3(hit.point.x, hit.point.y, bulletStart.transform.position.z));
            //if (reflectMode) {

            //    Vector3 pos = hit.point;
            //    Vector3 dir = Vector3.Reflect(bulletStart.transform.forward, hit.normal);
            //    addPoint(dir);
            //    Debug.Log("hit : " + hit.collider.gameObject.tag);
                
            //}
           
            return true;
        }
        return false;
    }

    public bool collideWithEnemy(RaycastHit2D hit)
    {
        if (hit.collider.gameObject.CompareTag("Enemy"))
        {
            //instance.SetPosition(1, new Vector3(hit.point.x, hit.point.y, bulletStart.transform.position.z));
            addPoint(new Vector3(hit.point.x, hit.point.y, bulletStart.transform.position.z));
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

    public override void updateCooldown()
    {
        if ((!isShooting || usedFuel) && cooldownImage.fillAmount < 1)
        {
            cooldownImage.fillAmount += (regainingSpeed) / waitTime * Time.deltaTime;
        }
        else if (!usedFuel && isShooting && cooldownImage.fillAmount > 0)
        {
            cooldownImage.fillAmount -= (consumptionsSpeed) / waitTime * Time.deltaTime;
        }

        if (cooldownImage.fillAmount <= 0)
        {
            usedFuel = true;
        }
        if(usedFuel && cooldownImage.fillAmount >= 1)
        {
            usedFuel = false;
        }
    }

}
