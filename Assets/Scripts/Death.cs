using System;
using UnityEngine;

public class Death : MonoBehaviour
{
    private const string APPLE_STRING = "Apple";

    public event Action OnAppleDropped;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(APPLE_STRING))
        {
            OnAppleDropped?.Invoke();
        }
    }
}