using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class LayoutGroupIgnore : MonoBehaviour
{
    [SerializeField]
    private bool ignoreLayout = false;

    void OnEnable()
    {
        UpdateLayout();
    }

    void Update()
    {
        UpdateLayout();
    }

    void UpdateLayout()
    {
        LayoutGroup layoutGroup = GetComponentInParent<LayoutGroup>();
        if (layoutGroup != null)
        {
            LayoutElement layoutElement = GetComponent<LayoutElement>();
            if (layoutElement == null)
            {
                layoutElement = gameObject.AddComponent<LayoutElement>();
            }

            layoutElement.ignoreLayout = ignoreLayout;
            layoutGroup.enabled = false;
            layoutGroup.enabled = true;
        }
    }
}
