using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace movestuff
{
    public class MoveObjectontrigger : MonoBehaviour
    {

        public enum LoopType
        {
            Once,
            PingPong,
            Repeat
        }

        public LoopType loopType;

        public bool activate = false;
        float position;
        public new Rigidbody rigidbody;
        //public Vector3 start = -Vector3.forward;
        //public Vector3 end = Vector3.forward;
        public Transform start;
        public Transform end;
        public float duration = 1;
        public AnimationCurve accelCurve;
        float time = 0f;
        float direction = 1f;

        public void FixedUpdate()
        {
            //if (activate)
            //{
                time = time + (direction * Time.deltaTime / duration);
                switch (loopType)
                {
                    case LoopType.Once:
                        LoopOnce();
                        break;
                    case LoopType.PingPong:
                        LoopPingPong();
                        break;
                    case LoopType.Repeat:
                        LoopRepeat();
                        break;
                }
                PerformTransform(position);
            //}
        }


        public void PerformTransform(float position)
        {
            var curvePosition = accelCurve.Evaluate(position);
            var pos = transform.TransformPoint(Vector3.Lerp(start.position, end.position, curvePosition));
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

        void LoopOnce()
        {
            position = Mathf.Clamp01(time);
            if (position >= 1)
            {
                enabled = false;
                direction *= -1;
            }
        }

		private void OnTriggerEnter(Collider other)
		{
			if (other.tag == "Player")
			{
                other.gameObject.transform.parent.parent = transform;
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.tag == "Player")
			{
                other.gameObject.transform.parent.parent = null;
			}
		}
	}
}
