using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinManager : MonoBehaviour
{
    [SerializeField] public GameObject standardRat;
    [SerializeField] public GameObject blueRat;
    [SerializeField] public GameObject goldenRat;

    [SerializeField] public static GameObject currentRat;
    
    public GameObject spawnPoint;

    public bool isPinkChosen = true;
    public bool isBlueChosen;
    public bool isGoldChosen;

    private void Awake()
    {
        spawnPoint = GameObject.Find("SpawnPoint");

        if (currentRat == null)
        {
            currentRat = standardRat;
        }

        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0))
        {
            Instantiate(currentRat, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
    }

    private void Start()
    {
    }

    public GameObject CurrentRat()
    {
        if (isBlueChosen) return blueRat;
        if (isPinkChosen) return standardRat;
        if (isGoldChosen) return goldenRat;
        else return standardRat;
    }

    public void SetRat(int rat)
    {
        if (rat == 1) { currentRat = standardRat; }
        else if (rat == 2) { currentRat = blueRat; }
        else if (rat == 3) { currentRat = goldenRat; }
        else { currentRat = standardRat; }

    }

    public GameObject GetRat()
    {
        return currentRat;
    }
}