using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zemar : MonoBehaviour
{
    private Vector2 pointToMoveOn;
    private float zemarSpeed;

    public Transform defaultPosition;
    public Transform positionToComeFromRight;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        pointToMoveOn = transform.position;
        zemarSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, pointToMoveOn, zemarSpeed * Time.deltaTime);
        //Vector2.MoveTowards(transform.position, pointToMoveOn, zemarSpeed * Time.deltaTime);
    }

    public void Move(Vector2 endPoint, float speed)
    {
        pointToMoveOn = endPoint;
        zemarSpeed = speed;
    }

    public void MoveToDefaultPosition(float speed)
    {
        pointToMoveOn = defaultPosition.position;
        zemarSpeed = speed;
    }

    public void AppearFromRightSide(float speed)
    {
        transform.position = positionToComeFromRight.transform.position;
        MoveToDefaultPosition(speed);
    }

    public void SetSpriteLookRight(bool lookRight)
    {
        spriteRenderer.flipX = lookRight;
    }
}
