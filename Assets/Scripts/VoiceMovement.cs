using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
using System.Linq;


public class VoiceMovement : MonoBehaviour
{
    private KeywordRecognizer KeywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    public CollisionHandler collisionHandler;

    void Start()
    {
        actions.Add("forward", Forward);
        actions.Add("backward", Backward);
        actions.Add("Boost", Boost);
        actions.Add("down", Down);

        KeywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        KeywordRecognizer.OnPhraseRecognized +=  RecognizedSpeech;
        KeywordRecognizer.Start();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void Forward()
    {
        transform.Translate(1, 0, 0);
    }
    private void Backward()
    {
        transform.Translate(-1, 0, 0);
    }
    private void Boost()
    {
        transform.Translate(0, 4, 0);
    }
    private void Down()
    {
        transform.Translate(0, -1, 0);
    }
}