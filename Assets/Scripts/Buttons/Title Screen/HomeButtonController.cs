using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButtonController : Selectable
{
        private Material _material;
    
        public override void MouseEnter()
        {
            _material.color = ColorManager.manager.tileHighlighted;
        }
    
        public override void MouseExit()
        {
            _material.color = ColorManager.manager.tile;
        }

        public override void MouseClick()
        {
            SceneManager.LoadScene("Title Screen");
        }

        private void Start()
        {
            _material = GetComponent<Renderer>().material;
        }
}
