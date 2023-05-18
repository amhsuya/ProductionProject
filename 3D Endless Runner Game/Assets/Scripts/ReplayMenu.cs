using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReplayMenu : MonoBehaviour
{
    public GameObject Replaypanel;
    // Start is called before the first frame update
    void Start()
    {
        Replaypanel.SetActive(false);
    }

    
     public void ShowReplayMenu()
    {
        Replaypanel.SetActive(true);
    }
    public void NewAction()
    {

    }
}
