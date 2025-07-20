using UnityEngine;

public class ClickInteraction : MonoBehaviour
{
    // The name of the parent tag to look for
    public string parentTag = "Flowers";

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera through the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Did we hit something whose parent has the right tag?
                Transform current = hit.transform;
                while (current != null)
                {
                    if (current.CompareTag(parentTag))
                    {
                        // Destroy the parent GameObject (and all its children)
                        Destroy(current.gameObject);
                        Debug.Log($"Destroyed parent {current.name} and all its children.");
                        return;
                    }
                    current = current.parent;
                }
            }
        }
    }
}
