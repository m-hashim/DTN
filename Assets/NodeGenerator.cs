using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGenerator : MonoBehaviour
{
    public static NodeGenerator Instance { private set; get; }
    public int NodesQuantity;
    private int[,] HitCount;
    public GameObject InstantiateNode;
    public Node[] Nodes;
    
    // Start is called before the first frame update

    void Start()
    {
        Instance = this;
        Nodes = new Node[NodesQuantity];
        HitCount = new int[NodesQuantity, NodesQuantity];
        for(int i = 0; i < NodesQuantity; i++)
        {

            var node = Instantiate(InstantiateNode, this.transform);
            Nodes[i] = node.GetComponent<Node>();
            node.GetComponent<Node>().Name = i.ToString();
            node.GetComponent<Node>().SetInitialPosition();

        }
        InvokeRepeating("GetClusters", 1f, 3f);
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


    public List<ClusterOfNode> GetClusters()
    {
        int i, j;
        List<ClusterOfNode> Clusters = new List<ClusterOfNode>();
        int[,] HitCount = new int[NodesQuantity, NodesQuantity];

        for (i = 0; i < NodesQuantity; i++)
            for (j = 0; j < NodesQuantity; j++)
                if (this.HitCount[i, j] > 5)
                    HitCount[i, j] = this.HitCount[i, j];

        print(HitCount);

        for (i = 0; i < NodesQuantity; i++)
        {
            var temp = new ClusterOfNode(Nodes[i]);
            //print(temp.Root);
            Clusters.Add(temp);
        }

        for (i = 0; i < NodesQuantity; i++)
        {
            for (j = i + 1; j < NodesQuantity; j++)
            {
                if (HitCount[i, j] != 0 && Nodes[i].Cluster.Root != Nodes[j].Cluster.Root)
                {
                    //Merge Cluster
                    ClusterOfNode a = Nodes[i].Cluster, b = Nodes[j].Cluster;
                    ClusterOfNode x, y;
                    if (a.Root < b.Root)
                    {
                        x = a;
                        y = b;
                    }
                    else
                    {
                        x = b;
                        y = a;
                    }

                    foreach (Node n in y.Nodes)
                    {
                        x.AddNode(n);
                    }
                    if (Clusters.Remove(y)) {
                        //print($"Cluster {y.Root} is removed");
                    };
                }
            }
        }
        
        print($"No of Clusters formed is {Clusters.Count}");
        return Clusters;
    }
}
