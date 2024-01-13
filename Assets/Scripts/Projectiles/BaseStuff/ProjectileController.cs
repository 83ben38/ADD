using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
     public ProjectileCode code;
     public Material material;

     public void Awake()
     {
          material = GetComponent<Renderer>().material;
     }

     public void FixedUpdate()
     {
          if (!StartButtonController.waveGoing)
          {
               Destroy(gameObject);
          }

          code.tick(this);
     }
}
