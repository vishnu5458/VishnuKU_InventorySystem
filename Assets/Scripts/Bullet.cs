using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;
    [SerializeField] float bulletSpeed = 10;
    Vector3 direction;
    bool isMove = false;

    private void OnEnable()
    {
        Destroy(gameObject, 5);
    }

    public void InitMove(Vector3 _pos)
    {
        isMove = true;
        direction = _pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
            rigid.velocity = (direction * bulletSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            other.transform.GetComponent<EnemyCongtroller>().ImpactHealth(20);
            Destroy(gameObject);
        }
    }
}
