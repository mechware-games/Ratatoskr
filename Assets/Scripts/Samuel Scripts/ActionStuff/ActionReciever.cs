using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace movestuff
{
    //Class used to call the proper GameCommandHandler subclass to a given GameCommandType received from a subclass of SendGameCommand
    public class ActionReciever : MonoBehaviour
    {
        Dictionary<ActionType, List<System.Action>> handlers = new Dictionary<ActionType, List<System.Action>>();

        public void Receive(ActionType e)
        {
            List<System.Action> callbacks = null;
            if (handlers.TryGetValue(e, out callbacks))
            {
                foreach (var i in callbacks) i();
            }
        }

        public void Register(ActionType type, ActionHandler handler)
        {
            List<System.Action> callbacks = null;
            if (!handlers.TryGetValue(type, out callbacks))
            {
                callbacks = handlers[type] = new List<System.Action>();
            }
            callbacks.Add(handler.OnInteraction);
        }

        public void Remove(ActionType type, ActionHandler handler)
        {
            handlers[type].Remove(handler.OnInteraction);
        }


    }

}