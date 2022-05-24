using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScipt : MonoBehaviour
{
    //Anim is the animator that the animations are being pulled from.
    public Animator anim;
    //Used to show the direction of the player(may not be needed but may also help).
    private Vector3 MoveDirection;
    //displays the values to me for testing.
    [SerializeField] private float MovingForBack;
    [SerializeField] private float MovingLeftRight;
    [SerializeField] private float JumpAndFall;
    [SerializeField] private bool Dying;
    [SerializeField] private float Falling;
    [SerializeField] private bool IsGrounded;
    [SerializeField] private GameObject Player = null;
    [SerializeField] Transform GroundCheck;
    [SerializeField] private float TestingMoreThings;
    [SerializeField] private float IDonno;

    //public float speed = 2f;
    
    // Update is called once per frame.
    void Start()
    {
        //gets the player
        if (Player == null)
        {
            Player = GameObject.Find("Ratatoskr");
        }
        IDonno = TestingMoreThings;
    }
    void Update()
    {
        run();
        JumpingAndFalling();
        Dead();
    }
    private void run()
    {
        //Squirrel walking animation. need to figure out player velocity.
        MovingForBack = Input.GetAxis("Vertical");
        MovingLeftRight = Input.GetAxis("Horizontal");
        JumpAndFall = Input.GetAxis("Jump");
        TestingMoreThings = Player.transform.position.y;
        //animation calls
        anim.SetFloat("Forwards", Input.GetAxis("Vertical"));
        anim.SetFloat("Turning", Input.GetAxis("Horizontal"));
       // anim.speed = speed;
        
    }
    private void JumpingAndFalling()
    { 
        //Jumping animation
        anim.SetFloat("Jumping", Input.GetAxis("Jump"));
        //Falling animation, needs to be set so when he is on the floor it doesn't play but if he is in the air it plays.
        if (TestingMoreThings < IDonno)
        {
            IsGrounded = true;
            IDonno = TestingMoreThings;
        }
        else
        {
            IsGrounded = false;
            IDonno = TestingMoreThings;
        }
        if (IsGrounded == true)
        {
            anim.SetBool("Falling", IsGrounded);
        }
        else
        {
            anim.SetBool("Falling", IsGrounded);
        }

    }
    private void Dead()
    {
        //Dying, need to tie this bool to character contacting wolf
        if (Dying == true)
        {
            anim.SetBool("Dying", Dying);
        }
        else
        {
            anim.SetBool("Dying", Dying);
        }
    }

}
