using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public static NodeManager Instance { private set; get; }
    private int NodesQuantity;
    private int MinimumClusterRequired;
    private int MinimumCollisionStrength;
    private int MaximumNodeInCluster;
    private int[,] HitCount;

    public GameObject InstantiateNode;
    public Node[] Nodes;

    // Start is called before the first frame update
    private int ClustersCreatedCount;
    private List<Cluster> ClustersCreated;
    void Start()
    {
        NodesQuantity = MainManager.Instance.NodeQuantity;
        MinimumClusterRequired = MainManager.Instance.MinimumClusterRequired;
        MinimumCollisionStrength = MainManager.Instance.MinimumCollisionStrength;
        MaximumNodeInCluster = MainManager.Instance.MaximumNodeInCluster;

        Instance = this;
        ClustersCreatedCount = NodesQuantity;
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
    
    void Update()
    {
        foreach(Node n in Nodes)
        {
            n.Movement();
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

    public List<Cluster> GetClusters()
    {
        if (ClustersCreatedCount <= MinimumClusterRequired) return ClustersCreated;

        int i, j;
        List<Cluster> Clusters = new List<Cluster>();
        int[,] HitCount = new int[NodesQuantity, NodesQuantity];

        for (i = 0; i < NodesQuantity; i++)
            for (j = 0; j < NodesQuantity; j++)
                if (this.HitCount[i, j] > MinimumCollisionStrength)
                    HitCount[i, j] = this.HitCount[i, j];

        print(HitCount);

        for (i = 0; i < NodesQuantity; i++)
        {
            var temp = new Cluster(Nodes[i]);
            //print(temp.Root);
            Clusters.Add(temp);
        }

        for (i = 0; i < NodesQuantity; i++)
        {
            while (true)
            {
                int Max = 0;
                int Pos = -1;
                for (j = 0; j < NodesQuantity; j++)
                {
                    if (j != i && HitCount[i, j] > Max)
                    {
                        Max = HitCount[i, j];
                        Pos = j;
                    }

                }
                if (Max == 0) break;
                else HitCount[i, Pos] = 0;

                if (Nodes[i].Cluster.Root != Nodes[Pos].Cluster.Root)
                {
                    Cluster a = Nodes[i].Cluster, b = Nodes[Pos].Cluster;
                    Cluster x, y;
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
                    if (x.Nodes.Count + y.Nodes.Count > MaximumNodeInCluster) continue;
                    foreach (Node n in y.Nodes)
                    {
                        x.AddNode(n);
                    }
                    if (Clusters.Remove(y))
                    {
                        //print($"Cluster {y.Root} is removed");
                    };
                }
            }
            
        }
        
        print($"No of Clusters formed is {Clusters.Count}");
        ClustersCreatedCount = Clusters.Count;
        ClustersCreated = Clusters;

        for (i = 0; i < ClustersCreatedCount; i++)
        {
            foreach(Node n in ClustersCreated[i].Nodes)
            {
                if (i < StationManager.Instance.Colors.Length) 
                    n.gameObject.GetComponent<Renderer>().material.color = StationManager.Instance.Colors[i];
            }
        }
       
        if (ClustersCreatedCount <= MinimumClusterRequired)
            MainManager.Instance.ClusteringDone(Clusters);
        return Clusters;
    }
}
