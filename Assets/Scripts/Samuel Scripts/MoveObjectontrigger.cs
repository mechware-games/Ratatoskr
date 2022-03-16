using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveObjectontrigger : MonoBehaviour
{

    public enum LoopType
    {
        Once,
        PingPong,
        Repeat
    }

    public LoopType loopType;
    [SerializeField]
    private bool activate = false;
    float position;
    public new Rigidbody rigidbody;
    public Transform start;
    public Transform end;
    public float duration = 1;
    public AnimationCurve accelCurve;
    float time = 0f;
    float direction = 1f;

    public bool Activate { get => activate; set => activate = value; }

    private void Start()
    {
        start.position = rigidbody.position;
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
            var pos = transform.TransformPoint(Vector3.Lerp(start.localPosition, end.localPosition, curvePosition));
            Vector3 deltaPosition = pos - rigidbody.position;
            if (Application.isEditor && !Application.isPlaying)
                rigidbody.transform.position = pos;
            rigidbody.MovePosition(pos);

        }

        void LoopPingPong()
        {
            position = Mathf.PingPong(time, 1f);
        }

        void LoopRepeat()
        {
            position = Mathf.Repeat(time, 1f);
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
