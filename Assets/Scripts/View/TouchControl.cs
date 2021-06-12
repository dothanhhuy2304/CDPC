using UnityEngine;

namespace CDPC.View
{
    public class TouchControl : MonoBehaviour
    {
        bool moveAllow;
        public float speed;
        RaycastHit2D touchCollider;

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                if (touch.phase == TouchPhase.Began)
                {
                    touchCollider = Physics2D.Raycast(touchPosition, Vector3.zero);
                    if (touchCollider)
                    {
                        if (touchCollider.collider.CompareTag("View"))
                        {
                            moveAllow = true;
                        }
                    }
                    else
                    {
                        moveAllow = false;
                    }
                }
                if (touch.phase == TouchPhase.Moved)
                {
                    if (moveAllow)
                    {
                        Vector3 of = new Vector3(touchPosition.sqrMagnitude, Camera.main.transform.position.y, Camera.main.transform.position.z);
                        Camera.main.transform.position = of * Time.deltaTime;
                    }
                    else
                    {
                        return;
                    }
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    moveAllow = false;
                }
            }
        }
    }
}