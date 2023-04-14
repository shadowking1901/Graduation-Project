using UnityEngine;
using UnityEngine.UI;


public class ColorPick : MonoBehaviour
{
    public RawImage paint;
    public RawImage saturation;
    public RawImage hue;
    public RawImage alpha;

    public Button paintBtn;
    public ScrollRectClick scrollRectSaturation;
    public Scrollbar scrollbarHue;
    public Scrollbar scrollbarAlpha;
    private Vector4 currentColorHSV = new Vector4(0, 1, 1, 1);

    private readonly float piexlWidth = 256.0f;
    private readonly float piexlHeight = 256.0f;
    private readonly float huePiexWidth = 50.0f;
    private Texture2D saturationTexture2D;
    private Texture2D hueTexture2D;
    private Texture2D alphaTexture2D;

    [SerializeField]
    private bool isShowPanel = true;

    public Color PaintColor
    {
        get { return HSVToRGB(currentColorHSV); }
    }
    private void Awake()
    {
        InitPaintPick();
    }

    private void InitPaintPick()
    {
        paintBtn.onClick.AddListener(OnPaintClick);
        scrollRectSaturation.onValueChanged.AddListener(OnSaturationClick);
        scrollbarHue.onValueChanged.AddListener(OnHueClick);
        scrollbarAlpha.onValueChanged.AddListener(OnAlphaClick);

        saturationTexture2D = new Texture2D((int)piexlWidth, (int)piexlHeight);
        saturation.texture = saturationTexture2D;

        hueTexture2D = new Texture2D((int)huePiexWidth, (int)piexlHeight);
        hue.texture = hueTexture2D;

        alphaTexture2D = new Texture2D((int)huePiexWidth, (int)piexlHeight);
        alpha.texture = alphaTexture2D;

        scrollbarHue.value = 1 - currentColorHSV.x;
        scrollbarAlpha.value = currentColorHSV.w;

        OnPaintClick();
        UpdateSaturation(currentColorHSV);
        UpdateHue();
        UpdateAlpha();
        UpdateSaturationPoint(currentColorHSV);
        PaintChange();
    }

    private void PaintChange()
    {
        paint.color = PaintColor;
    }

    void OnPaintClick()
    {
        saturation.gameObject.SetActive(isShowPanel);
        hue.gameObject.SetActive(isShowPanel);
        alpha.gameObject.SetActive(isShowPanel);
        isShowPanel = !isShowPanel;
    }

    public void OnSaturationClick(Vector2 point)
    {
        Vector2 hsv = GetSaturationHSV(point);
        currentColorHSV.y = hsv.x;
        currentColorHSV.z = hsv.y;
        UpdateSaturationPoint(currentColorHSV);
        PaintChange();
        UpdateAlpha();
    }

    public void OnHueClick(float value)
    {
        currentColorHSV.x = 1 - value;
        UpdateSaturationPoint(currentColorHSV);
        PaintChange();
        UpdateSaturation(currentColorHSV);
        UpdateAlpha();
    }

    public void OnAlphaClick(float value)
    {
        currentColorHSV.w = value;
        UpdateSaturationPoint(currentColorHSV);
        PaintChange();
    }

    public void UpdateSaturation(Vector4 hsv)
    {
        for (int y = 0; y < piexlHeight; y++)
        {
            for (int x = 0; x < piexlWidth; x++)
            {
                Color pixColor = GetSaturation(hsv, x / piexlWidth, y / piexlHeight);
                saturationTexture2D.SetPixel(x, y, pixColor);
            }
        }
        saturationTexture2D.Apply();
        saturation.texture = saturationTexture2D;
    }

    public void UpdateHue()
    {
        for (int y = 0; y < piexlHeight; y++)
        {
            for (int x = 0; x < huePiexWidth; x++)
            {
                Color pixColor = GetHue(y / piexlHeight);
                hueTexture2D.SetPixel(x, y, pixColor);
            }
        }
        hueTexture2D.Apply();
        hue.texture = hueTexture2D;
    }

    public void UpdateAlpha()
    {
        for (int y = 0; y < piexlHeight; y++)
        {
            Color pixColor = GetAlpha(y / piexlHeight);
            for (int x = 0; x < huePiexWidth; x++)
            {
                alphaTexture2D.SetPixel(x, (int)piexlHeight - y, pixColor);
            }
        }
        alphaTexture2D.Apply();
        alpha.texture = alphaTexture2D;
    }

    public void UpdateSaturationPoint(Vector4 hsv)
    {
        Vector2 saturationPoint = GetSaturationPoint(hsv);
        scrollRectSaturation.content.anchoredPosition = saturationPoint;
    }

    private Color GetSaturation(Vector4 hsv, float x, float y)
    {
        Vector4 saturationHSV = hsv;
        saturationHSV.y = x;
        saturationHSV.z = y;
        saturationHSV.w = 1;
        return HSVToRGB(saturationHSV);
    }

    private Color GetHue(float y)
    {
        Vector4 hueHSV = new Vector4(y, currentColorHSV.y, currentColorHSV.z, 1);
        return HSVToRGB(hueHSV);
    }

    private Color GetAlpha(float y)
    {
        Vector4 hueHSV = new Vector4(currentColorHSV.x, currentColorHSV.y, currentColorHSV.z, y);
        return HSVToRGB(hueHSV);
    }

    private Vector2 GetSaturationHSV(Vector2 point)
    {
        Vector2 hsv = new Vector2();
        hsv.x = point.x / saturation.rectTransform.sizeDelta.x + 0.5f;
        hsv.y = point.y / saturation.rectTransform.sizeDelta.y + 0.5f;
        return hsv;
    }

    private Vector2 GetSaturationPoint(Vector4 hsv)
    {
        Vector2 point = new Vector2();
        point.x = (hsv.y - 0.5f) * saturation.rectTransform.sizeDelta.x;
        point.y = (hsv.z - 0.5f) * saturation.rectTransform.sizeDelta.y;
        return point;
    }

    private Color HSVToRGB(Vector4 hsv)
    {
        Color color = Color.HSVToRGB(hsv.x, hsv.y, hsv.z);
        color.a = hsv.w;
        return color;
    }
}
