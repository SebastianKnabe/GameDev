using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatureTriggerScript : MonoBehaviour
{
    public UnityEvent triggeredEvent;
    public UnityEvent defaultEvent;
    public Sprite default_sprite;
    public Sprite active_sprite;

    private SpriteRenderer spriteRenderer;
    private bool playerInRange;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Sprite new_sprite = spriteRenderer.sprite == default_sprite ? active_sprite : default_sprite;
            spriteRenderer.sprite = new_sprite;

            if(new_sprite == default_sprite)
            {
                defaultEvent.Invoke();
            }else if(new_sprite == active_sprite)
            {
                triggeredEvent.Invoke();
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {

            playerInRange = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {

            playerInRange = false;

        }
    }

}
