using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleCenas : MonoBehaviour
{

    public void CarregarCena (string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }



    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
