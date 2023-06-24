using System;
using UnityEngine;

namespace Resources.Scripts.Core
{
    public class DoorMovement : MonoBehaviour
    {
        public bool open = false;
        public bool moving = false;
        public float speed = 0.15f;

        // Update is called once per frame
        void Update()
        {
            if (open)
            {
                if (transform.position.y < 1)
                {
                    transform.position += new Vector3(0, speed, 0);
                    moving = true;
                }
                else
                {
                    moving = false;
                }
            }
            else
            {
                if (transform.position.y > 0)
                {
                    transform.position -= new Vector3(0, speed, 0);
                    moving = true;
                }
                else
                {
                    moving = false;
                }
            }
        }
    }
}
