using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    public Vector2 movement { get; private set; }

    public Vector2 look;
    public bool run = false;
    public bool zoom;
    public bool shoot;
    public bool reload;
    public bool jump;
    public void OnMove(InputAction.CallbackContext value)
    {
        movement = value.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext value)
    {
        look = value.ReadValue<Vector2>();
    }

    public void OnZoom(InputAction.CallbackContext value)
    {
        zoom = value.ReadValueAsButton();
    }

    public void OnRun(InputAction.CallbackContext value)
    {
        run = value.ReadValueAsButton();

    }
    public void OnShoot(InputAction.CallbackContext value)
    {
        shoot = value.ReadValueAsButton();

    }
    public void OnReload(InputAction.CallbackContext value)
    {
        reload = value.ReadValueAsButton();

    }
    public void OnJump(InputAction.CallbackContext value)
    {
        jump = value.ReadValueAsButton();
    }

}
