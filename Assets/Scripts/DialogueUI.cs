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
    public delegate void EndDialogueDelegate();
    public static EndDialogueDelegate endDialogueDelegate;


    private void Start() {
        _typewriter = GetComponent<Typewriter>();
        CloseDialogueBox();
        initiateDialogueDelegate += ShowDialogue;
        endDialogueDelegate += CloseDialogueBox;
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
        endDialogueDelegate?.Invoke();
    }

    private void CloseDialogueBox(){
        _dialogueBox.SetActive(false);
        _textLabel.text = string.Empty;
    }
}
