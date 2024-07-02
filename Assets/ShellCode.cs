using UnityEngine;

public class ShellMovement : MonoBehaviour
{
    public float speed = 5f; // Adjust the speed of the shell

    private bool isMoving = false;
    private Vector3 direction;

    void Update()
    {
        if (isMoving)
        {
            // Move the shell in the set direction
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Stop the shell when it hits a wall
            isMoving = false;
            // Optionally, you can add code to destroy the shell or handle other logic
        }
    }

    public void StartMoving(Vector3 moveDirection)
    {
        isMoving = true;
        direction = moveDirection;
    }
}



