using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.UI;

public class move_frog : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    public Animator anim;
    public Transform groundcheck1, groundcheck2;
    private bool isgrouned;
    public LayerMask whatisground;
    public SpriteRenderer playerSR;
    public float apples;
    public Text score;
    public game_over over;
    
    



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        over = GameObject.FindGameObjectWithTag("game_over").GetComponent<game_over>();
    }

   
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);
        isgrouned = Physics2D.OverlapCircle(groundcheck1.position, 0.1f, whatisground) 
            ||Physics2D.OverlapCircle(groundcheck2.position, 0.1f, whatisground);

        if (Input.GetButtonDown("Jump") && isgrouned)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("isgrounded", isgrouned);
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            playerSR.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            playerSR.flipX = true;
        }
       
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Apple")
        {
            Destroy(collision.gameObject);
            apples = apples + 1;
            score.text = "score: " + apples;
            
        }
        if (apples == 5)
        {
            over.gameover();
        }

    }
    
    
}
