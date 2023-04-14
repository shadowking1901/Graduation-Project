using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollRectClick : Selectable, IPointerDownHandler, IDragHandler, IEventSystemHandler
{
    public Camera m_camera;
    public RectTransform content;
    public Vector3 contentPoint;
    public Vector4 limitBounds;
    public ScrollRect.ScrollRectEvent onValueChanged;

    [SerializeField]
    private bool isScreenSpace = true;
    private RectTransform rect;
    private Vector3 screenPoint;

    protected override void Awake()
    {
        rect = transform as RectTransform;
        limitBounds.x = -rect.sizeDelta.x / 2;
        limitBounds.y = rect.sizeDelta.x / 2;
        limitBounds.z = -rect.sizeDelta.y / 2;
        limitBounds.w = rect.sizeDelta.y / 2;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (isScreenSpace)
        {
            contentPoint = rect.InverseTransformPoint(eventData.position);
        }
        else
        {
            screenPoint = m_camera.WorldToScreenPoint(transform.position);
            Vector3 world = m_camera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, screenPoint.z));
            contentPoint = rect.InverseTransformPoint(world);
        }
        contentPoint.x = Mathf.Max(limitBounds.x, Mathf.Min(limitBounds.y, contentPoint.x));
        contentPoint.y = Mathf.Max(limitBounds.z, Mathf.Min(limitBounds.w, contentPoint.y));
        content.anchoredPosition = contentPoint;
        onValueChanged.Invoke(contentPoint);
    }

    public void OnDrag(PointerEventData eventData)
    {
         if (isScreenSpace)
        {
            contentPoint = rect.InverseTransformPoint(eventData.position);
        }
        else
        {
            screenPoint = m_camera.WorldToScreenPoint(transform.position);
            Vector3 world = m_camera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, screenPoint.z));
            contentPoint = rect.InverseTransformPoint(world);
        }
        contentPoint.x = Mathf.Max(limitBounds.x, Mathf.Min(limitBounds.y, contentPoint.x));
        contentPoint.y = Mathf.Max(limitBounds.z, Mathf.Min(limitBounds.w, contentPoint.y));
        content.anchoredPosition = contentPoint;
        onValueChanged.Invoke(contentPoint);
    }
}
