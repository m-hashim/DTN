using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGenerator : MonoBehaviour
{
    public static NodeGenerator Instance { private set; get; }
    public int NodesQuantity;
    private int[,] HitCount;
    public GameObject Node;
    
    // Start is called before the first frame update

    void Start()
    {
        Instance = this;
        HitCount = new int[NodesQuantity, NodesQuantity];
        for(int i = 0; i < NodesQuantity; i++)
        {

            var node = Instantiate(Node, this.transform);
            node.GetComponent<Node>().Name = i.ToString();
            node.GetComponent<Node>().SetInitialPosition();

        }
    }
    
    public void HitRegister(int source, int destination)
    {
        HitCount[source, destination]++;
    }

    public int MaximumLikelyhoodNode(int source)
    {
        int max = -1; int pos = -1;
        for (int i = 0; i < NodesQuantity; i++)
        {
            if (HitCount[source,i] > max)
            {
                max = HitCount[source,i];
                pos = i;
            }
        }
        return pos;
    }



}
