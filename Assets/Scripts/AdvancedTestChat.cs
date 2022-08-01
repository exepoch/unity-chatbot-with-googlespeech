using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using RogoDigital.Lipsync;
using TextSpeech;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class AdvancedTestChat : MonoBehaviour
{
    [SerializeField] private LipSync lipper;
    [SerializeField] private Animator animator;
    
    public static AdvancedTestChat _AdvancedTestChat;
    
    //Instance Field
    #region TTS REG

    private TextToSpeech tts;
    private SpeechToText stt;

    #endregion
    #region BOT REG

    private Chatbot.Core bot = new Chatbot.Core();

    #endregion
    #region Canvas

    public Text input;
    public Text output;

    #endregion

    private void Awake() => _AdvancedTestChat = this;
    
   private void Start()
   {
       bot = this.gameObject.GetComponent<ChatbotCore>().bot;
       if(!Permission.HasUserAuthorizedPermission(Permission.Microphone))
           Permission.RequestUserPermission(Permission.Microphone);

       tts = TextToSpeech.instance;
       stt = SpeechToText.instance;
       stt.Setting("tr_TR");
       tts.Setting("tr_TR", 1,1);
       stt.onResultCallback = SetResult;
       
       Invoke("Greet",1);
   }

   private void Greet()
   {
       bot.SetGlobalSetting("greet", "1");
   }

   public void Play(LipSyncData lipData) //For LipsyncPlay
   {
       lipper.Play(lipData);
   }

   public void Play(string triggerName) //For AnimationPlay
   {
       animator.SetTrigger(triggerName);
   }
   
   //Speech Talk
   private void SetResult(string res)
   {
       input.text = res;
       string non_dialectic = CleanDialectics(res);

       Output_Text = bot.Chat(non_dialectic);
       output.text = Output_Text;

       //input.text = "";

       //TextToSpeech istenirse
       /*
       request.rawInput = non_dialectic;
       request.StartedOn = DateTime.Now;
       result = bot.Chat(request);
       outputText = result.Output;
       
       output.text = outputText;
       Debug.Log(outputText);
       
       tts.StartSpeak(outputText);
       */
   }

   public void SetResultWithKeyboard(TMP_InputField res)
   {
       if (String.IsNullOrEmpty(res.text) || String.IsNullOrWhiteSpace(res.text)) return; //Olası boş girdiler için
       input.text = res.text;
       Input_Text = res.text;
        
       string non_dialectic = CleanDialectics(res.text);
       Output_Text = bot.Chat(non_dialectic);
       output.text = Output_Text;

       input.text = "";
       //Input_Text = "";
       res.text = "";
   }
   
   public void StartRec()
   {
       SpeechToText.instance.StartRecording();
   }

   public void StopRec()
   {
       SpeechToText.instance.StopRecording();
   }
   
   private string CleanDialectics(string bef)
   {
       string yazi = bef;
        
       //Turkce karakterleri kaldırmak için
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

       //Olusan ingilizce uyumlu cümleyi AIML kullanımı için uppercase çevirmek için
       return yazi.ToUpper(new CultureInfo("en-US", false));
   }

   #region GUI
   
    private string Input_Text="";
    private string Output_Text="";
    //public float _emoticonScale=1.0f;
    private float speed = 0.12f;
    private Vector2 drawPosition;
    List<Texture2D> gifFrames = new List<Texture2D>();
    /*void OnGUI() {
        // Draw Emoticon in front of other elements
        //GUI.depth = 0;
        // Render if reference to bot exists
        if (bot != null) {
            // Centered output style, color and wordwarp
            GUIStyle outputstyle = new GUIStyle ();
            outputstyle.alignment = TextAnchor.UpperCenter;
            outputstyle.normal.textColor = UnityEngine.Color.white;
            outputstyle.wordWrap = true;
            // Enable Word warp
            GUI.skin.label.wordWrap = true;
            // Make a background box
            GUI.Box (new Rect (10, 10, 600, 400), "Chat with a Chatbot");
            // Make output label
            GUI.Label (new Rect (20, 300, 580, 40), Output_Text, outputstyle);
            // Make a text field that modifies Input_Text.
            Input_Text = GUI.TextField (new Rect (20, 350, 580, 20), Input_Text, 100);
            // If send button or enter pressed
            if (((Event.current.keyCode == KeyCode.Return) || GUI.Button (new Rect (550, 380, 50, 20), "Send")) && (Input_Text != "")) {
                // Simple perform chat and return output string
                Output_Text = bot.Chat (Input_Text);
                // Setting components in Scene are updated intern
                // Reset input text again
                Input_Text = "";
            }
            // When image changes, gui might be called.
            // So prevent error, if no frames loaded.
            //if(gifFrames!=null&&gifFrames.Count!=0)
                // Draw selected emoticon
                //GUI.DrawTexture(new Rect(drawPosition.x, drawPosition.y, (gifFrames[0].width*_emoticonScale), (gifFrames[0].height*_emoticonScale)), gifFrames[(int)(Time.realtimeSinceStartup * speed) % gifFrames.Count]);
        } else {
            // Try to access ChatbotCore component
            if(this.gameObject.GetComponent<ChatbotCore>())
                // Try to access bot
                if(this.gameObject.GetComponent<ChatbotCore>().bot!=null)
                    bot = this.gameObject.GetComponent<ChatbotCore>().bot;
        }
    }*/

    #endregion

}
