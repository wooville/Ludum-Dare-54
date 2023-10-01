using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private string speakerName;
    public string SpeakerName => speakerName;
    public string[] Dialogue => dialogue;


}
