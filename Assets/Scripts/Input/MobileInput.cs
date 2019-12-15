using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nm.MyInput
{
    public class MobileInput : Iinput
    {
        private InputManager inputManager;

        public MobileInput(InputManager inputManager)
        {
            this.inputManager = inputManager;
        }

        public void Run()
        {
            Touch[] touches = Input.touches;

            for (int i = 0; i < touches.Length; i++)
            {
                int eventIndex;
                if (touches[i].phase == TouchPhase.Began)
                {
                    inputManager.inputEvents.Add(new InputEvent(touches[i].fingerId, touches[i].position));
                }

                eventIndex = GetRegisteredInput(touches[i].fingerId);
                switch (touches[i].phase)
                {
                    case TouchPhase.Moved:
                        inputManager.inputEvents[eventIndex].endPosition = touches[i].position;
                        break;
                    case TouchPhase.Stationary:
                        break;
                    case TouchPhase.Ended:
                        inputManager.inputEvents[eventIndex].endPosition = touches[i].position;
                        inputManager.inputEvents[eventIndex].ready = true;
                        break;
                    case TouchPhase.Canceled:
                        inputManager.inputEvents.RemoveAt(eventIndex);
                        break;
                    default:
                        break;
                }
            }
        }

        private int GetRegisteredInput(int id)
        {
            for (int i = 0; i < inputManager.inputEvents.Count; i++)
            {
                if (inputManager.inputEvents[i].id == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
