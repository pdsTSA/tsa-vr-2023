using UnityEngine;

namespace Resources.Scripts.Core
{
    public class BeltScript : MonoBehaviour
    {
        public bool move = false;
        public float movementSpeed = 0.01f;

        // Update is called once per frame
        void Update()
        {
            if (move)
            {
                foreach (Transform child in transform)
                {
                    child.position -= new Vector3(0, 0, movementSpeed);
                    if (child.position.z <= 0.5)
                    {
                        child.position = new Vector3(0, 0, 6.45f);
                    }
                }
            }
        }
    }
}
