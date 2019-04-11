using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClick()
    {
        
        print($"Button click {NodeManager.Instance.GetComponentsInChildren<Node>().Length}");
        string str = "";
        for(int i = 0; i < NodeManager.Instance.GetComponentsInChildren<Node>().Length; i++){
            str += $"{i} : {NodeManager.Instance.MaximumLikelyhoodNode(int.Parse(NodeManager.Instance.transform.GetChild(i).gameObject.GetComponent<Node>().Name))}\n";
        }
        NodeManager.Instance.GetClusters();
        print(str);
    }

    
}
