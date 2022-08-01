using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using AIMLbot;
using TextSpeech;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class TestChat : MonoBehaviour
{
    #region AIML REG
    private string outputText =""; 
    // Program # Variables
    private AIMLbot.Bot bot;
    private AIMLbot.User user;
    private AIMLbot.Request request;
    private AIMLbot.Result result;

    #endregion

    #region TTS REG

    private TextToSpeech tts;
    private SpeechToText stt;

    #endregion

    #region Canvas

    public Text input;
    public Text output;

    #endregion
    

    // Start is called before the first frame update
    void Start()
    {
        if(!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            Permission.RequestUserPermission(Permission.Microphone);
        
        bot = new Bot();
        user = new AIMLbot.User("User",bot);
        request = new AIMLbot.Request("",user,bot);
        result = new AIMLbot.Result(user,bot,request);

        tts = TextToSpeech.instance;
        stt = SpeechToText.instance;
        stt.Setting("tr_TR");
        tts.Setting("tr_TR", 1,1);
        stt.onResultCallback = SetResult;

        bot.loadSettings("MustChat/Program #/config/Settings");
        bot.loadAIMLFromFiles();
    }
    
    private void SetResult(string res)
    {
        input.text = res;
        string non_dialectic = CleanDialectics(res);
        
        request.rawInput = non_dialectic;
        request.StartedOn = DateTime.Now;
        result = bot.Chat(request);
        outputText = result.Output;
        
        output.text = outputText;
        Debug.Log(outputText);
        
        tts.StartSpeak(outputText);
    }

    public void StartRec()
    {
        SpeechToText.instance.StartRecording();
    }

    public void StopRec()
    {
        SpeechToText.instance.StopRecording();
    }

    /*void OnGUI () 
    {
        // Enable Word warp
        GUI.skin.label.wordWrap = true;
        // Make a background box
        GUI.Box(new Rect(10,10,300,150), "Chat with a Chatbot");
        // Make output label
        GUI.Label (new Rect (20, 30, 280, 40), Output_Text);
        // Make a text field that modifies Input_Text.
        Input_Text = GUI.TextField (new Rect (20, 100, 280, 20), Input_Text, 100);
        // If send button or enter pressed
        if(((Event.current.keyCode == KeyCode.Return)||GUI.Button(new Rect(250,130,50,20),"Send")) && (Input_Text != "")) {
            // Prepare Variables
            // You don't need to care, wether Only Jurassics or only Program #'s Variables
            // are changed. This is managed immediate intern every time you change a global
            // variable in Program # or Jurassic.
            // bot.jscript_engine.SetGlobalValue("abc",15);

            request.rawInput = Input_Text;
            request.StartedOn = DateTime.Now;
            result = bot.Chat(request);
            Output_Text = result.Output;
            Input_Text = "";
		
            // Gather Variables
            // bot.jscript_engine.GetGlobalValue<string>("abc");
        }
    }*/
    
    private string CleanDialectics(string bef)
    {
        string yazi = bef;
        
        yazi = yazi.Replace("ü", "u");
        yazi = yazi.Replace("ı", "i");
        yazi = yazi.Replace("ö", "o");
        yazi = yazi.Replace("ü", "u");
        yazi = yazi.Replace("ş", "s");
        yazi = yazi.Replace("ğ", "g");
        yazi = yazi.Replace("ç", "c");
        yazi = yazi.Replace("Ü", "U");
        yazi = yazi.Replace("İ", "I");
        yazi = yazi.Replace("Ö", "O");
        yazi = yazi.Replace("Ü", "U");
        yazi = yazi.Replace("Ş", "S");
        yazi = yazi.Replace("Ğ", "G");
        yazi = yazi.Replace("Ç", "C");

        return yazi;
    }
    
}