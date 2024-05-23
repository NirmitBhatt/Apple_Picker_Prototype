using UnityEngine;

public class AppleTreeMovement : MonoBehaviour
{
    public float speed = 1f;
    public float chanceToChangeDirections = 0.0001f;
    public float leftScreenEdge = -10f;
    public float rightScreenEdge = 10f;
    
    Vector3 pos;
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveAppleTree();
    }

    private void FixedUpdate()
    {
        ChangeTreeDirectionRandomly();
        
    }


    private void MoveAppleTree()
    {
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
        ChangeTreeDirection();
    }
    private void ChangeTreeDirection()
    {
        if (pos.x < leftScreenEdge)
        {
            speed = Mathf.Abs(speed);
        }
        else if (pos.x > rightScreenEdge)
        {
            speed = -Mathf.Abs(speed);
        }
    }

    private void ChangeTreeDirectionRandomly()
    {
        if (Random.value < chanceToChangeDirections)
        {
            speed *= -1;
        }
    }
}
