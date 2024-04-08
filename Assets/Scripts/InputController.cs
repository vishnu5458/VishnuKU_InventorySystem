using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    public delegate void OnMove();
    public static OnMove onMove;

    public delegate void OnPick();
    public static OnPick onPick;

    public delegate void OnDrop();
    public static OnDrop onDrop;

    public delegate void OnFirstPerson();
    public static OnFirstPerson onFirstPerson;

    public delegate void OnThirdPerson();
    public static OnThirdPerson onThirdPerson;

    public delegate void OnFire();
    public static OnFire onFire;

    // Update is called once per frame
    void Update()
    {
        onMove?.Invoke();
    }

    public void OnPickUpTapped()
    {
        onPick?.Invoke();
    }

    public void OnFirstPersonTapped()
    {
        onFirstPerson?.Invoke();
    }

    public void OnThirdPersonTapped()
    {
        onThirdPerson?.Invoke();
    }

    public void OnFireTapped()
    {
        onFire.Invoke();
    }

    public void OnReset()
    {
        SceneManager.LoadScene(0);
    }
}
