// UiActivator.cs

using UnityEngine;

public class UiActivator : MonoBehaviour
{
    [SerializeField] RecruitmentManager recruitmentManager;
    [SerializeField] bool maybeStack;
    [SerializeField] GameObject postIt;
    [SerializeField] GameObject mainPostIt;

    private void Update()
    {
        if (maybeStack)
        {
            MaybeStackActivateObjects();
            ActivatePostItBasedOnMaybeStack();
        }
        else
        {
            ActivateObjectsBasedOnCount();
        }
    }

    private void ActivateObjectsBasedOnCount()
    {
        int remainingCount = Mathf.Max(0, recruitmentManager.allNPCData.Count - 1);

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(remainingCount > 0 && i < remainingCount);
        }
    }

    private void MaybeStackActivateObjects()
    {
        int maybeStackCount = recruitmentManager.maybeStack.Count;
        int allNPCDataCount = recruitmentManager.allNPCData.Count;

        int activeCount = (allNPCDataCount > 0) ? maybeStackCount : Mathf.Max(0, maybeStackCount - 1);

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(i < activeCount);
        }
    }

    private void ActivatePostItBasedOnMaybeStack()
    {
        // Check if at least one child object of maybeStack is active
        bool atLeastOneActive = false;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if (child.gameObject.activeSelf)
            {
                atLeastOneActive = true;
                break;
            }
        }

        // Activate or deactivate the postIt GameObject based on the condition
        if(mainPostIt.activeInHierarchy == false)
        {
            postIt.SetActive(atLeastOneActive);
        }
       
    }
}
