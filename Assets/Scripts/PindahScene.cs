using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PindahScene : MonoBehaviour {
    public bool isEscapeToExit;
    
    void Start() {
    }
    
    void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            if (isEscapeToExit) {
                Application.Quit();
            }
            else {
                BukaSceneIni("MainMenu");
            }
        }
    }
    public void BukaSceneIni(string LoadSceneTo) {
        SceneManager.LoadScene(LoadSceneTo);
    }

    public void ShowSceneIni(GameObject Panel) {
        if (Panel.activeSelf) {
            Panel.SetActive(false);
        }
        else {
            Panel.SetActive(true);
        }
        
    }
}