using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject __failPanel;
    
    public void Retry()
    {
        SceneManager.LoadScene("ImpactDuel");
        __failPanel.SetActive(false);
    }
}
