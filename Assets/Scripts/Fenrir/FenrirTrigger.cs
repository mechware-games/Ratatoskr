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
            bool fenrirActive = _fenrir.GetComponent<FenrirScript>().GetActive();
            if (_toggle)
			{  
               if (fenrirActive)
				{
                    _fenrir.GetComponent<FenrirScript>().Despawn(false);
				}
				else
				{
                    _fenrir.GetComponent<FenrirScript>().Spawn();
                }
            }
            else if (_set != fenrirActive) // If Fenrir's active state is different to the one this interactable triggers, set it accordingly
			{
                _fenrir.GetComponent<FenrirScript>().SetActive(fenrirActive);

            }

		}
	}
}
