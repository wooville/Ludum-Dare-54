using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    public Image image;
    public Sprite[] spriteArrayEnding1;
    public Sprite[] spriteArrayEnding2;
    public float speed = .02f;
    public EndGameUI.ENDINGS ending;
    private int _indexSprite;
    Coroutine coroutineAnim;
    bool isDone;

    public void StartUIAnimation(){
        isDone = false;
        StartCoroutine(PlayUIAnimation());
    }

    IEnumerator PlayUIAnimation(){
        yield return new WaitForSeconds(speed);

        Sprite[] spriteArray;
        switch(ending){
            case EndGameUI.ENDINGS.ENDING_BOUNCER:
                spriteArray = spriteArrayEnding1;
                break;
            case EndGameUI.ENDINGS.ENDING_REBIRTH:
                spriteArray = spriteArrayEnding2;
                break;
            default:
                spriteArray = spriteArrayEnding1;
                break;
        }

        if (_indexSprite >= spriteArray.Length){
            _indexSprite = 0;
        }

        image.sprite = spriteArray[_indexSprite];
        _indexSprite += 1;
        if (!isDone){
            coroutineAnim = StartCoroutine(PlayUIAnimation());
        }
    }
}
