using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NetworkButtons : MonoBehaviour
{
    public Button jButton;
    public TMP_InputField ipInput;
    public GameObject playScreen;
    public GameObject mainMenu;

    private void Start()
    {
        jButton.interactable = false;
    }

    public void ValidateIPInputField(string ip)
    {
        if (ip.Length > 8)
        {
            jButton.interactable = true;
        }
        else
        {
            jButton.interactable = false;
        }
    }


    public void StartHost()
    {
        IPManager.Singleton.nMan.StartHost();
        RemButtons();
    }

    public void StartClient()
    {
        IPManager.Singleton.SetIP(ipInput.GetComponent<TMP_InputField>().text);
        IPManager.Singleton.nMan.StartClient();
        RemButtons();
    }

    //private Uri 

    private void RemButtons()
    {
        playScreen.SetActive(false);
    }

    // private void Awake() {
    //     GetComponent<UnityTransport>().SetDebugSimulatorParameters(
    //         packetDelay: 120,
    //         packetJitter: 5,
    //         dropRate: 3);
    // }
}