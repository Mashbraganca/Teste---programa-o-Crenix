using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Vector3 defaultPos;
    public bool droppedOnSlot;

    private Vector2 oldAnchorMin, oldAnchorMax;
    private bool reseted = false;
    [SerializeField]
    GameObject gearPrefab;
    GameObject gearInstance;
    GearSlot gearSlot;

    private void Start()
    {
        defaultPos = transform.position;
        oldAnchorMin = rectTransform.anchorMin;
        oldAnchorMax = rectTransform.anchorMax;
    }
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        defaultPos = transform.position;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        eventData.pointerDrag.GetComponent<DragDrop>().droppedOnSlot = false;
        if (gearInstance != null)
        {
            gearInstance.SetActive(false);
            gearSlot.setGear(null);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (droppedOnSlot == false)
        {
            transform.position = defaultPos;
        }

        if (gearInstance == null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit.collider != null)
            {
                gearSlot = hit.collider.gameObject.GetComponent<GearSlot>();
                if (!gearSlot.isGearSetted())
                {
                    Vector3 pos = hit.collider.gameObject.transform.position;
                    gearInstance = Instantiate(gearPrefab, pos, Quaternion.identity);
                    canvasGroup.alpha = 0f;
                    GetComponent<RectTransform>().anchoredPosition = Camera.main.WorldToViewportPoint(pos);
                    oldAnchorMin = rectTransform.anchorMin;
                    oldAnchorMax = rectTransform.anchorMax;
                    defaultPos = pos;
                    rectTransform.anchorMin = Camera.main.WorldToViewportPoint(pos);
                    rectTransform.anchorMax = Camera.main.WorldToViewportPoint(pos);
                    gearSlot.setGear(gearInstance);
                    transform.position = defaultPos;
                }
            }
        }
        else
        {
            gearInstance.SetActive(true);
            if (gearSlot != null)
            {
                gearSlot.setGear(gearInstance);
            }
            canvasGroup.alpha = 0f;
            if (reseted)
            {
                canvasGroup.alpha = 1f;
                reseted = false;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void ResetAnchor()
    {
        rectTransform.anchorMin = oldAnchorMin;
        rectTransform.anchorMax = oldAnchorMax;
        if (gearInstance != null)
        {
            Destroy(gearInstance);
            gearSlot = null;
        }
        reseted = true;
    }
}
