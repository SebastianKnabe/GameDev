using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    

    public static float dialogueCooldown;
    public static float timeSinceLastDialogue;

    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public static bool dialogueMode;
    public Queue<string> sentences;




    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        dialogueMode = false;
        dialogueCooldown = 2.05f;
        timeSinceLastDialogue = 0;
        
    }

 void Update(){

           
            if(Input.GetKeyDown(KeyCode.E) && dialogueMode){
                Debug.Log("PRESSED E");
                DisplayNextSentence();
            }

    }

    public void StartDialogue(Dialogue dialogue){


       animator.SetBool("isOpen", true);
       nameText.text = dialogue.name;
   
       sentences.Clear();

       foreach (string sentence in dialogue.sentences){
            
            sentences.Enqueue(sentence);
       }

       DisplayNextSentence();
       dialogueMode = true;
    }

    public void DisplayNextSentence(){

        if(sentences.Count == 0) {

            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        
    }
    
    IEnumerator TypeSentence(string sentence){
        Debug.Log(sentence);
        dialogueText.text = "";
        foreach(char c in sentence.ToCharArray()){
            dialogueText.text += c;
            yield return null;
        }
    }

    void EndDialogue(){
        
        animator.SetBool("isOpen", false);
        dialogueMode = false;
        timeSinceLastDialogue = Time.time + dialogueCooldown;
        
    }

}
