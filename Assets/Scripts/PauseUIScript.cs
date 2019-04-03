using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUIScript : MonoBehaviour {


    public Button pauseCloseButton;

    // Use this for initialization
    void Start () {

    pauseCloseButton.onClick.AddListener(closePauseMenu);

}

// Update is called once per frame
void Update () {
		
	}

    public void closePauseMenu()
    {
        SceneManager.LoadScene(0);
    }

}
