using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneClear : MonoBehaviour
{
    [SerializeField] private Button ClearButton;

    private void Awake()
    {
        ClearButton.onClick.AddListener(() => 
        {
            ClearAllPaintable(); 
        });
    }

    private void ClearAllPaintable()
    {
        Paintable[] paintables = FindObjectsOfType<Paintable>();
        foreach (Paintable paintable in paintables)
        {
            PaintManager.instance.ClearMask(paintable);
        }
    }
}
