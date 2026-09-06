using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class UIGradient : MonoBehaviour, IMeshModifier
{
    [SerializeField] private Color _colorLeft = new Color(1f, 0.41f, 0.71f);
    [SerializeField] private Color _colorRight = new Color(0.2f, 0.8f, 0.4f);

    public void ModifyMesh(Mesh mesh) { }

    public void ModifyMesh(VertexHelper vh)
    {
        UIVertex vertex = default;
        int count = vh.currentVertCount;

        float minX = float.MaxValue;
        float maxX = float.MinValue;

        for (int i = 0; i < count; i++)
        {
            vh.PopulateUIVertex(ref vertex, i);
            minX = Mathf.Min(minX, vertex.position.x);
            maxX = Mathf.Max(maxX, vertex.position.x);
        }

        float width = maxX - minX;

        for (int i = 0; i < count; i++)
        {
            vh.PopulateUIVertex(ref vertex, i);
            float t = width > 0f ? (vertex.position.x - minX) / width : 0f;
            vertex.color = Color.Lerp(_colorLeft, _colorRight, t);
            vh.SetUIVertex(vertex, i);
        }
    }

    void OnEnable()
    {
        GetComponent<Graphic>().SetVerticesDirty();
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        GetComponent<Graphic>().SetVerticesDirty();
    }
#endif
}