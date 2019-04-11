using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cluster 
{
    public List<Node> Nodes { get; private set; }
    public int Root { get; private set; } = -1;
    public Cluster() { }

    public Cluster(Node n)
    {
        Nodes = new List<Node>();
        Nodes.Add(n);
        Root = int.Parse(n.Name);
        n.Cluster = this;
    }
    
    public void AddNode(Node n)
    {
        Nodes.Add(n);
        n.Cluster = this;
    }
    

    public override string ToString()
    {
        string result = string.Empty;
        foreach(Node n in Nodes)
        {
            result += $"{n.Name} ,";
        }
        return result;
    }
}