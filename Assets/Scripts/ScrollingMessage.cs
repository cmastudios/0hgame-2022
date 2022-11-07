using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingMessage : MonoBehaviour
{
    public struct Message
    {
        public string text;
        public float time;
    }

    private Text text;
    private List<Message> messages;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        messages = new List<Message>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMessage(Message message)
    {
        messages.Add(message);
    }
}
