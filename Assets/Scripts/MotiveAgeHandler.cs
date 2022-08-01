using System;
using System.Collections;
using System.Collections.Generic;
using RogoDigital.Lipsync;
using UnityEngine;

public class MotiveAgeHandler : MonoBehaviour
{
    private GameObject bot;
    private AdvancedTestChat atc;

    public LipSyncData kucuk;
    public LipSyncData denk;
    public LipSyncData buyuk;
    
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
    
    void Trigger()
    {
        string valueS = bot.GetComponent<ChatbotCore>().bot.GetGlobalSetting("YourAge");
        int value = Int32.Parse(valueS);
        
        

        if (value < 5)
        {
            StartCoroutine(Wait(kucuk));
        }
        else if (value > 12)
        {
            StartCoroutine(Wait(buyuk));
        }
        else
        {
            StartCoroutine(Wait(denk));
        }
        print("Trigger Name : " + gameObject.name);
    }
    
    IEnumerator Wait(LipSyncData aud) {

        atc.Play(aud); //Play trigger specific audio for LipSync
        yield return new WaitForSeconds (aud.length+.2f);  //Wait for end of trigger
        //atc.OnStartRecordingPressed();
        
        // Next try to send Finished message
        if (bot != null)
            bot.SendMessage ("MotiveFinished");
        else
            Debug.LogWarning ("No Motive to send Finish event to.");
    }
    
}
