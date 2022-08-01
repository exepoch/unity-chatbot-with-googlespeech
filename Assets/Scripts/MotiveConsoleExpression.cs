using System;
using System.Collections;
using System.Collections.Generic;
using AIMLbot.AIMLTagHandlers;
using RogoDigital.Lipsync;
using UnityEngine;

[AddComponentMenu("Chatbot/Helperfunctions/MotiveCustomExpression")]
public class MotiveConsoleExpression : MonoBehaviour
{
    public LipSyncData aud;
    public string anim;
    private GameObject bot;
    private AdvancedTestChat atc;

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
    IEnumerator Wait() {

        if (aud != null) //Check audio exist
        {
            if(!String.IsNullOrEmpty(anim)) atc.Play(anim);
            atc.Play(aud); //Play trigger specific audio for LipSync
            yield return new WaitForSeconds (aud.length+.2f); 
            //atc.OnStartRecordingPressed();//Wait for end of trigger
            
            // Next try to send Finished message
            if (bot != null)
                bot.SendMessage ("MotiveFinished");
            else
                Debug.LogWarning ("No Motive to send Finish event to.");
        }
        else
        {
            Debug.Log("No audio file for this Trigger! : " + gameObject.name); //Warning if trigger hasn't audio
        }
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
