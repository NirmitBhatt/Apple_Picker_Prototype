using System;
using UnityEngine;

public class BasketController : MonoBehaviour
{
    public static event Action AppleCollect;
    private const string APPLE_STRING = "Apple";

    [SerializeField] private float leftScreenEdgeForBasket = -7.9f;
    [SerializeField] private float rightScreenEdgeForBasket = 7.9f;

    private bool pauseCounter = false;
    private float clampedMouseXPos3D;

    private void OnEnable()
    {
        UIPauseController.OnPauseGame += PauseCounter;
        UIPauseController.OnResumeGame += ResumeCounter;
    }

    private void OnDisable()
    {
        UIPauseController.OnPauseGame -= PauseCounter;
        UIPauseController.OnResumeGame -= ResumeCounter;
    }

    private void PauseCounter() => pauseCounter = true;

    private void ResumeCounter() => pauseCounter = false;

    void Update()
    {
        MoveBasketWithMouseCursor();
    }

    private void MoveBasketWithMouseCursor()
    {
        if (pauseCounter)
        {
            return;
        }

        Vector3 mousePosd2D = Input.mousePosition;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePosd2D);
        clampedMouseXPos3D = Mathf.Clamp(mousePos3D.x, leftScreenEdgeForBasket, rightScreenEdgeForBasket);
        transform.position = new Vector3(clampedMouseXPos3D, transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(APPLE_STRING))
        {
            Destroy(collision.gameObject);
            AppleCollect?.Invoke();
        }
    }
}
