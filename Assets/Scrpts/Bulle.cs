using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DestroyObject), 0.5f);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
