using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnScript : EffectScript
{
    public float damage;
    public float applyDamageNTimes;
    public float applyDamageEveryNSeconds;
    public float delay;
    public GameObject burningEffect;
    public string animationString;


    private float appliedTimes;
    private GameObject target;
    private GameObject burningEffectInstance;

    public override void InitEffect(GameObject targetObject)
    {
        target = targetObject;
        appliedTimes = 0;
        startAnimation();

        StartCoroutine(dealDamage());
    }

    private void startAnimation()
    {
        float animationPosition_y = target.transform.position.y + ((0.3f)*(target.GetComponent<SpriteRenderer>().bounds.size.y));
        
        Vector3 spawnPosition = new Vector3(target.transform.position.x, animationPosition_y, target.transform.position.z);
        
        burningEffectInstance = Instantiate(burningEffect, spawnPosition, Quaternion.identity);
        burningEffectInstance.gameObject.transform.parent = target.transform;
        burningEffectInstance.GetComponent<Animator>().Play(animationString);


        
    }

    IEnumerator dealDamage()
    {
        yield return new WaitForSeconds(delay);

        for (appliedTimes = 0;  appliedTimes < applyDamageNTimes; appliedTimes++)
        {

            if (target.tag == "Player")
            {
                target.GetComponent<PlayerEntity>().takeDamageFromEffect(damage);
                ScreenShakeController.instace.startShake(.5f, .1f);

            }
            else if (target.tag == "Enemy")
            {
                target.GetComponent<EnemyEntity>().takeDamage(damage);
                ScreenShakeController.instace.startShake(.5f, .1f);

            }
            else
            {
                Destroy(this.gameObject);
            }
           
            
            if(appliedTimes != applyDamageNTimes - 1)
            {
                yield return new WaitForSeconds(applyDamageEveryNSeconds);
            }
            
        }

       
        Destroy(burningEffectInstance);
        Destroy(this.gameObject);
        
    }

}
