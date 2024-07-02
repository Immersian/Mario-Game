using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetectMovement : MonoBehaviour
{
    public Transform platform;
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 1.5f;
    private int targetIndex = 0;
    private Vector3[] targets;
    private bool isFacingRight = true;
    public GameObject SnailAlive;
    public GameObject Shell;

    private void Start()
    {
        // Define the sequence of movement targets
        targets = new Vector3[] { startPoint.position, endPoint.position };
    }

    private void Update()
    {
        // Get the current movement target
        Vector3 target = targets[targetIndex];

        // Move the platform towards the target
        platform.position = Vector3.MoveTowards(platform.position, target, speed * Time.deltaTime);

        // Check if the platform has reached close enough to the target
        if (Vector3.Distance(platform.position, target) <= 0.1f)
        {
            // Move to the next target index
            targetIndex = (targetIndex + 1) % targets.Length;

            // Flip the platform
            Flip();
        }
    }

    private void Flip()
    {
        // Toggle the direction the platform is facing
        isFacingRight = !isFacingRight;

        // Multiply the platform's x local scale by -1 to flip it
        Vector3 platformScale = platform.localScale;
        platformScale.x *= -1;
        platform.localScale = platformScale;
    }

    public void TakeDamage()
    {
        // Disable the SnailAlive GameObject
        SnailAlive.SetActive(false);

        // Enable the Shell GameObject at the Snail's position
        Shell.transform.position = SnailAlive.transform.position;
        Shell.SetActive(true);
    }
}
