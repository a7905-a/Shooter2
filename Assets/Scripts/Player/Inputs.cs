using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    //State
    public Vector2 movement { get; private set; }
    public Vector2 look { get; private set; }
    //Event
    public bool run { get; private set; }
    public bool zoom { get; private set; }
    //Action
    public bool shoot { get; private set; }
    public bool reload { get; private set; }
    public bool jump { get; private set; }

    public void OnMove(InputAction.CallbackContext value)
    {
        movement = value.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext value)
    {
        look = value.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext value)
    {
        run = value.ReadValueAsButton();

    }
    public void OnZoom(InputAction.CallbackContext value)
    {
        zoom = value.ReadValueAsButton();
    }

    public void OnShoot(InputAction.CallbackContext value)
    {
        if (value.ReadValueAsButton())
        {
            shoot = true;
        }
        else
        {
            shoot = false;
        }

    }
    public void OnReload(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            reload = true;
        }

    }
    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            jump = true;
        }
    }

    public void Resetjump()
    {
        jump = false;
    }
    public void Resetshoot()
    {
        shoot = false;
    }
    public void Resetreload()
    {
        reload = false;
    }

}
