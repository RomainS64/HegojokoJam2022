using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteMovement : MonoBehaviour
{
    public float fallingSpeed = 10;
    public float limitBeforeDestroy = -10;
    public bool isSleeping = false;

    // Update is called once per frame
    void Update()
    {
        if(!isSleeping)
        {
            transform.Translate(Vector2.down * fallingSpeed * Time.deltaTime);

            if (transform.position.y < limitBeforeDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
}
