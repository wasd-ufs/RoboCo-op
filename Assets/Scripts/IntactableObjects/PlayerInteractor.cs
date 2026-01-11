using System.Linq;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MonoBehaviour inputSource;
    private IPlayerInput input;

    [Header("Interaction")]
    [SerializeField] private float searchRadius = 1.5f;
    

    private void Awake()
    {
        input = inputSource as IPlayerInput;
        if (input == null)
        {
            Debug.LogError("PlayerInteractor: inputSource não implementa IPlayerInput");
            enabled = false;
        }
    }

    private void Update()
    {
        if (input.Interact.JustPressed)
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        IInteractable interactable = FindNearestObject<IInteractable>();
        interactable?.Interact();
    }

    public T FindNearestObject<T>() where T : IInteractable
    {
        var objectsOfType = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<T>();

        T nearest = default;
        float minDistance = Mathf.Infinity;

        foreach (var item in objectsOfType)
        {
            var mono = item as MonoBehaviour;
            float distance = Vector2.Distance(transform.position, mono.transform.position);

            if (distance <= searchRadius && distance < minDistance)
            {
                minDistance = distance;
                nearest = item;
            }
        }

        return nearest;
    }
}