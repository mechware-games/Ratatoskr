using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScipt : MonoBehaviour
{
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        //Squirrel walking animation. Needs to be tied to momentum of the squirrel instead of direction
        anim.SetFloat("Forwards", Input.GetAxis("Vertical"));
        anim.SetFloat("Forwards", Input.GetAxis("Vertical"));
        //Jumping below here, when jump is pressed, set Airborn to 1, when not touching the ground, set airborn to 2, when touching the ground, make airborn go from 2 to 0
        //Dying, when dead, set Dying float to 1
    }
}
