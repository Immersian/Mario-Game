using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle_dead : MonoBehaviour
{
    private Collider beetleCollider;

    void OnEnable()
    {
        // Destroy the game object 1 second after it is enabled
        Invoke("DestroyGameObject", 1f);

    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}



