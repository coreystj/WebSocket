using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Net;

public class WebSocket : MonoBehaviour
{
    private static WebSocket _instance;
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void Connect(string url);
        [DllImport("__Internal")]
        private static extern void Send(string message);
#else
    private static WebSocketSharp.WebSocket _webSocket;
    public static void Connect(string url)
    {
        if (_webSocket == null)
        {
            _webSocket = new WebSocketSharp.WebSocket("ws://" + url + "/Echo");
            
            // Set the WebSocket events.
            _webSocket.OnMessage += (sender, e) =>
            {
                _instance.OnMessageReceived(e.Data);
            };

            _webSocket.OnError += (sender, e) =>
            {
                _instance.OnError(e.Message);
            };

            _webSocket.OnClose += (sender, e) =>
            {
                _instance.OnClosed();
            };

            _webSocket.Connect();
        }
    }
    public static void Send(string message)
    {
        if (_webSocket != null)
        {
            _webSocket.Send(message);
        }
    }
#endif

    public void OnMessageReceived(string message)
    {
        Debug.Log("Message: " + message.Length);
    }

    public void OnError(string error)
    {
        Debug.Log("Error: " + error);
    }

    public void OnClosed()
    {
        Debug.Log("Closed");
    }

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        Connect("192.168.0.182:4649");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Send(new string('*', 1000000));
        }
    }
    
}
