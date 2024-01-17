using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class TowerRangeController : MonoBehaviour
{

    
    public TowerCode tower;
    public bool isVisibal = false ;
    
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        gameObject.transform.localPosition = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isVisibal && tower != null)
        {
            gameObject.transform.localPosition = new Vector3(0, 0.5f, 0);

            gameObject.transform.localScale = new Vector3(tower.getRange() + 1.5f, (tower.getRange() + 1.5f) / 2 , tower.getRange() + 1.5f);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            gameObject.transform.localPosition = new Vector3(0,0,0);
        }
        
    }
}
