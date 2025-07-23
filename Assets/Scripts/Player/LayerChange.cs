using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LayerChange : MonoBehaviour
{
    public int maxLayer = 2;
    
    int layer = 1;

    public GameObject[] layerObjs;

    
    private void LayerSwitch()
    {
        for (int i = 0; i < layerObjs.Length; i++)
        {
            if (i+1 == layer)
                layerObjs[i].SetActive(true);
            else
                layerObjs[i].SetActive(false);
        }
    }

    private void Start()
    {
        LayerSwitch();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && layer != 1)
        {
            layer--;
            LayerSwitch();


        }
                      

        if (Input.GetKeyDown(KeyCode.RightArrow) && layer != maxLayer)
        {
            layer++;
            LayerSwitch();
        }
        
    }


}
