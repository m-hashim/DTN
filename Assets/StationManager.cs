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
    
    public void CreateStation(List<ClusterOfNode> clusters)
    {
        MainManager.Instance.NodeSpeed = 0.5f;
        for (int i=0;i < clusters.Count;i++)
        {
            Vector3 Avg = new Vector3();
            foreach(Node n in clusters[i].Nodes)
            {
                Avg += n.InstantiatePosition;
            }
            Avg /= clusters[i].Nodes.Count;
            var Station = Instantiate(InstantiateStation, this.transform);
            Station.transform.position = Avg;

            Station.GetComponent<Renderer>().material.color = Colors[i];

        }
    }
}
