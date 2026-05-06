using UnityEngine;

public abstract class OutlineBase : MonoBehaviour
{
    [SerializeField] protected Renderer objectRenderer;
    [SerializeField] protected float outlineThickness = 0.015f;
    protected Material outlineMaterial;

    protected virtual void Awake()
    {
        if (objectRenderer != null)
        {
            Material[] mats = objectRenderer.materials;
            outlineMaterial = mats[1];
        }
        SetOutline(false);
    }

    public virtual void SetOutline(bool active)
    {
        if (outlineMaterial == null) return;
        outlineMaterial.SetFloat("_OutlineThickness", active ? outlineThickness : 0f);
    }
}