using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace movestuff
{
    public class SendOnTriggerEnter : MonoBehaviour
    {
        public LayerMask layers;


        public ActionType interactionType;
        public ActionReciever interactiveObject;
        public bool oneShot = false;
        public float coolDown = 1;
        public AudioSource onSendAudio;
        public float audioDelay;

        float lastSendTime;
        bool isTriggered = false;

        void OnTriggerEnter(Collider other)
        {
            if (0 != (layers.value & 1 << other.gameObject.layer))
            {
                if (oneShot && isTriggered) return;
                if (Time.time - lastSendTime < coolDown) return;
                isTriggered = true;
                lastSendTime = Time.time;
                interactiveObject.Receive(interactionType);
                if (onSendAudio) onSendAudio.PlayDelayed(audioDelay);
            }
        }

        protected void Reset()
        {
            if (LayerMask.LayerToName(gameObject.layer) == "Default")
                gameObject.layer = LayerMask.NameToLayer("Environment");
            var c = GetComponent<Collider>();
            if (c != null)
                c.isTrigger = true;

            interactiveObject = GetComponent<ActionReciever>();
        }
    }
}