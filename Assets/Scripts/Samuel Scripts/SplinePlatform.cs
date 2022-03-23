using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;


public class SplinePlatform : MonoBehaviour
{

    public enum LoopType
    {
        Once,
        PingPong,
        Repeat
    }

    public SplineContainer path;
    public LoopType loopType;
    [SerializeField]
    private bool activate = false;
    float position;
    public new Rigidbody rigidbody;
    public float duration = 1;
    public AnimationCurve accelCurve;
    float time = 0f;
    float direction = 1f;
    int pathnodes;
    float3 previous;
    float3 next;
    int nextnode = 2;

    public bool Activate { get => activate; set => activate = value; }

    private void Start()
    {
        previous = path.Spline[0].Position;
        next = path.Spline[1].Position;
        pathnodes = path.Spline.Count;
        Debug.Log(pathnodes);
        Debug.Log(path.Spline[1].Position);
    }


    public void FixedUpdate()
    {
        if (Activate)
        {
            time = time + (direction * Time.deltaTime / duration);
            switch (loopType)
            {
                case LoopType.Once:
                    Once();
                    break;
                case LoopType.PingPong:
                    LoopPingPong();
                    break;
                case LoopType.Repeat:
                    LoopRepeat();
                    break;
            }
            PerformTransform(position);
        }
    }

    public void PerformTransform(float position)
    {
        var curvePosition = accelCurve.Evaluate(position);
        var pos = transform.TransformPoint(Vector3.Lerp(previous, next, curvePosition));
        Vector3 deltaPosition = pos - rigidbody.position;
        rigidbody.MovePosition(pos);
    }

    void LoopPingPong()
    {
        position = Mathf.PingPong(time, 1f);
        Debug.Log(position);
    }

    void LoopRepeat()
    {
        position = Mathf.Repeat(time, 1f);
        if (position >= 0.98f)
        {
            previous = next;
            if (nextnode == pathnodes)
            {
                next = path.Spline[nextnode - 1].Position;
            }
            else
            {
                next = path.Spline[nextnode].Position;
            }

            position = 0;
            if (nextnode == pathnodes)
            {
                if (path.Spline.Closed)
                {
                    nextnode = 0;
                    previous = next;
                    next = path.Spline[0].Position;
                }
                else
                {
                    nextnode = 1;
                    previous = path.Spline[0].Position;
                    next = path.Spline[1].Position;
                }
            }
            nextnode++;
            Debug.Log(nextnode);
        }
    }

    void Once()
    {
        position = Mathf.Clamp01(time);
        if (position >= 1)
        {
            enabled = false;
            direction *= -1;
        }
    }
}
