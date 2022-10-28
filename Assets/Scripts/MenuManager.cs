using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject CMenu;
    public GameObject EMenu;

    public void OpenCredits() {
        CMenu.SetActive(true);
    }
    public void CloseCredits() {
        CMenu.SetActive(false);
    }
    public void OpenExit() {
        EMenu.SetActive(true);
    }
    public void CloseExit() {
        EMenu.SetActive(false);
    }
    public void Die() {
        Application.Quit();
    }
}
