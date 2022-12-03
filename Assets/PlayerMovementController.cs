using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private bool isSpecialJump = false;
    private bool isJump = false;
    public bool isGround = false;
    private bool hascomedown = false;
    private bool didSpring = false;
    public float allGroundCollisions = 0;
    private float JumpPower;

    
    //m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
    public string Walk(bool isLeft, bool isRight, bool isUp, bool isDown, Rigidbody2D rb, float movementSpeed, string facing, AnimatorClipInfo[] m_CurrentClipInfo)
    {
        
        if (isLeft && !((rb.velocity.x <= 0.05 && rb.velocity.x >= -0.05) && (isDown == true || isUp == true) && (isGround == true)))
        {
            if (isSpecialJump == false)
            {
                try
                {
                    rb.velocity = m_CurrentClipInfo[0].clip.name.Contains("throw") && !m_CurrentClipInfo[0].clip.name.Contains("jump")
                    ? new Vector2(0, rb.velocity.y)
                    : new Vector2(-movementSpeed, rb.velocity.y);
                }
                catch
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }


            }
        }
        else if (isRight && !((rb.velocity.x <= 0.05 && rb.velocity.x >= -0.05) && (isDown == true || isUp == true) && (isGround == true)))
        {
            if (isSpecialJump == false)
            {
                try
                {
                    rb.velocity = m_CurrentClipInfo[0].clip.name.Contains("throw") && !m_CurrentClipInfo[0].clip.name.Contains("jump")
                        ? new Vector2(0, rb.velocity.y)
                        : new Vector2(movementSpeed, rb.velocity.y);
                }
                catch
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }

            }
        }

        else if (isSpecialJump == false)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);

        }

        if (isLeft)
        {
            if (facing == "right")
            {
                facing = "left";
                Flip();
            }
        }
        else if (isRight)
        {
            if (facing == "left")
            {
                facing = "right";
                Flip();
            }
        }
        
        return facing;
    }
    public void Jump(AudioClip jump, Rigidbody2D rb, float JumpPower)
    {
        // if did regular jump we want no x velocity to be kept so when special hit, have special = true untill ground = true 
        // if hitting jump and on ground
        // && (gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0.1 && gameObject.GetComponent<Rigidbody2D>().velocity.y >= -0.1)
        if (Input.GetKey(KeyCode.Space) && isGround == true && !isJump)// && Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.y) <= 30f)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, 0));
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpPower), ForceMode2D.Impulse);
            isJump = true;
            hascomedown = false;
            gameObject.GetComponent<PlayerAudioController>().AudioPlayerSoft(jump);
            //audioSource.PlayOneShot(jump, 1F);
        }
        if (hascomedown == false && rb.velocity.y < 0)
        {
            hascomedown = true;
        }
        if (isJump && isGround == true && hascomedown == true)
        {
            isJump = false;
        }

    }
    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public bool IsJump() { return isJump; }
    public bool IsGround() { return isGround; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spring" && isGround == false)
        {
            //AudioPlayerSoft(spring);
            SpringController theSpring = collision.gameObject.GetComponent<SpringController>();
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpPower * theSpring.springPower), ForceMode2D.Impulse);
            didSpring = true;
        }
        else if (collision.gameObject.tag == "Ground")//
        {
            allGroundCollisions += 1;
            isSpecialJump = false;
            isGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            allGroundCollisions -= 1;
            if (allGroundCollisions == 0)
            {
                isGround = false;

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            allGroundCollisions += 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            allGroundCollisions -= 1;
            if (allGroundCollisions == 0)
            {
                isGround = false;

            }

        }
    }
}
