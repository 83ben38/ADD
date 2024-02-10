using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Shape : MonoBehaviour
{ 
    [SerializeField]
    public ShapeScriptableObject scriptableObject;

    
    // Start is called before the first frame update
    void Awake()
    {
        transform.localPosition = scriptableObject.meshPosition;
        transform.rotation = scriptableObject.meshRotation;
        transform.localScale = scriptableObject.meshScale;
        GetComponent<MeshCollider>().sharedMesh = scriptableObject.mesh;
        GetComponent<MeshFilter>().sharedMesh = scriptableObject.mesh;
    }

    public void Reset()
    {
        transform.localPosition = scriptableObject.meshPosition;
        transform.rotation = scriptableObject.meshRotation;
        transform.localScale = scriptableObject.meshScale;
        GetComponent<MeshCollider>().sharedMesh = scriptableObject.mesh;
        GetComponent<MeshFilter>().sharedMesh = scriptableObject.mesh;
    }

    public Vector3 getDotPos(int i)
    {
        return scriptableObject.dotPositions[i];
    }

    // Update is called once per frame
    public Vector2 getPosition(Vector2 coords)
    {
        return new Vector2(coords.x * scriptableObject.xXFactor + (coords.y%scriptableObject.xYFactorMod) * scriptableObject.xYFactor + scriptableObject.xyXFactor * ((coords.x+coords.y)%scriptableObject.xyXFactorMod), (coords.x%scriptableObject.yXFactorMod) * scriptableObject.yXFactor + coords.y * scriptableObject.yYFactor+ scriptableObject.xyYFactor * ((coords.x+coords.y)%scriptableObject.xyYFactorMod));
    }

    public Quaternion getRotation(Vector2 coords)
    {
        return Quaternion.Euler(new Vector3(coords.x*scriptableObject.xRotationFactor+coords.y*scriptableObject.yRotationFactor,0,0)+scriptableObject.meshRotation.eulerAngles);
    }
}



