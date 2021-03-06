using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private bool hasShot = false;
    private Camera camera;
    private SpriteRenderer sprite;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float coolDown;
    [SerializeField] private GameObject weaponMouth;

    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<Camera>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 Point_2 = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 Point_1 = transform.position;
        float aimAngle = Mathf.Atan2(Point_2.y - Point_1.y, Point_2.x - Point_1.x) * 180 / Mathf.PI;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, aimAngle));
        if (!hasShot && Input.GetAxis("Shot") > 0)
        {
            hasShot = true;
            Invoke(nameof(StopCoolDown), coolDown);
            GameObject newBullet = Instantiate(bullet);
            var dir = (Point_2 - Point_1).normalized;
            newBullet.GetComponent<Bullet>().ShootBullet(weaponMouth.transform.position, transform.rotation, dir);
            
        }

        if (Point_2.x > transform.position.x)
        {
            sprite.flipY = false;
        }
        else
        {
            sprite.flipY = true;
        }
    }
    void StopCoolDown()
    {
        hasShot = false;
    }
}