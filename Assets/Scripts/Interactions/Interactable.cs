using UnityEngine;

[RequireComponent(typeof(SphereCollider))] 

public class Interactable : MonoBehaviour
{
    public float radius;
    public SphereCollider range;
    public bool inRange;

    public GameObject interactionPrompt;
    public GameObject interactionCanvas;

    [SerializeField] public Pause pause;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Awake()
    {
        range = GetComponent<SphereCollider>();
        pause = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<Pause>();
    }

    private void Start()
    {
        range.radius = radius;
    }

    private void Update()
    {
        if (pause.paused == false)
        {
            range.radius = radius;
            GetInput();
            if (inRange)
            {
                interactionCanvas.SetActive(true);
                interactionPrompt.SetActive(true);
            }
            else
            {
                interactionCanvas.SetActive(false);
                interactionPrompt.SetActive(false);
            }
        }
        else
        {
            interactionCanvas.SetActive(false);
            interactionPrompt.SetActive(false);
        }
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