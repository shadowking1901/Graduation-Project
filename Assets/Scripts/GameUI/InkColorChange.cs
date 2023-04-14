using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InkColorChange : MonoBehaviour
{
    [SerializeField] private RawImage paint;
    [SerializeField] private Material inkMaterial;
    [SerializeField] private GameObject spray;

    private ParticlesController sprayColor;
    private Button clickChange;
    private Color originalColor;

    private void Start()
    {
        sprayColor = spray.GetComponent<ParticlesController>();
        clickChange = GetComponent<Button>();
        originalColor = inkMaterial.color;
    }

    private void Update()
    {
        clickChange.onClick.AddListener(()=> {
            inkMaterial.color = paint.color;
            sprayColor.paintColor = paint.color;
        });
    }

    public void ResetColor()
    {
        inkMaterial.color = originalColor;
    }
}
