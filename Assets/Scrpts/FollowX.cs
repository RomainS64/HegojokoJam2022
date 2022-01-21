using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowX : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
    private void FixedUpdate()
    {
        transform.position= new Vector3(objectToFollow.position.x, 
            transform.position.y, 
            transform.position.z);
    }
}
