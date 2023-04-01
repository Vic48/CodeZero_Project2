
using System.Collections;
using UnityEngine;

namespace DialogueSystem 
{
    public class DialogueHolder : MonoBehaviour
    {

        public TextAsset dialogueTextJSON;
        public DialogueLine dialogueLine;

        private DialogueTextList dialogueTextList;

        private void Awake()
        {
            this.dialogueTextList = JsonUtility.FromJson<DialogueTextList>(dialogueTextJSON.text);
            StartCoroutine(dialogueSequence());
        }
        private IEnumerator dialogueSequence()
        {
            foreach (DialogueText dialogueText in this.dialogueTextList.DialogueText)
            {
                dialogueLine.transform.gameObject.SetActive(false);
                dialogueLine.finished = false;
                dialogueLine.transform.gameObject.SetActive(true);
                dialogueLine.setInput(dialogueText.text);
                yield return new WaitUntil(() => dialogueLine.finished);
            }
            gameObject.SetActive(false);
            //Look for all the child object and activate each one of them
            //for (int i = 0; i < transform.childCount; i++)
            //{
                //Deactivate();
                //transform.GetChild(i).gameObject.SetActive(true);
                //// tell when the line is finished
                //yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
            //}
            //gameObject.SetActive(false);
        }

        private void Deactivate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}


