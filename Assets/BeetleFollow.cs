using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowing : MonoBehaviour
{
    public int speed;
    public LayerMask targetLayer;
    public float fixedYPosition;

    void Start()
    {
        // Set the initial position to the fixed Y position
        Vector3 startPosition = new Vector3(transform.position.x, fixedYPosition, transform.position.z);
        transform.position = startPosition;
    }

    void Update()
    {
        GameObject player = FindTargetWithTag("Player");

        if (player != null)
        {
            Vector3 targetPosition = GetFixedYPosition(player.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    GameObject FindTargetWithTag(string targetTag)
    {
        return GameObject.FindGameObjectWithTag(targetTag);
    }

    Vector3 GetFixedYPosition(Vector3 originalPosition)
    {
        return new Vector3(originalPosition.x, fixedYPosition, transform.position.z);
    }

    void OnDrawGizmos()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.transform.position);
        }
    }
}

