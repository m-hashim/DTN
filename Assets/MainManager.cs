using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }

    public float NodeSpeed = 0.5f;
    public float NodeRange = 15;

    public int NodeQuantity = 50;
    public int MinimumCollisionStrength = 5;
    public int MinimumClusterRequired = 5;
    public int MaximumNodeInCluster = 15;

    public bool IsStationSet { get; set; }

    public GameObject PlayGround;

    public List<Cluster> Clusters;
    void Start()
    {
        Clusters = new List<Cluster>();
        Instance = this;
    }

    public void ClusteringDone(List<Cluster> Clusters)
    {
        print("Stations are creating");
        this.Clusters = Clusters; 
        StationManager.Instance.CreateStation(Clusters);
      //  StopAndPlayBehaviour();
    }
    public void StopAndPlayBehaviour()
    {
        InvokeRepeating("PauseUnpause", 0f, 5f);
    }

    public void PauseUnpause()
    {
        if (NodeSpeed == 0f)
            NodeSpeed = 0.5f;
        else NodeSpeed = 0f;
    }

}
