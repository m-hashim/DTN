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

    public GameObject PlayGround;
    void Start()
    {
        Instance = this;
    }

   
}
