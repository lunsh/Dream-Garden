using UnityEngine;
using UnityEngine.EventSystems;

public class CloseOnContextLoss : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // thank u https://forum.unity.com/threads/how-to-close-a-ui-panel-when-clicking-outside.322565/
    bool inContext;
    GameObject myGO;

    private void Awake()
    {
        myGO = gameObject;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !inContext)
        {
            myGO.SetActive(inContext);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inContext = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inContext = false;
    }
}
