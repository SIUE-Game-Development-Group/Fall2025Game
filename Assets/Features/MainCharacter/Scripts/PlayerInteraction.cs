using Core.Scripts.Input;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Interactable currentInteractable;

    private void Update()
    {
        if (InputManager.Instance.InteractJustPressed)
        {
            if (currentInteractable != null)
            {
                Debug.Log("Interacting with " + currentInteractable, currentInteractable);
                currentInteractable.Interact();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Interactable interactable))
        {
            currentInteractable = interactable;
            Debug.Log(currentInteractable + " nearby (E to interact)", currentInteractable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Interactable interactable))
        {
            if (interactable == currentInteractable)
            {
                currentInteractable = null;
                Debug.Log("No longer near any interactable");
            }
        }
    }
}
