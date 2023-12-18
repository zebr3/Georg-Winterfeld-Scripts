// UiActivator.cs

using UnityEngine;

public class UiActivator : MonoBehaviour
{
    [SerializeField] RecruitmentManager recruitmentManager;
    [SerializeField] bool maybeStack;

    private void Update()
    {
        // Check if the list is not null and has at least one element
        if (recruitmentManager.allNPCData != null && maybeStack != true)
        {
            ActivateObjectsBasedOnCount();
        }
        else
        {
            MaybeStackActivateObjects();
        }
    }

    private void ActivateObjectsBasedOnCount()
    {
        int remainingCount = Mathf.Max(0, recruitmentManager.allNPCData.Count);

        if (remainingCount >= 0)
        {
            // Loop through all child objects of the parent
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                // Deactivate all child objects when the list is empty
                child.gameObject.SetActive(remainingCount > 0 && i < remainingCount);
            }
        }
    }

    private void MaybeStackActivateObjects()
    {
        int remainingCount = Mathf.Max(0, recruitmentManager.maybeStack.Count);

        if (remainingCount >= 0)
        {
            // Loop through all child objects of the parent
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                // Deactivate all child objects when the list is empty
                child.gameObject.SetActive(remainingCount > 0 && i < remainingCount);
            }
        }
    }
}
