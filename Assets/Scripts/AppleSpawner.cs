using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    public GameObject applePrefab;
    public float secsBetweenAppleDrop = 1f;
    Vector3 pos;

    private float appleLinearDrag;
    private float appleGravityScale;
    private float currentTime = 0;

    private void Update()
    {
        HandleAppleSpawn();
        AssignSpawnPosition();
    }

    private void HandleAppleSpawn()
    {
        currentTime += Time.deltaTime;
        if (currentTime > secsBetweenAppleDrop)
        {
            SpawnAppleToDrop();
            currentTime = 0;
        }
    }

    private void AssignSpawnPosition()
    {
        pos = transform.position;
        pos.y = 2.2f;
    }

    public void SetSecsBetweenAppleDrop(float newSecsBetweenAppleDrop)
    {
        secsBetweenAppleDrop = newSecsBetweenAppleDrop;
    }

    public void SetAppleRigidbodyValues(float linearDrag, float gravityScale)
    {
        appleLinearDrag = linearDrag;
        appleGravityScale = gravityScale;
    }

    public void SpawnAppleToDrop()
    {
        GameObject newApple = Instantiate(applePrefab, pos, Quaternion.identity);
        ApplePrefabManager applePrefabManager = newApple.GetComponent<ApplePrefabManager>();
        if (applePrefabManager != null)
        {
            applePrefabManager.linearDrag = appleLinearDrag;
            applePrefabManager.gravityScale = appleGravityScale;
            applePrefabManager.ApplyRigidbodyValues();
        }
        FindObjectOfType<AudioManager>().PlayAudio("AppleSpawn");
    }
}
