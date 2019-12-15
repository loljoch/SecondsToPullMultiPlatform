using UnityEngine;

namespace nm.MyInput
{
    public class InputEvent
    {
        public int id;
        public Vector2 startPosition, endPosition;
        public bool isDrag
        {
            get
            {
                if (Vector2.Distance(startPosition, endPosition) > 0.4f)
                {
                    return true;
                }
                return false;
            }
            private set { isDrag = value; }
        }
        public bool ready = false;

        public InputEvent()
        {

        }

        public InputEvent(int id, Vector2 startPosition, bool isReady = false)
        {
            this.id = id;
            this.startPosition = startPosition;
            this.ready = isReady;
        }

        public InputEvent(int id, Vector2 startPosition, Vector2 endPosition, bool isReady = true)
        {
            this.id = id;
            this.startPosition = startPosition;
            this.endPosition = endPosition;
            this.ready = isReady;
        }
    }
}
