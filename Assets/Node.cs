using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Node : MonoBehaviour
{
    public Vector3 DestinationPosition;
    public string Name { get; set; }
    int[] HitCount;

    private float InstantiateRange = 45f;
    private Vector3 InstantiatePosition; 
    public float MovingRange = 15f;
    public float MovingSpeed = 0.5f;

    private Rigidbody Rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        HitCount = new int[NodeGenerator.Instance.NodesQuantity];
        Rigidbody = this.gameObject.GetComponent<Rigidbody>();
        NewDestination();
    }

    public void SetInitialPosition()
    {
        Rigidbody = this.gameObject.GetComponent<Rigidbody>();
        InstantiatePosition = new Vector3(Random.Range(-InstantiateRange, InstantiateRange), Rigidbody.position.y, Random.Range(-InstantiateRange, InstantiateRange));

        Rigidbody.position = InstantiatePosition;

    }

    void NewDestination()
    {
        
        DestinationPosition = InstantiatePosition + new Vector3(Random.Range(-MovingRange, MovingRange), 0, Random.Range(-MovingRange, MovingRange));
        //DestinationPosition =  Vector3.ClampMagnitude(DestinationPosition, InstantiateRange);
    }

    // Update is called once per frame
    void Update()
    {
        if (Rigidbody.position != DestinationPosition)
        {
            Rigidbody.position = Vector3.MoveTowards(Rigidbody.position, DestinationPosition, MovingSpeed);
        }
        else
        {
            NewDestination();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Node>() != null)
        {
            NodeGenerator.Instance.HitRegister(int.Parse(Name), int.Parse(other.GetComponent<Node>().Name));        
     //       print($"{Name} is interacted with {int.Parse(other.GetComponent<Node>().Name)} : {HitCount[int.Parse(other.GetComponent<Node>().Name)]} times");
        }
    }
    
    
}
