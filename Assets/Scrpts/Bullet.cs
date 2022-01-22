using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed = 1f;
    public void shootBullet(Vector3 position, Vector3 rotation, Vector3 direction)
    {
        transform.position = position;
        transform.rotation = Quaternion.Euler(rotation);
        StartCoroutine(moveBullet(direction));
    }

    IEnumerator moveBullet(Vector3 direction)
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(5/100);
            transform.Translate(direction * speed);
        }
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
