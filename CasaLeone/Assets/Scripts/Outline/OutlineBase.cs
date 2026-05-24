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

				Material[] uniqueMats = objectRenderer.materials; 
      
				if (uniqueMats != null && uniqueMats.Length > 1)
				{

					outlineMaterial = uniqueMats[1];
					objectRenderer.sharedMaterials = uniqueMats;
				}

			}
   
			SetOutline(false);
		}

		public void SetOutline(bool active)
		{
			if (outlineMaterial == null) return;
			
			outlineMaterial.SetFloat("OutlineThickness", active ? outlineThickness : 0f);
		}
	}
}