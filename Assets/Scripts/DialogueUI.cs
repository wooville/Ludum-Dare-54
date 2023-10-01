using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject _nameBox;
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private TMP_Text _textLabel;
    [SerializeField] private TMP_Text _nameLabel;
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
        _nameBox.SetActive(true);
        _nameLabel.text = dialogueObject.SpeakerName;
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject){
        foreach (string dialogue in dialogueObject.Dialogue){
            yield return _typewriter.Run(dialogue, _textLabel);
            yield return new WaitUntil(() => Input.GetButtonDown("Jump"));
        }
        // wack but ok
        endDialogueDelegate?.Invoke();
    }

    private void CloseDialogueBox(){
        _dialogueBox.SetActive(false);
        _nameBox.SetActive(false);
        _textLabel.text = string.Empty;
        _nameLabel.text = string.Empty;
    }
}
