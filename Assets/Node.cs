using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Node : MonoBehaviour
{
    public Vector3 DestinationPosition;
    public string Name { get; set; }
    public Station Cluster;

    private float InstantiateRange ;
    public Vector3 InstantiatePosition;

    private float Range = 15f;

    private Rigidbody Rigidbody;
    
    public void SetInitialPosition()
    {
        Range = MainManager.Instance.NodeRange;
        InstantiateRange = MainManager.Instance.PlayGround.transform.localScale.x * 5 - Range;
        Rigidbody = this.gameObject.GetComponent<Rigidbody>();
        InstantiatePosition = new Vector3(Random.Range(-InstantiateRange, InstantiateRange), Rigidbody.position.y, Random.Range(-InstantiateRange, InstantiateRange));
        //print(InstantiatePosition);
        Rigidbody.position = InstantiatePosition;
        NewDestination();

    }

    void NewDestination()
    {
        
        DestinationPosition = InstantiatePosition + new Vector3(Random.Range(-Range, Range), 0, Random.Range(-Range, Range));
    }

    public void Movement()
    {
        if (Rigidbody.position != DestinationPosition)
        {
            Rigidbody.position = Vector3.MoveTowards(Rigidbody.position, DestinationPosition, MainManager.Instance.NodeSpeed);
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
            NodeManager.Instance.HitRegister(int.Parse(Name), int.Parse(other.GetComponent<Node>().Name));        
        }
    }
    
    
}
