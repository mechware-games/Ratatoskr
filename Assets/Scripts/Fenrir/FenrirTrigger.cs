using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenrirTrigger : MonoBehaviour
{
    [SerializeField]
    [Header("Does ")]
    private bool _toggle = false;

    [SerializeField]
    private bool _set = true;

    private GameObject _fenrir;

    // Start is called before the first frame update
    void Start()
    {
        _fenrir = GameObject.Find("Fenrir");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
            bool active = _fenrir.GetComponent<FenrirScript>().GetActive();
            if (_toggle)
			{  
                _fenrir.GetComponent<FenrirScript>().SetActive(!active);
            }
            else if (_set != active) // If Fenrir's active state is different to the one this interactable triggers, set it accordingly
			{
                _fenrir.GetComponent<FenrirScript>().SetActive(active);

            }

		}
	}
}