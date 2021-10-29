using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{

    public static ScreenShakeController instace;
    private float shakeTimeRemaining, shakePower, shakeFadeTime, shakeRoation;
    public float rotMultiplier = 15f;


    // Start is called before the first frame update
    void Start()
    {
        instace = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {

            startShake(.5f, 1f);
        }
    }

    private void LateUpdate()
    {
        if (shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;
            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0f);
            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);
            shakeRoation = Mathf.MoveTowards(shakeRoation, 0f, shakeFadeTime * rotMultiplier * Time.deltaTime);
        }

        transform.rotation = Quaternion.Euler(0f, 0f, shakeRoation * Random.Range(-1f,1f));
    }

    public void startShake(float length, float power) 
    {
        shakeTimeRemaining = length;
        shakePower = power;
        shakeFadeTime = power / length;
        shakeRoation = power * rotMultiplier;
    }
}
