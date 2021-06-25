using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAnimationScript : MonoBehaviour
{

    public string animatorString;
    private float playTime = -1;

    private Animator animator;
    private bool started = false;
    // Update is called once per frame

    public void Start()
    {
        animator = GetComponent<Animator>();
       
    }

    void Update()
    {
        if (!started)
            return;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animatorString) &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= playTime)
        {

            Destroy(this.gameObject);
      
        }


    }

    public void playAnimation(float playTime)
    {
        started = true;
        //animator.speed = playSpeed;
        this.playTime = playTime;
        animator.Play(animatorString);

    }

   
}
