using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public Selectable mouseOn;
    public Camera cameraTransform;
    public Input input;

    private void Start()
    {
        input = new Input();
        input.Enable();
        input.Mouse.LeftClick.performed += (context) =>
        {
            if (mouseOn != null) mouseOn.MouseClick();
        };
    }


    private void Update()
    {
        RaycastHit hit;
        Ray ray = cameraTransform.ScreenPointToRay(UnityEngine.Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            Selectable newOn = hit.collider.GetComponentInParent<Selectable>();
            if (newOn != mouseOn)
            {
                if (mouseOn != null) mouseOn.MouseExit();
                if (newOn != null)
                {
                    newOn.MouseEnter();
                }
                mouseOn = newOn;
            }
        }
    }
}
