using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueBase : MonoBehaviour
    {
        //input the text information into the textbox
        protected IEnumerator WriteText(string input, Text textHolder, Color textColor, Font textFont)
        {
            textHolder.color = textColor;
            textHolder.font = textFont;
            for (int i =0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                //small delay in each letter
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}

