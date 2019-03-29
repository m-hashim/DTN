using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGenerator : MonoBehaviour
{
    public static NodeGenerator Instance { private set; get; }
    public int NodesQuantity;
    public GameObject Node;
    
    // Start is called before the first frame update

    void Start()
    {
        Instance = this;
        for(int i = 0; i < NodesQuantity; i++)
        {

            var node = Instantiate(Node, this.transform);
            node.GetComponent<Node>().Name = i.ToString();
            node.GetComponent<Node>().SetInitialPosition();

        }
    }
    
    
}
