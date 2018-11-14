using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour {

    // Update is called once per frame
    bool entrou;

    [SerializeField] private string newLevel;


    private void Update()
    {
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(newLevel);
            }
        }


    }
}
