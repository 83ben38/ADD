using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;


public abstract class TowerState
{
    public abstract void Run(TowerController controller);
    public abstract void MouseClick(TowerController controller);

    
}
