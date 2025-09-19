using UnityEngine;

public class InteractableHighlight : MonoBehaviour
{
    [SerializeField] private Renderer targetRenderer; // Inspector���� �θ� Renderer �Ҵ�
    [SerializeField] private Material outlineMaterial;

    private Material[] originalMaterials;

    void Awake()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponent<Renderer>(); // fallback

        if (targetRenderer != null)
            originalMaterials = targetRenderer.materials;
    }

    public void SetHighlight(bool active)
    {
        if (targetRenderer == null) return;

        if (active)
        {
            var mats = new Material[originalMaterials.Length + 1];
            for (int i = 0; i < originalMaterials.Length; i++)
                mats[i] = originalMaterials[i];
            mats[mats.Length - 1] = outlineMaterial;
            targetRenderer.materials = mats;
        }
        else
        {
            targetRenderer.materials = originalMaterials;
        }
    }
}
