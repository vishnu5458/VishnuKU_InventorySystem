using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollControll : MonoBehaviour
{
    [SerializeField] Collider[] cols;
    [SerializeField] Rigidbody[] rbodies;

    // Start is called before the first frame update
    void Start()
    {
        DisableRagdollComponent();
    }

    public void EnableRagdollComponent()
    {
        for (int i = 0; i < cols.Length; i++)
        {
            cols[i].isTrigger = false;
        }
        for (int i = 0; i < rbodies.Length; i++)
        {
            rbodies[i].isKinematic = false;
        }
    }

    public void DisableRagdollComponent()
    {
        for (int i = 0; i < cols.Length; i++)
        {
            cols[i].isTrigger = true;
        }
        for (int i = 0; i < rbodies.Length; i++)
        {
            rbodies[i].isKinematic = true;
        }
    }

}
