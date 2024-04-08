using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;
    [SerializeField] RagdollControll ragdoll;

    [SerializeField]
    GameObject[] weapon;
    [SerializeField] GameObject thirdPerson;
    [SerializeField]
    ParticleSystem[] muzzleFlashs;
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float rotationSpeed = 1.0f;
    [SerializeField] int health = 100;

    Item item;
    bool IsWalking = false;

    private void OnEnable()
    {
        InputController.onMove += Move;
        InputController.onPick += PickItem;
        InputController.onDrop += DropItem;
        InputController.onFirstPerson += ChangeFirstPerson;
        InputController.onThirdPerson += ChangeThirdPerson;
        InputController.onFire += Fire;
    }

    private void OnDisable()
    {
        InputController.onMove -= Move;
        InputController.onPick -= PickItem;
        InputController.onDrop -= DropItem;
        InputController.onFirstPerson += ChangeFirstPerson;
        InputController.onThirdPerson += ChangeThirdPerson;
        InputController.onFire += Fire;
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);

        if (movementDirection != Vector3.zero)
        {
            if (!IsWalking)
            {
                IsWalking = true;
                anim.SetBool("Walking", true);
            }
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (IsWalking)
            {
                IsWalking = false;
                anim.SetBool("Walking", false);
            }
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        item = other.gameObject.GetComponent<Item>();
    }

    private void OnTriggerExit(Collider other)
    {
        item = null;
    }

    void PickItem()
    {
        if (item == null)
            return;
        anim.SetTrigger("Pick");
        GameManager.Instance.AddToInventory(item);
        item.Picked();
        item = null;
    }

    void DropItem()
    {
        anim.SetTrigger("Drop");
    }

    void ChangeFirstPerson()
    {
        thirdPerson.SetActive(false);
        weapon[0].SetActive(false);
        weapon[1].SetActive(false);
        weapon[GameManager.Instance.weaponType].SetActive(true);
        GameManager.Instance.thirdPersonButton.SetActive(true);
        GameManager.Instance.firstPersonButton.SetActive(false);
        GameManager.Instance.fireButton.SetActive(true);
    }

    void ChangeThirdPerson()
    {
        thirdPerson.SetActive(true);
        weapon[0].SetActive(false);
        weapon[1].SetActive(false);
        GameManager.Instance.firstPersonButton.SetActive(true);
        GameManager.Instance.thirdPersonButton.SetActive(false);
        GameManager.Instance.fireButton.SetActive(false);
    }

    void Fire()
    {
        muzzleFlashs[GameManager.Instance.weaponType].Stop();
        muzzleFlashs[GameManager.Instance.weaponType].Play();
        GameObject go = Instantiate(bulletPrefab, muzzleFlashs[GameManager.Instance.weaponType].transform.position, muzzleFlashs[GameManager.Instance.weaponType].transform.rotation);
        go.GetComponent<Bullet>().InitMove(Camera.main.transform.forward);
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
}
