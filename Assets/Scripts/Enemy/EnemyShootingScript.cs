using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingScript : MonoBehaviour
{
    public GameObject playerTarget;
    public GameObject bulletPrefab;
    public GameObject bulletStart;
    public float weaponCooldown = 1f;
    public float attackRange = 1f;
    public float bulletSpeed = 60.0f;
    [Range(0f, 10f)] public float inAccuracyFactor = 1f;
    //public AudioSource audioSource;

    private float weaponCooldownTimer = 0f;
    //[SerializeField] private AudioClip bulletSound;

    public void Update()
    {
        if (weaponCooldownTimer > weaponCooldown)
        {
            Vector2 randomVector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * inAccuracyFactor;
            Vector2 targetPosition = (Vector2)playerTarget.transform.position + randomVector;
            Vector2 playerDifference = targetPosition - (Vector2)bulletStart.transform.position;
            float rotationZ = Mathf.Atan2(playerDifference.y, playerDifference.x) * Mathf.Rad2Deg;
            float distance = playerDifference.magnitude;
            Vector2 direction = playerDifference / distance;
            direction.Normalize();
            if (distance < attackRange)
            {
                fireBullet(direction, rotationZ);
                //audioSource.PlayOneShot(bulletSound);
                weaponCooldownTimer = 0f;
            }
        }
        else
        {
            weaponCooldownTimer += Time.fixedDeltaTime;
        }
    }

    void fireBullet(Vector2 direction, float rotationZ)
    {
        GameObject b = Instantiate(bulletPrefab) as GameObject;
        b.tag = tag;
        b.transform.position = bulletStart.transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}
