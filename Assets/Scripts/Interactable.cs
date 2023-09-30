using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isInteractable {get;set;} = false;
    // protected Label buttonPromptLabel;

    // Start is called before the first frame update
    void Start()
    {
        // get ui component to be toggled
    }

    public virtual void Interact()
    {
        Destroy(gameObject);
    }

    public void ToggleButtonPrompt(){
        // Debug.Log(name + " interactable");
        // buttonPromptContainer.Visible = !buttonPromptContainer.Visible;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("Player")){
            isInteractable = true;
            ToggleButtonPrompt();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag.Equals("Player")){
            isInteractable = false;
            ToggleButtonPrompt();
        }
    }
}
