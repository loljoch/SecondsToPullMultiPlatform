using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nm.MyInput
{
    public class MouseInput : Iinput
    {
        private InputManager inputManager;
        private KeyCode leftMouseButton = KeyCode.Mouse0;
        private KeyCode rightMouseButton = KeyCode.Mouse1;
        private InputEvent inputEvent = new InputEvent(0, Vector2.zero, true);

        public MouseInput(InputManager inputManager)
        {
            this.inputManager = inputManager;
        }

        public void Run()
        {
            Vector2 mousePos = Input.mousePosition;
            if (Input.GetKeyDown(leftMouseButton))
            {
                inputEvent.startPosition = mousePos;
            } else if (Input.GetKeyUp(leftMouseButton))
            {
                inputEvent.endPosition = mousePos;
                inputManager.inputEvents.Add(inputEvent);
            }

            if (Input.GetKeyDown(rightMouseButton))
            {
                inputManager.inputEvents.Add(new InputEvent(0, mousePos, true));
            }
        }
    }
}
