using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{

    public GameObject crosshair;
    public GameObject bulletPrefab;
    public GameObject bulletStart;
    public float spriteFlipTreshold = 5f;
    public AudioSource audioSource;
    private SpriteManager spriteManager;
    private BulletScript bullet;
    private Animator animator;
    [SerializeField] private AudioClip bulletSound;
    private float weaponCooldownTimer = 0f;
    private Rigidbody2D rb;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        spriteManager = gameObject.GetComponent<SpriteManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        bullet = bulletPrefab.GetComponent<BulletScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 crosshairPlayerDifference = crosshair.GetComponent<CrosshairMouseScript>().getCrosshairPlayerPosition();
        float rotationZ = Mathf.Atan2(crosshairPlayerDifference.y, crosshairPlayerDifference.x) * Mathf.Rad2Deg;
        flipSprite(crosshairPlayerDifference);

        if (Input.GetMouseButton(0) && !DialogueManager.dialogueMode && weaponCooldownTimer > bullet.weaponCooldown)
        {
            float distance = crosshairPlayerDifference.magnitude;
            Vector2 direction = crosshairPlayerDifference / distance;
            direction.Normalize();
            fireBullet(direction, rotationZ);
            animator.SetTrigger("shooting");
            audioSource.PlayOneShot(bulletSound);
            weaponCooldownTimer = 0f;
        }
        else if (weaponCooldownTimer <= bullet.weaponCooldown)
        {
            weaponCooldownTimer += Time.fixedDeltaTime;
        }
    }

    void fireBullet(Vector2 direction, float rotationZ)
    {
        GameObject b = Instantiate(bulletPrefab) as GameObject;
        b.transform.position = bulletStart.transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * bullet.bulletSpeed;
    }

    private void flipSprite(Vector3 crosshairPlayerDifference)
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
