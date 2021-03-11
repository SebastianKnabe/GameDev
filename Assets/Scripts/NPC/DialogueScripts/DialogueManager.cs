using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{

    public static DialogueManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("DialogueManager already exists");
        }
        else
        {
            instance = this;
        }
    }


    public static float dialogueCooldown;
    public static float timeSinceLastDialogue;

    public GameObject dialogueOptionUI;
    public GameObject DialogueBox;
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public float delay;
    public static bool dialogueMode;
    private bool dialogueOptionsMode; 
    public Queue<DialogueBase.Info> dialogueInfo;
    private GameObject button;
    private DialogueBase currentDialogue;
    public GameObject choiceButton;
    private List<GameObject> buttonsCreated; 



    // Start is called before the first frame update
    void Start()
    {
        DialogueBox.SetActive(false);
        dialogueOptionUI.SetActive(false);
        dialogueInfo = new Queue<DialogueBase.Info>();
        dialogueMode = false;
        dialogueCooldown = 2.05f;
        timeSinceLastDialogue = 0;
        buttonsCreated = new List<GameObject>();


    }

    public void EnqueueDialogue (DialogueBase db)
    {
        DialogueBox.SetActive(true);
        dialogueMode = true;
        currentDialogue = db;
        dialogueInfo.Clear();

        



        foreach (DialogueBase.Info info in db.dialogueInfo)
        {
            dialogueInfo.Enqueue(info);
        }
        DequeueDialogue();
       
    }


    public void DequeueDialogue()
    {
        dialogueOptionsMode = currentDialogue.dialogueOptions.Length > 0 && dialogueInfo.Count == 0 ? true : false;

        if (dialogueInfo.Count == 0)
        {
            if (dialogueOptionsMode)
            {
                dialogueOptionUI.SetActive(true);
               
                //EnqueueDialogue(currentDialogue.dialogueOptions[0].nextDialogue);
                for (int i = 0; i < currentDialogue.dialogueOptions.Length; i++)
                {
                    button = Instantiate(choiceButton, dialogueOptionUI.transform);
                    button.GetComponentInChildren<Text>().text = currentDialogue.dialogueOptions[i].answer;
                    int j = i;
                    button.GetComponent<Button>().onClick.AddListener(() => { answerEvent(j); });
                    buttonsCreated.Add(button);
                    button.SetActive(true);

                  
                }
                


                return;
            }
            DialogueBox.SetActive(false);
            dialogueMode = false;
            timeSinceLastDialogue = Time.time + dialogueCooldown;



            return;
        }


        DialogueBase.Info info = dialogueInfo.Dequeue();
        nameText.text = info.name;
        //dialogueText.text = info.text;

        Debug.Log("DequeueDialogue");
        Debug.Log("" + info.name);
        Debug.Log("" + info.text);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(info.text));
        



    }

    void answerEvent(int j)
    {
        dialogueOptionUI.SetActive(false);
        Debug.Log("HITTED " + j + " " + currentDialogue.dialogueOptions.Length);

        EnqueueDialogue(currentDialogue.dialogueOptions[j].nextDialogue);
        dialogueOptionsMode = false;
        foreach (GameObject go in buttonsCreated)
        {
            buttonsCreated.Remove(go);
            Destroy(go);
        }



        

    }



    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char c in sentence.ToCharArray())
        {
            yield return new WaitForSeconds(delay);
            dialogueText.text += c;
            yield return null;
        }
    }


    void Update()
    {


        if (Input.GetKeyDown(KeyCode.E) && dialogueMode)
        {
            
            DequeueDialogue();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && (dialogueMode || dialogueOptionsMode))
        {

            dialogueOptionUI.SetActive(false);
            DialogueBox.SetActive(false);
            dialogueMode = false;
            timeSinceLastDialogue = Time.time + dialogueCooldown;
        }


    }

    /*
   


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
      */
}
