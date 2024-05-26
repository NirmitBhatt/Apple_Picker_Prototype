using UnityEngine;

public class ApplePrefabManager : MonoBehaviour
{
    public float linearDrag;
    public float gravityScale;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ApplyRigidbodyValues();
    }

    public void ApplyRigidbodyValues()
    {
        if (rb != null)
        {
            rb.drag = linearDrag;
            rb.gravityScale = gravityScale;
        }
        else
        {
            Debug.LogWarning("No Rigidbody component found!");
        }
    }

}
