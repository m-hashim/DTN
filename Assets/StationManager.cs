using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class StationManager : MonoBehaviour
{
    public Color[] Colors;
    public static StationManager Instance { get; private set; }

    public GameObject InstantiateStation;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }
    
    // station is positioned here
    public void CreateStation(List<Station> clusters)
    {
        MainManager.Instance.Stations = new List<GameObject>();
        MainManager.Instance.NodeSpeed = 0.5f;
        for (int i=0;i < clusters.Count;i++)
        {
            MainManager.Instance.IsStationSet = true;
            Vector3 Avg = new Vector3();
            foreach(Node n in clusters[i].Nodes)
            {
                Avg += n.InstantiatePosition;
            }
            Avg /= clusters[i].Nodes.Count;
            GameObject Station = Instantiate(InstantiateStation, this.transform);
            MainManager.Instance.Stations.Add(Station);
            Station.transform.position = Avg;

            Station.GetComponent<Renderer>().material.color = Colors[i];

        }
        Invoke("RearrangeNode", 3f);
    }

    public void RearrangeNode()
    {
        print("Rearranging nodes according to neighbour;");
        List<Node> Nodes = NodeManager.Instance.Nodes.ToList<Node>();
        foreach (Node n in Nodes)
        {
            float min = float.MaxValue;

            int chosen = -1;
            //print($"Node name is {n.Name}");
            
            for (int i=0;i<MainManager.Instance.Stations.Count;i++)
            {
                //print($"Distance with station {i} is {Vector3.Distance(n.InstantiatePosition, MainManager.Instance.Stations[i].transform.position)}");
                if (Vector3.Distance(n.InstantiatePosition, MainManager.Instance.Stations[i].transform.position) < min)
                {
                    chosen = i;
                    min = Vector3.Distance(n.InstantiatePosition, MainManager.Instance.Stations[i].transform.position);
                }
                
                
            }
            //print($"Chosen is {chosen}");

            if (chosen != -1)
            {
                n.gameObject.transform.parent = MainManager.Instance.Stations[chosen].transform;
                n.GetComponent<Renderer>().material.color = MainManager.Instance.Stations[chosen].GetComponent<Renderer>().material.color;
            }
            else
            {
              //  print($"Node {n.Name} is nulla ");
            }
        }
        
    }
}
