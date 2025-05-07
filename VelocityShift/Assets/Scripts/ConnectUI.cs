using System;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        Debug.Log("===================================");
        Debug.Log("Address: " + NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address);
        Debug.Log("Port: " + NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Port);
        Debug.Log("ServerListenAddress: " + NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.ServerListenAddress);
        Debug.Log("===================================");
    }

    private void HostButtonOnClick()
    {
        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.ConnectionData.Address = "0.0.0.0";
        transport.ConnectionData.Port = 7777;

        NetworkManager.Singleton.StartHost();
        menuCamera.SetActive(false);
    }

    private void ClientButtonOnClick()
    {
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
        menuCamera.SetActive(false);
    }
}
