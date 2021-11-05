using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDotScript : EffectScript
{
    public float delay;
    private GameObject target;

    public override void InitEffect(GameObject targetObject)
    {
        target = targetObject;
        startAnimation();
        StartCoroutine(waitUntil());
    }

    private void startAnimation()
    {
        
    }

    IEnumerator waitUntil()
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }

 

}
