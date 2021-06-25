using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sneakerSelfDestructionScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform sneaker;
    public float damage;
    public float force;
    public GameObject ExplosionPrefab;

    public EffectScript[] statusEffectOfTarget;
    
    private StateModel sneakerEntity;
    private Rigidbody2D sneakerRigidbody;



    void Start()
    {
        sneakerEntity = sneaker.GetComponent<StateModel>();
        sneakerRigidbody = sneakerEntity.GetComponent<Rigidbody2D>();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            explode(collision.gameObject);
        }
    }

    private void explode(GameObject playerObject)
    {
     
        playerObject.GetComponent<PlayerEntity>().takeDamage(damage);
        //Vector2 pushDirection = playerObject.transform.position - sneaker.position;
        //playerObject.GetComponent<Rigidbody2D>().AddForce(pushDirection * force);

        GameObject explosionInstance = Instantiate(ExplosionPrefab, sneaker.position, Quaternion.identity);
        explosionInstance.GetComponent<RunAnimationScript>().Start();
        explosionInstance.GetComponent<RunAnimationScript>().playAnimation(0.9f);

        foreach(EffectScript effect in statusEffectOfTarget)
        {
            EffectScript instance = Instantiate(effect, playerObject.transform.position, Quaternion.identity);
            instance.gameObject.transform.parent = playerObject.transform;
            instance.InitEffect(playerObject);
          
        }

        Destroy(sneaker.gameObject);


    }
}
