using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    public abstract void MouseEnter();
    

    public abstract void MouseExit();

    public abstract void MouseClick();
}
