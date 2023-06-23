using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltScript : MonoBehaviour
{
    public bool move = false;
    public float movementSpeed = 0.0002f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
                    child.position = new Vector3(0, 0, 6.5f);
                }
            }
        }
    }
}
