using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInput : MonoBehaviour
{
    [SerializeField] private Hero _hero;

    public void Move(InputAction.CallbackContext context)
    {
        _hero.SetMoveDirection = context.ReadValue<Vector2>();
    }
    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
            _hero.Attack();
    }
    public void MousePos(InputAction.CallbackContext context)
    {
        _hero.SetAimPoint = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }
    private void Update()
    {
        _hero.SetAimPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}