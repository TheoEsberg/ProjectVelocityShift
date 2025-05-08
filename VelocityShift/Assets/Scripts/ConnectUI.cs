using System;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class ConnectUI : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private GameObject menuCamera;
    [SerializeField] private TMP_InputField ipInputField;

    private void Start()
    {
        hostButton.onClick.AddListener(HostButtonOnClick);
        clientButton.onClick.AddListener(ClientButtonOnClick);
    }

    private void Update()
    {
        
    }

    private async void HostButtonOnClick()
    {
        CustomSceneManager.instance.LoadScene("Game");
        await Task.Delay(250);


        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.ConnectionData.Address = "0.0.0.0";
        transport.ConnectionData.Port = 7777;

        NetworkManager.Singleton.StartHost();
    }

    private async void ClientButtonOnClick()
    {
        CustomSceneManager.instance.LoadScene("Game");
        await Task.Delay(250);

        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();

        string ipAddress = ipInputField.text;
        if (string.IsNullOrEmpty(ipAddress))
        {
            Debug.LogWarning("IP address is empty! Please enter a valid IP.");
            return;
        }

        transport.ConnectionData.Address = ipAddress;
        transport.ConnectionData.Port = 7777;

        NetworkManager.Singleton.StartClient();
    }
}
