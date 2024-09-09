using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputs : MonoBehaviour
{
    public GameObject MenuPanel;
    public Vector2 MousePosition { get; private set; } = Vector2.zero;
    public event Action OnShootEvent;

    private Camera _mainCamera;
    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    public void OnMove(InputValue value)
    {
        
        MousePosition = _mainCamera.ScreenToWorldPoint(value.Get<Vector2>());
    }
    public void OnShoot(InputValue value) 
    {
        if (value.isPressed) 
        {
            OnShootEvent?.Invoke();
        }
    }
    public void OnOpenMenu(InputValue value) 
    {
        if (value.isPressed) 
        {
            if (MenuPanel.activeSelf) 
            {
                MenuPanel.SetActive(false);
                Time.timeScale = 1f;
            }
            else 
            {
                MenuPanel.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}
