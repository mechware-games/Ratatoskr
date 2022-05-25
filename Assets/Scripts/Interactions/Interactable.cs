using UnityEngine;

[RequireComponent(typeof(SphereCollider))] 

public class Interactable : MonoBehaviour
{
    public float radius;
    public SphereCollider range;
    public bool inRange;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Awake()
    {
        range = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        range.radius = radius;
    }

    private void Update()
    {
        range.radius = radius;
        GetInput();
    }

    public void GetInput()
    {
        if (Input.GetButtonDown("Interact") && inRange)
        {
            Action();
        }
    }

    public virtual void Action()
    {
        Debug.Log("No Action has been assigned");
    }

     public virtual void StopAction()
    {
        Debug.Log("STOPPED ACTION:");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}