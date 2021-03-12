using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FollowBehaviour : MonoBehaviour
{
    private NavMeshAgent navMesh;
    public Transform followTarget;

    public enum TargetColor
    {
        Red,
        Blue
    }
    private TargetColor followColor;
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(followTarget != null)
        {
            navMesh.SetDestination(followTarget.position);
        }

    }

    public void FollowNewTarget(TargetColor newColor)
    {
        followColor = newColor;
        var renderer = GetComponent<Renderer>();

        switch (followColor)
        {
            case TargetColor.Red:
            {
                followTarget = GameObject.Find("RedPlayer").GetComponent<Transform>();

                renderer.material.SetColor("_Color", Color.red);

                transform.gameObject.tag = "Red";
            }
                break;
            case TargetColor.Blue:
            {
                followTarget = GameObject.Find("BluePlayer").GetComponent<Transform>();

                renderer.material.SetColor("_Color", Color.blue);

                transform.gameObject.tag = "Blue";
            }
            break;
            default:
                break;
        }
    }

    public bool IsWithinStoppingDistance()
    {
        if(followTarget == null) { return false; }

        return Mathf.Sqrt(Mathf.Pow(followTarget.position.x - transform.position.x, 2) + Mathf.Pow(followTarget.position.y - transform.position.y, 2)) < navMesh.stoppingDistance;

    }
}
