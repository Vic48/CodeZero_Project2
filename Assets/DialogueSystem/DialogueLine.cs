using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBase
    {
        private Text textHolder;

        [Header("Text Options")] // create sections with custom names
        // can be edited in unity
        [SerializeField] private string input;
        [SerializeField] private Color textColor;
        [SerializeField] private Font textFont;

        [Header("Time Parameter")]
        [SerializeField] private float delay;
        [SerializeField] private float delayBetweenLines;

        [Header("Audio Sound")]
        [SerializeField] private AudioClip sound;

        private void Awake()
        {
            textHolder = GetComponent<Text>();
            textHolder.text = "";
        }

        private void Start()
        { }

        public void setInput(string text)
        {
            textHolder = GetComponent<Text>();
            textHolder.text = "";
            this.input = text;
            StartCoroutine(WriteText(input, textHolder, textColor, textFont, delay, sound, delayBetweenLines));
        }
    }
}

