using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour {
    private Rigidbody2D rb2d;
    [HideInInspector] public bool jump = false;

    public float move_force = 365f;
    public float max_speed = 5f;

    public float jump_force = 1000f;
    private bool grounded = false;
    public Transform groundCheck;

    private Animator animator;
    public int _currentAnimationState = 0;

    const int STATE_IDLE = 0;
    const int STATE_WALK = 1;
    const int STATE_ATTACK = 2;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
	}

    void FixedUpdate()
    {

        string[] arr = { "right", "left", "a", "d" };
        
        if (Input.GetKey("x"))
        {
            changeState(STATE_ATTACK);
        }
        else if (Input.GetKey("right") || Input.GetKey("left") || Input.GetKey("space"))
        {
            changeState(STATE_WALK);
        }

        else
        {
            changeState(STATE_IDLE);
        }

        float moveHorizontal = Input.GetAxis("Horizontal");

        if ( moveHorizontal * rb2d.velocity.x < max_speed)
        {
            rb2d.AddForce(Vector2.right * moveHorizontal * move_force);
        }

        if (Mathf.Abs(rb2d.velocity.x) > max_speed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * max_speed, rb2d.velocity.y);

        if (jump)
        {
            rb2d.AddForce(new Vector2(0f, jump_force));
            jump = false;
        }
    }

    // Update is called once per frame
    void Update () {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        float vertical = Input.GetAxis("Vertical");

        if(Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }

    }

    void changeState(int state)
    {
        /*
        if(_currentAnimationState == state)
        {
            return;
        }
        */
        switch (state)
        {
            case STATE_IDLE:
                animator.SetInteger("anim_state", STATE_IDLE);
                break;
            case STATE_WALK:
                animator.SetInteger("anim_state", STATE_WALK);
                break;
            case STATE_ATTACK:
                animator.SetInteger("anim_state", STATE_ATTACK);
                break;
        }

        _currentAnimationState = state;
    }
}
