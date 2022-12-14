using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyTraceBullet : DamageController
{
    public float bulletSpeed;
    public float distance;
    public LayerMask isLayer;
    public float bulletDamage;
    float var;
    public Transform box; // �÷��̾� ���� �ڽ�
    public Vector2 boxSize; // // �÷��̾� ���� �ݰ�

    private void Awake()
    {

    }

    void Start()
    {

    }


    void Update()
    {
        StartCoroutine("Trace");
        Invoke("DestroyBullet", 3);
    }
    //public void ShotLeftBullet()
    //{
    //    var = -1;
    //}
    //public void ShotRightBullet()
    //{
    //    var = 1;
    //}
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        DestroyBullet();
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<DamageController>()?.PlayerOnDamage(bulletDamage);
            //Rigidbody2D PlayerRigid = collision.GetComponent<Rigidbody2D>();
            //PlayerRigid.AddForce(new Vector2(0, 1) * 5, ForceMode2D.Impulse);
            DestroyBullet();
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        collision.gameObject.GetComponent<DamageController>()?.PlayerOnDamage(bulletDamage);
    //        Rigidbody2D PlayerRigid = collision.GetComponent<Rigidbody2D>();
    //        PlayerRigid.AddForce(new Vector2(0, 1) * 5, ForceMode2D.Impulse);
    //        DestroyBullet();
    //    }
    //}
    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    IEnumerator Trace()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(box.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            float bulletSpeedDown;
            bulletSpeedDown = bulletSpeed;
            bulletSpeedDown -= Time.deltaTime * 1f;
            if (collider.tag == "Player")
            {
                transform.position = Vector3.MoveTowards(transform.position, collider.transform.position, Time.deltaTime * bulletSpeedDown);
            }

        }
        yield return new WaitForSeconds(0.5f);
    }

}
