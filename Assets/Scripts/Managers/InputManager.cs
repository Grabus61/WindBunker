using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager current;

    private PlayerInput input;

    private void Awake()
    {
        current = this;
        input = GetComponent<PlayerInput>();
    }

    public void ChangeActionMap(string actionMapName)
    {
        input.SwitchCurrentActionMap(actionMapName);
    }

}
