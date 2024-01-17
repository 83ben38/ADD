using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class TowerRangeControler : MonoBehaviour
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

            gameObject.transform.localScale = new Vector3(tower.range + 1, (tower.range + 1) / 2 , tower.range + 1);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            gameObject.transform.localPosition = new Vector3(0,0,0);
        }
        
    }
}
