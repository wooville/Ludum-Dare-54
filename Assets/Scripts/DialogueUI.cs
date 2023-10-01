using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _dialogueBox;
    [SerializeField]
    private TMP_Text _textLabel;
    private Typewriter _typewriter;
    public delegate void InitiateDialogueDelegate(DialogueObject dialogueObject);
    public static InitiateDialogueDelegate initiateDialogueDelegate;


    private void Start() {
        _typewriter = GetComponent<Typewriter>();
        CloseDialogueBox();
        initiateDialogueDelegate += ShowDialogue;
        // ShowDialogue(_testDialogue);
        
    }

    public void ShowDialogue(DialogueObject dialogueObject){
        _dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject){
        foreach (string dialogue in dialogueObject.Dialogue){
            yield return _typewriter.Run(dialogue, _textLabel);
            yield return new WaitUntil(() => Input.GetButtonDown("Jump"));
        }
        CloseDialogueBox();
    }

    private void CloseDialogueBox(){
        _dialogueBox.SetActive(false);
        _textLabel.text = string.Empty;
    }
}
