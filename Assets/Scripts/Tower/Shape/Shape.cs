using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Shape : MonoBehaviour
{ 
    [SerializeField]
    private ShapeScriptableObject scriptableObject;

    public string code;
    // Start is called before the first frame update
    void Awake()
    {
        code = scriptableObject.checkName;
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
        return new Vector2(coords.x * scriptableObject.xXFactor + (coords.y%scriptableObject.xYFactorMod) * scriptableObject.xYFactor, (coords.x%scriptableObject.yXFactorMod) * scriptableObject.yXFactor + coords.y * scriptableObject.yYFactor);
    }
}



