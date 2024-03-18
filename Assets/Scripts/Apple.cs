using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public static float bottomLimitToDestroyApple = -5.5f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < bottomLimitToDestroyApple)
        {
            Destroy(gameObject);
        }
    }
}
