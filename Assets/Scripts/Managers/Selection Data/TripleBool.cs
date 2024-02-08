using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class TripleBool
{
   [SerializeField] private bool b1;
   [SerializeField] private bool b2;
   [SerializeField] private bool b3;

   public TripleBool(bool b1, bool b2, bool b3)
   {
      this.b1 = b1;
      this.b2 = b2;
      this.b3 = b3;
      
   }

   public bool this[int num]
   {
      get => get(num);
      set => set(num, value);
   }

   private bool get(int num)
   {
      if (num == 0)
      {
         return b1;
      }
      if (num == 1)
      {
         return b2;
      }

      return b3;
   }

   private void set(int num,bool set)
   {
      if (num == 0)
      {
         b1 = set;
         return;
      }
      if (num == 1)
      {
         b2 = set;
         return;
      }

      b3 = set;
   }

}
