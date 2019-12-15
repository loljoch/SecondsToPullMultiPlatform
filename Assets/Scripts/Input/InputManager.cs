using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nm.MyInput
{
    public delegate void OnDrag(float power, Vector2 direction);
    public delegate void OnTap(Vector2 pos);

    interface Iinput
    {
        void Run();
    }

    public class InputManager : MonoBehaviour
    {
        private OnDrag OnDragDelegate { get; set; }
        private OnTap OnTapDelegate { get; set; }

        public List<InputEvent> inputEvents = new List<InputEvent>();

        private Iinput inputDevice;
        private Resolution resolution;
        private int resolutionAddUp { get { return resolution.width + resolution.height; } set { resolutionAddUp = value; } }
        private PlayerMovement playerMovement;
        private AudioManager audioManager;

        private void Awake()
        {
            audioManager = FindObjectOfType<AudioManager>();
            playerMovement = FindObjectOfType<PlayerMovement>();
            CamFollow2D camFollow2D = FindObjectOfType<CamFollow2D>();
#if UNITY_ANDROID
            Debug.Log("Android");
            inputDevice = new MobileInput(this);
#elif UNITY_STANDALONE_WIN
            if (Input.mousePresent)
            {
                Debug.Log("Windows mouse");
                inputDevice = new MouseInput(this);
            }
#endif
            OnDragDelegate += camFollow2D.OnPlayerMove;
            OnDragDelegate += playerMovement.PushOff;
            OnDragDelegate += (float power, Vector2 direction) => audioManager.soundManager.PlayAudioClip(audioManager.wooshAudio, Mathf.Clamp(power, 0, 1));

            resolution = Screen.currentResolution;
        }

        private void OnDisable()
        {
            OnDragDelegate -= playerMovement.PushOff;
        }

        private void Update()
        {
            inputDevice.Run();
            CheckForInput();
        }

        private void CheckForInput()
        {
            //backward loop
            for (int i = inputEvents.Count - 1; i > -1; i--)
            {
                if (inputEvents[i].ready)
                {
                    if (inputEvents[i].isDrag)
                    {
                        OnDragCalculations(inputEvents[i].startPosition, inputEvents[i].endPosition);
                        inputEvents.RemoveAt(i);
                    } else
                    {
                        OnTapDelegate?.Invoke(inputEvents[i].startPosition);
                        inputEvents.RemoveAt(i);
                    }
                }
            }
        }

        private void OnDragCalculations(Vector2 a, Vector2 b)
        {
            Vector2 direction = a - b;
            float distance = direction.magnitude;
            direction /= distance;
            distance /= resolutionAddUp;
            OnDragDelegate(distance, direction);
        }
    }
}
