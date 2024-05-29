using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public LayerMask interactLayerMask;
    public TextMeshProUGUI promptText;
    public float rayCheckDistance;

    private GameObject curInteractableObj;
    private void Update()
    {
        CheckRay();
    }

    void CheckRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, rayCheckDistance, interactLayerMask))
        {
            if (hit.collider.gameObject != curInteractableObj)
            {
                curInteractableObj = hit.collider.gameObject;
                SetPromptText();
            }
        }
        else
        {
            curInteractableObj = null;
            promptText.gameObject.SetActive(false);
        }
    }

    void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractableObj.GetComponent<Item>().ItemDataToString();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractableObj != null)
        {
            curInteractableObj.GetComponent<Item>().OnInteract();
            curInteractableObj = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
