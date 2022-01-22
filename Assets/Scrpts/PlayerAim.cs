using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private Transform transfArm;
    private Vector2 mousePos;
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        transfArm = GetComponent<Transform>();
        camera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Point_1 = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 Point_2 = transfArm.position;
        float aimAngle = Mathf.Atan2(Point_2.y - Point_1.y, Point_2.x - Point_1.x) * 180 / Mathf.PI;
        Debug.Log(aimAngle);
        transfArm.transform.rotation = Quaternion.Euler(new Vector3(0, 0, aimAngle));
        Aim();
    }

    void Aim()
    {
    }
}