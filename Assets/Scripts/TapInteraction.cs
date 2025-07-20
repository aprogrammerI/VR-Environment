using UnityEngine;

public class TapInteraction : MonoBehaviour
{
    public string parentTag = "Flowers";

    void Update()
    {
#if UNITY_EDITOR
        // In the Editor, allow left-click to simulate a tap
        if (Input.GetMouseButtonDown(0))
        {
            ProcessTap(Input.mousePosition);
        }
#else
        // On device, detect a single touch
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ProcessTap(Input.GetTouch(0).position);
        }
#endif
    }

    private void ProcessTap(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Walk up the parent chain looking for the Flowers tag
            Transform t = hit.transform;
            while (t != null)
            {
                if (t.CompareTag(parentTag))
                {
                    Destroy(t.gameObject);
                    Debug.Log($"Destroyed parent {t.name} and all its children.");
                    return;
                }
                t = t.parent;
            }
        }
    }
}
