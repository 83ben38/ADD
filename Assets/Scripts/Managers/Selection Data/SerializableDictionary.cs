using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TK,TB> : Dictionary<TK, TB>, ISerializationCallbackReceiver
{
   [SerializeField]
   private TK[] keys;
   [SerializeField]
   private TB[] values;

   public void OnBeforeSerialize()
   {
      keys = new TK[Count];
      values = new TB[Count];
      int i = 0;
      foreach (var key in Keys)
      {
         keys[i] = key;
         values[i] = this[key];
         i++;
      }
   }

   public void OnAfterDeserialize()
   {
      for (int i = 0; i < keys.Length; i++)
      {
         this[keys[i]] = values[i];
      }
   }
}
