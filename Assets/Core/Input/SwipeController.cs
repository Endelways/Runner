using UnityEngine;

namespace GameInput
{
    public class SwipeController : MonoBehaviour
    {
        public static event OnSwipeInput SwipeEvent;
        public delegate void OnSwipeInput(Vector2 direction);
        private bool isMobile, isSwipping;
        private Vector2 previousPos, CurrentPos;
        public float Sensetivity = 1.0f;
        private void Start()
        {
            isMobile = Application.isMobilePlatform;
        }

        private void Update()
        {
            if (!isMobile)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isSwipping = true;
                    previousPos = Input.mousePosition;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    isSwipping = false;
                }
            }
            else
            {
                if (Input.touchCount > 0)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        isSwipping = true;
                        previousPos = Input.touches[0].position;
                    }            
                    else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        isSwipping = false;
                    }
                }

            }
            CheckSwipe();
        }
        private void CheckSwipe()
        {
            if(isSwipping)
            {
                CurrentPos = isMobile ? Input.GetTouch(0).position : Input.mousePosition;
                Vector2 difference = new Vector2() { x = CurrentPos.x - previousPos.x };
                if (SwipeEvent != null) SwipeEvent(difference);
                previousPos = CurrentPos;
            }
        }
    }
}