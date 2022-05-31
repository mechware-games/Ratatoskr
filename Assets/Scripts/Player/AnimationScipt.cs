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

    [SerializeField] private bool wallSwap;

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
        bool wallRunning = false;
        if (Player.GetComponent<Movement>().CheckWallRun() && !Player.GetComponent<Movement>().CheckGrounded())
        {
            wallRunning = true;
        }
        else
        {
            wallRunning = false;
        }

        if (wallRunning)
        {
            anim.SetBool("WallRunning", true);
            anim.SetFloat("Wall Running", 2f);
            if (Player.GetComponent<Movement>().CheckWallRight())
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            anim.SetBool("WallRunning", false);
            anim.SetFloat("Wall Running", 0f);
            run();
        }

        JumpingAndFalling();
        Dead();
    }
    private void run()
    { if (Player.GetComponent<Movement>().CheckGrounded() && !Player.GetComponent<Movement>().CheckNearWall())
        {
            //Squirrel walking animation. need to figure out player velocity.
            MovingForBack = Input.GetAxis("Vertical");
            MovingLeftRight = Input.GetAxis("Horizontal");
            JumpAndFall = Input.GetAxis("Jump");
            TestingMoreThings = Player.transform.position.y;
            //animation calls
            float vert = Mathf.Abs(Input.GetAxis("Vertical"));
            float horz = Mathf.Abs(Input.GetAxis("Horizontal"));

            if (vert > 0.05f)
            {
                float speed = vert * 3f;
                if (speed < 1f) { speed = 1f; }
                anim.SetFloat("Forwards", 1f);
                anim.speed = speed;
            }
            else
            {
                anim.SetFloat("Forwards", 0f);
            }

            if (horz > 0.05f)
            {
                float speed = horz * 3f;
                if (speed < 1f) { speed = 1f; }
                anim.SetFloat("Turning", 1f);
                anim.speed = speed;
            }
            else
            {
                anim.SetFloat("Turning", 0f);
            }

            // anim.SetFloat("Forwards", Input.GetAxis("Vertical"));
            // anim.SetFloat("Turning", Input.GetAxis("Horizontal"));
            anim.speed = 1;

        }

        if (Player.GetComponent<Movement>().CheckWallRun())
        {
           
        }
        else
        {
            anim.SetBool("WallRunning", false);
            anim.SetFloat("Wall Running", 0f);
        }
    }

    private void JumpingAndFalling()
    {
        bool testingThingsIDunno = false;

        

        float temp;
        bool hasJumped;

        if (Input.GetAxis("Jump") > 0 && !Player.GetComponent<Movement>().CheckGrounded())
        {
            temp = 1f;
        }
        else if (!Player.GetComponent<Movement>().CheckGrounded())
        {
            testingThingsIDunno = true;
            temp = 0f;
        }
        else
        {
            temp = 0f;
        }
        

        anim.SetBool("Falling", testingThingsIDunno);
        //Jumping animation
        anim.SetFloat("Jumping", temp);
        anim.speed = 2f;
    }
    private void Dead()
    {
        //Dying, need to tie this bool to character contacting wolf
        anim.SetBool("Dying", Dying);
    }
}