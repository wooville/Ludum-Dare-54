using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private GameObject _animationImage;
    [SerializeField] private GameObject _infoBox;
    [SerializeField] private TMP_Text _infoLabel;
    [SerializeField] private ENDINGS _ending;
    private Typewriter _typewriter;
    public delegate void GameEndDelegate(ENDINGS ending);
    public static GameEndDelegate gameEndDelegate;
    public delegate void InitiateGameEndDelegate(DialogueObject dialogueObject);
    public static InitiateGameEndDelegate initiateGameEndDelegate;
    public delegate void ResetGameDelegate();
    public static ResetGameDelegate resetGameDelegate;
    public enum ENDINGS {ENDING_BOUNCER, ENDING_REBIRTH}


    private void Start() {
        _typewriter = GetComponent<Typewriter>();
        CloseInfoBox();
        gameEndDelegate += AnimateEnding;
        initiateGameEndDelegate += ShowInfo;
        resetGameDelegate += CloseInfoBox;
        // ShowInfo(_testDialogue);
    }

    public void ShowInfo(DialogueObject dialogueObject){
        _infoBox.SetActive(true);
        _animationImage.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject){
        foreach (string dialogue in dialogueObject.Dialogue){
            yield return _typewriter.Run(dialogue, _infoLabel);
            yield return new WaitUntil(() => Input.GetButtonDown("Jump"));
        }
        CloseInfoBox();
        yield return new WaitUntil(() => Input.GetButtonDown("Jump"));
        resetGameDelegate?.Invoke();
    }

    private void CloseInfoBox(){
        _infoBox.SetActive(false);
        _infoLabel.text = string.Empty;
    }

    private void AnimateEnding(ENDINGS ending){
        var anim = _animationImage.GetComponent<UIAnimation>();
        anim.ending = ending;
        _animationImage.SetActive(true);
        anim.StartUIAnimation();
    }
}
