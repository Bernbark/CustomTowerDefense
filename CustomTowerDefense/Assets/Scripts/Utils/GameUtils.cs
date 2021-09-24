
using UnityEngine;

public class GameUtils : MonoBehaviour{

    public static GameUtils Instance { get; set; }
    private Camera camera;
    public const int sortingOrderDefault = 5000;
    public static TextMesh CreateWorldText(string text, Vector3 localPosition,Transform parent = null, int fontSize = 10, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = sortingOrderDefault)
    {
        if (color == null) color = Color.white;
        return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
    }

    private void Awake()
    {
        Instance = this;
    }

    // Create Text in the World
    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.fontStyle = FontStyle.Bold;
        textMesh.color = color;
        textMesh.transform.localScale *= .1f;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }

    public Camera GetCamera()
    {
        camera = (Camera)FindObjectOfType(typeof(Camera));
        if (camera)
        {
            return camera;
        }
        else
        {
            camera = Camera.main;
        }
        return camera;
    }

    public Vector3 GetMouseWorldPositionForUtils()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, GetCamera());
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPosition()
    {
        return Instance.GetMouseWorldPositionForUtils();
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    public static void DeserializeTransform(Transform transform, SerializedTransform serializedTransform)
    {
        transform.localPosition.Set(serializedTransform.position[0], serializedTransform.position[1], serializedTransform.position[2]);
        transform.localScale.Set(serializedTransform.scale[0], serializedTransform.scale[1], serializedTransform.scale[2]);
    }
}
