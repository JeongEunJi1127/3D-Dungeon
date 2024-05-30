using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public LayerMask interactLayerMask;
    public LayerMask goalLayerMask;
    public LayerMask trapLayerMask;
    public TextMeshProUGUI promptText;
    public TextMeshProUGUI interactText;
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
                SetInteractText("E키를 눌러 아이템 줍기");
            }
        } 
        else if(Physics.Raycast(ray, out hit, rayCheckDistance, goalLayerMask))
        {
            curInteractableObj = hit.collider.gameObject;
            SetInteractText("E키를 눌러 게임 클리어하기");
        }
        else if (Physics.Raycast(ray, out hit, rayCheckDistance, trapLayerMask))
        {
            SetInteractText("닿으면 아플 것 같습니다");
        }
        else
        {
            curInteractableObj = null;
            promptText.gameObject.SetActive(false);
            interactText.gameObject.SetActive(false);
        }
    }

    void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractableObj.GetComponent<Item>().ItemDataToString();
    }

    void SetInteractText(string str)
    {
        interactText.gameObject.SetActive(true);
        interactText.text = str;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractableObj != null)
        {
            Item item;
            // 아이템과 상호작용 하는 경우
            if (curInteractableObj.TryGetComponent<Item>(out item))
            {
                item.OnInteract();
            }
            else if(curInteractableObj.layer == 7)
            {
                CharacterManager.Instance.Player.condition.Win();
            }
            curInteractableObj = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
