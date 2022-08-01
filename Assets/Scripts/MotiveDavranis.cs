using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotiveDavranis : MonoBehaviour
{
    private GameObject bot;
    private AdvancedTestChat atc;

    [Tooltip("Trigger name for character animator")]
    public string triggerName;

    // Start is called before the first frame update
    void Start()
    {
        // Counter to avoid endless loop
        int counter = 0;
        // Count till this value
        int countmax = 10;
        // Current transform
        Transform tmpTrans = this.transform;
        // Gathered ChatbotCore component
        ChatbotCore tmpChatbotCore=null;
        // Loop through transforms till counter reached countmax
        while(counter < countmax) {
            // Is Transform available?
            if(tmpTrans==null)
                // Abort global settings update
                counter=countmax;
            else {
                // Try to grab ChatbotCore component
                tmpChatbotCore = tmpTrans.gameObject.GetComponent<ChatbotCore>();
                // Test wether tmpChatbotCore exists
                if(tmpChatbotCore!=null) {
                    // Set bot gameobject
                    bot=tmpChatbotCore.gameObject;
                    // Abort loop
                    counter=countmax;
                }
                // Get parent transform
                tmpTrans = tmpTrans.parent;
                // Increase counter
                counter++;
            }
        }
        
        atc = AdvancedTestChat._AdvancedTestChat;
    }

    /// <summary>
    /// Coroutine called by Trigger
    /// </summary>
    IEnumerator Wait() 
    {
        atc.Play(triggerName); //Pass the trigger specific animator trigger name

        yield return new WaitForSeconds(3);
        
        if (bot != null)
            bot.SendMessage ("MotiveFinished");
        else
            Debug.LogWarning ("No Motive to send Finish event to.");
        yield break;
    }

    /// <summary>
    /// Trigger this instance.
    /// </summary>
    void Trigger() {
        // Start coroutine
        StartCoroutine("Wait");
        print("Trigger Name : " + gameObject.name);
    }
}
