using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public bool menuOpen = false;
    public GameObject pMenu;
    public GameObject cMenu1;
    public GameObject cMenu2;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuOpen)
            {
                menuOpen = true;
                pMenu.SetActive(true);
            }
            else
            {
                menuOpen = false;
                pMenu.SetActive(false);
                cMenu1.SetActive(false);
                cMenu2.SetActive(false);
            }
        }
    }

    public void Disconnect()
    {
        NetworkManager.Singleton.Shutdown();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void Quit()
    {
        NetworkManager.Singleton.Shutdown();
        Application.Quit();
    }
}
