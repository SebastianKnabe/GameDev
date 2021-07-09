using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    private bool facingRight = true;

    public bool getFacingRight()
    {
        return facingRight;
    }

    public void setFacingRight(bool facing)
    {
        facingRight = facing;
    }

    public void rotateSprite()
    {
        gameObject.transform.Rotate(new Vector3(0f, 180f, 0f));
        facingRight = !facingRight;
    }
}
