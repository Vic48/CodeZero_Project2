
using System.Collections;
using UnityEngine;
using System.Linq;

namespace DialogueSystem 
{
    public class DialogueHolder : MonoBehaviour
    {

        public TextAsset dialogueTextJSON;
        public DialogueLine dialogueLine;

        private TextFromJson text;

        private void Awake()
        {
            this.text = JsonUtility.FromJson<TextFromJson>(dialogueTextJSON.text);
            StartCoroutine(dialogueSequence());
        }
        private IEnumerator dialogueSequence()
        {
            // Search for objects where the "Character" field is "Start"
            var filteredList = text.DialogueText.Where(obj => obj.character == "Start");

            // Sort the filtered list based on the "Order" field
            var sortedList = filteredList.OrderBy(obj => obj.order);

            foreach (DialogueText dialogueText in sortedList)
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

        public IEnumerator NpcDialogueSequence()
        {
            gameObject.SetActive(true);
            // Search for objects where the "Character" field is "Start"
            var filteredList = text.DialogueText.Where(obj => obj.character == "NPC");

            // Sort the filtered list based on the "Order" field
            var sortedList = filteredList.OrderBy(obj => obj.order);

            foreach (DialogueText dialogueText in sortedList)
            {
                dialogueLine.transform.gameObject.SetActive(false);
                dialogueLine.finished = false;
                dialogueLine.transform.gameObject.SetActive(true);
                dialogueLine.setInput(dialogueText.text);
                yield return new WaitUntil(() => dialogueLine.finished);
            }
            gameObject.SetActive(false);
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


