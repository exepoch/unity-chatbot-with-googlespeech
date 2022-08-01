using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpeechButtonBehaviour : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public AdvancedTestChat tChat;


    public void OnPointerDown(PointerEventData eventData)
    {
        tChat.StartRec();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Invoke("Delay", 0.5f);
    }

    void Delay() //Konusma bitirmeye hafif gecikme koydum. Kullanıcıların cümleyi bitirmeyle birebir butondan ellerini cekmeleri son kelimeyi ignore etmelerine sebep oluyor.
    {
        tChat.StopRec();
    }
}
