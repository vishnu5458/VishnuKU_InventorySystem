using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCongtroller : MonoBehaviour
{
    [SerializeField] RagdollControll ragdoll;
    [SerializeField] ParticleSystem fireVfx;
    [SerializeField] float moveSpeed = 3;
    GameObject player;
    int health = 10;
    float nextFire = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist > 1f)
            {
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
            }
            else
            {
                if (nextFire < Time.time)
                    StartCoroutine(Fire());
            }
            float x = transform.rotation.eulerAngles.x;
            float z = transform.rotation.eulerAngles.z;
            Quaternion tempRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Euler(x, tempRotation.eulerAngles.y, z);
        }
    }

    public void ImpactHealth(int _impact)
    {
        health -= _impact;
        if (health < 0)
        {
            ragdoll.EnableRagdollComponent();
            GameManager.Instance.OnGameOver();
        }
    }

    IEnumerator Fire()
    {
        nextFire = 10 + Time.time;
        PlayerController controller = player.GetComponent<PlayerController>();
        yield return new WaitForSeconds(1);
        fireVfx.Stop();
        fireVfx.Play();
        for (int i = 0; i<10; i++)
        {
            controller.ImpactHealth(1);
            yield return new WaitForSeconds(0.1f);
        }
        fireVfx.Stop();
    }
}
