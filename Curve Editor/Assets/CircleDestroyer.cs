using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDestroyer : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
