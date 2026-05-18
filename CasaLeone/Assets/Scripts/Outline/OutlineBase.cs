using UnityEngine;

namespace Outline
{
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

		public void SetOutline(bool active)
		{
			if (outlineMaterial == null) return;
			Debug.Log(active);
			outlineMaterial.SetFloat("OutlineThickness", active ? outlineThickness : 0f);
			
		}
	}
}