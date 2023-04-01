using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueBase : MonoBehaviour
    {
        public bool finished { get; set; }
        //input the text information into the textbox
        protected IEnumerator WriteText(string input, Text textHolder, Color textColor, Font textFont, float delay, AudioClip sound, float delayBetweenLines)
        {
            textHolder.color = textColor;
            textHolder.font = textFont;
            for (int i =0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                //play letter sound
                SoundManager.instance.PlaySound(sound);

                //small delay in each letter
                yield return new WaitForSeconds(delay);
            }

            yield return new WaitUntil(() => Input.GetMouseButton(0));

            finished = true;
        }
    }
}

