using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class TapTeleporter : MonoBehaviour
{
    public Camera vrCamera;
    public float speed = 2f;

    CharacterController character;

    void Awake()
    {
        character = GetComponent<CharacterController>();
        if (vrCamera == null) vrCamera = Camera.main;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            TryTeleport(Input.mousePosition);
#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            TryTeleport(Input.GetTouch(0).position);
#endif
    }

    void TryTeleport(Vector2 screenPos)
    {
        Ray ray = vrCamera.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Start a smooth move or instant teleport:
            StartCoroutine(MoveRoutine(hit.point));
        }
    }

    IEnumerator MoveRoutine(Vector3 target)
    {
        Vector3 start = transform.position;
        float dist = Vector3.Distance(start, target);
        float duration = dist / speed;
        float elapsed = 0f;

        // Lift target to character's height
        target.y = start.y;

        while (elapsed < duration)
        {
            Vector3 next = Vector3.Lerp(start, target, elapsed / duration);
            character.Move(next - character.transform.position);
            elapsed += Time.deltaTime;
            yield return null;
        }
        // Final step
        character.Move(target - character.transform.position);
    }
}
