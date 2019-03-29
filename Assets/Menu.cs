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
        
        print($"Button click {NodeGenerator.Instance.GetComponentsInChildren<Node>().Length}");
        string str = "";
        for(int i = 0; i < NodeGenerator.Instance.GetComponentsInChildren<Node>().Length; i++){
            str += $"{i} : {NodeGenerator.Instance.MaximumLikelyhoodNode(int.Parse(NodeGenerator.Instance.transform.GetChild(i).gameObject.GetComponent<Node>().Name))}\n";
        }
        print(str);
    }
}
