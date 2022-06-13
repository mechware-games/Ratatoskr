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
    [SerializeField] private GameObject Player;
    [SerializeField] Transform GroundCheck;
    [SerializeField] private float TestingMoreThings;
    [SerializeField] private float IDonno;

    [SerializeField] private bool wallSwap;

    void Start()
    {
        //gets the player
        IDonno = TestingMoreThings;
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (Player != null)
        {
            Debug.Log("PLAYERPLAYER " + Player);
            if (!Player.GetComponent<Player>().HasDied() && !Player.GetComponent<Player>().restarting)
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
            }
            Dead();
        }
        else 
        {
            Debug.Log("PLAYER IS NULL");
            GetPlayer();
        }
    }

    private void GetPlayer()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void run()
    {
        float vert = 0;
        float horz = 0;
        if (!Player.GetComponent<Player>().HasDied()) 
        {
           vert = Mathf.Abs(Input.GetAxis("Vertical"));
           horz = Mathf.Abs(Input.GetAxis("Horizontal"));
        

        if (Player.GetComponent<Movement>().CheckGrounded() && !Player.GetComponent<Movement>().CheckNearWall())
        {
            MovingForBack = Input.GetAxis("Vertical");
            MovingLeftRight = Input.GetAxis("Horizontal");
            JumpAndFall = Input.GetAxis("Jump");
            TestingMoreThings = Player.transform.position.y;

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

    else if(!Player.GetComponent<Movement>().CheckGrounded() && ( vert < 0.05f || horz < 0.05f))
		{
            anim.SetFloat("Forwards", 0f);
            anim.SetFloat("Turning", 0f);
        }
    }
    }
    private void JumpingAndFalling()
    {
        bool testingThingsIDunno = false;

        float temp;
        bool hasJumped;

        temp = 0f;

        if (!Player.GetComponent<Movement>().CheckGrounded())
        {
            testingThingsIDunno = true;
            temp = 1f;
        }
        
        anim.SetBool("Falling", testingThingsIDunno);
        //Jumping animation
        anim.SetFloat("Jumping", temp);
        anim.speed = 2f;
    }
    private void Dead()
    {
        anim.SetBool("Dying", Player.GetComponent<Player>().HasDied());
    }
}