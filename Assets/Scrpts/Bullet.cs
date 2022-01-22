using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed;
    public void ShootBullet(Vector3 position, Quaternion rotation, Vector3 dir)
    {
        AkSoundEngine.PostEvent("PlayGunSound", gameObject);
        Debug.Log("Rotation : " + rotation);
        transform.position = position;
        transform.rotation = rotation;
        
        StartCoroutine(moveBullet(dir));
    }

    IEnumerator moveBullet(Vector3 direction)
    {
        for (int i = 0; i < 1000; i++)
        {
            transform.position += direction * speed/10;
            yield return new WaitForSeconds(5f/1000);
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
