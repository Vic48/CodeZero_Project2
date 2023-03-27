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

        private void Awake()
        {
            textHolder = GetComponent<Text>();
            StartCoroutine(WriteText(input, textHolder, textColor, textFont));
        }
    }
}

