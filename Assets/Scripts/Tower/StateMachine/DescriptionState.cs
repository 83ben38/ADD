using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DescriptionState : TowerState
{

    public override void Run(TowerController controller)
    {
    }

    public override void MouseClick(TowerController controller)
    {
        SelectionData.data.towerSelected = controller.x;
        SceneManager.LoadScene("Tower Description");
    }

    
}
