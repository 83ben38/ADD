using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
     [SerializeReference]
     public ProjectileCode code;
     public Material material;

     public void Awake()
     {
          material = GetComponent<Renderer>().material;
     }

     public void FixedUpdate()
     {
          if (StartButtonController.waveFinished || transform.position.magnitude > 20f)
          {
               Destroy(gameObject);
          }
          
          code?.tick(this);
     }
}
