using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class Basket : MonoBehaviour
{
    [SerializeField] private float leftScreenEdgeForBasket = -10.1f;
    [SerializeField] private float rightScreenEdgeForBasket = 10.1f;
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosd2D = Input.mousePosition;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePosd2D);
        float clampedMouseXPos3D = Mathf.Clamp(mousePos3D.x, leftScreenEdgeForBasket, rightScreenEdgeForBasket);
        transform.position = new Vector3(clampedMouseXPos3D, transform.position.y, transform.position.z);
    }
}
