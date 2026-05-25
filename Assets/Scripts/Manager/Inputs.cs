using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectTwo.Manager
{
    public class Inputs : MonoBehaviour
    {
        //프로퍼티를 활용한 접근 제어
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
        public bool toggleInventory {get; private set;}
        public bool interactItem {get; private set;}
        public bool interactAction {get; private set;}

        //Invoke Unity Events 방식에서 요구하는 메서드
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
            shoot = value.ReadValueAsButton();
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

        public void OnToggleInventory(InputAction.CallbackContext value)
        {
            if (value.started)
            {
                toggleInventory = true;
            }
        }

        public void OnInteractItem(InputAction.CallbackContext value)
        {
            if (value.started)
            {
                interactItem = true;
            }
        }

        public void OnInteractAction(InputAction.CallbackContext value)
        {
            if (value.started)
            {
                interactAction = true;
            }
        }

    //단발성 이벤트의 처리를 위한 리셋 메서드
        public void ResetJump()
        {
            jump = false;
        }
        public void ResetShoot()
        {
            shoot = false;
        }
        public void ResetReload()
        {
            reload = false;
        }
        public void ResetToggleInventory()
        {
            toggleInventory = false;
        }

        public void ResetInteractItem()
        {
            interactItem = false;
        }

        public void ResetInteractAction()
        {
            interactAction = false;
        }

    }
}
