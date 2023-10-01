using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Collider2D interactionCollider;
	private List<Interactable> nearbyInteractables = new List<Interactable>();
    private int selectedInteractable = 0;
    public enum PICKUPS {NONE, WINGS, LIGHT, BOOTS, KEY1}

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact")){
            if (nearbyInteractables.Count > 0){
				nearbyInteractables[selectedInteractable].Interact();
			}
        }

        if (Input.GetButtonDown("CycleInteract")){
			selectedInteractable++;
			selectedInteractable = (nearbyInteractables.Count > 0) ? selectedInteractable%nearbyInteractables.Count : 0;
		}
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("Interactable")){
            var interactable = other.GetComponent<Interactable>();
            nearbyInteractables.Add(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag.Equals("Interactable")){
            var interactable = other.GetComponent<Interactable>();
            nearbyInteractables.Remove(interactable);
        }
    }
}
