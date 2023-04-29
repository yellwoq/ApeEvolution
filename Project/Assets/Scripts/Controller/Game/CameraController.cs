using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
    //*************
    //Author:27-QSHF
    //Time:2023-2-11
    //…„œÒÕ∑Õœ∂Ø
    //**********
    public class CameraController : MonoBehaviour
    {
        private Vector2 dragStartPos, dragCurrentPos;
        private Vector3 newPos;
        public bool interacting = false;
        [SerializeField] float moveTime = 1f;

        [SerializeField] float scaleTime;

        [SerializeField]
        public Vector2 LB;
        [SerializeField]
        public Vector2 RT;

        private void Start()
        {
            newPos = transform.position;

        }
        private void FixedUpdate()
        {
            HandleCameraMove();
        }
        private void HandleCameraMove()
        {
            if (!interacting)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                if (Input.GetMouseButton(1))
                {
                    dragCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 difference = dragStartPos - dragCurrentPos;
                    newPos = new Vector3(transform.position.x + difference.x, transform.position.y + difference.y, transform.position.z);

                    newPos=new Vector3(Mathf.Clamp(newPos.x, LB.x, RT.x), Mathf.Clamp(newPos.y, LB.y, RT.y), transform.position.z);
                }

                transform.position = Vector3.Lerp(transform.position, newPos, moveTime * Time.deltaTime);

            }

            Camera.main.orthographicSize = Camera.main.orthographicSize - Input.GetAxis("Mouse ScrollWheel") * scaleTime;

        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(LB, 0.5f);
            Gizmos.DrawWireSphere(RT, 0.5f);
        }
    }
}
