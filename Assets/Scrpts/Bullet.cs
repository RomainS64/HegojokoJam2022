using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed = 1f;
    public void ShootBullet(Vector3 position, Quaternion rotation, Vector3 dir)
    {
        
        Debug.Log("Rotation : " + rotation);
        transform.position = position;
        transform.rotation = rotation;
        
        StartCoroutine(moveBullet(dir));
    }

    IEnumerator moveBullet(Vector3 direction)
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(5/100);
            transform.position += direction * speed/10;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Nounours")
        {
            collision.gameObject.GetComponent<Enemy>().Kill();
            Destroy(gameObject);
        }
    }
}
