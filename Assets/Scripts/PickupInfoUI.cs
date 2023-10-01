using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupInfoUI : MonoBehaviour
{
    [SerializeField] private GameObject _nameBox;
    [SerializeField] private GameObject _infoBox;
    [SerializeField] private TMP_Text _infoLabel;
    [SerializeField] private TMP_Text _nameLabel;
    private Typewriter _typewriter;
    public delegate void InitiatePickupDelegate(DialogueObject dialogueObject);
    public static InitiatePickupDelegate initiatePickupDelegate;
    public delegate void EndPickupDelegate();
    public static EndPickupDelegate endPickupDelegate;


    private void Start() {
        _typewriter = GetComponent<Typewriter>();
        CloseInfoBox();
        initiatePickupDelegate += ShowInfo;
        endPickupDelegate += CloseInfoBox;
        // ShowInfo(_testDialogue);
    }

    public void ShowInfo(DialogueObject dialogueObject){
        _infoBox.SetActive(true);
        _nameBox.SetActive(true);
        _nameLabel.text = dialogueObject.SpeakerName;
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject){
        foreach (string dialogue in dialogueObject.Dialogue){
            yield return _typewriter.Run(dialogue, _infoLabel);
            yield return new WaitUntil(() => Input.GetButtonDown("Jump"));
        }
        endPickupDelegate?.Invoke();
    }

    private void CloseInfoBox(){
        _infoBox.SetActive(false);
        _nameBox.SetActive(false);
        _infoLabel.text = string.Empty;
        _nameLabel.text = string.Empty;
    }
}
