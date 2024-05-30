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
                SetInteractText("EŰ�� ���� ������ �ݱ�");
            }
        } 
        else if(Physics.Raycast(ray, out hit, rayCheckDistance, goalLayerMask))
        {
            curInteractableObj = hit.collider.gameObject;
            SetInteractText("EŰ�� ���� ���� Ŭ�����ϱ�");
        }
        else if (Physics.Raycast(ray, out hit, rayCheckDistance, trapLayerMask))
        {
            SetInteractText("������ ���� �� �����ϴ�");
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
            // �����۰� ��ȣ�ۿ� �ϴ� ���
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
