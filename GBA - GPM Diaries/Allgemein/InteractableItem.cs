using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class InteractableItem : MonoBehaviour
{
    [SerializeField] Animator itemAnimator;
    public bool repeatAnimation;
    [SerializeField] GameObject pulsatingPoint;

    private bool isAnimationPlaying = false;

    void Start()
    {
    }

    public void PlayAnimation(string animationName)
    {
        if (!isAnimationPlaying)
        {
            pulsatingPoint.SetActive(false);

            if (repeatAnimation)
            {
                itemAnimator.Play(animationName, 0, 0);
            }
            else
            {
                itemAnimator.Play(animationName);
            }

            StartCoroutine(WaitForAnimation());
        }
    }

    IEnumerator WaitForAnimation()
    {
        // Wait for the length of the animation
        yield return new WaitForSeconds(itemAnimator.GetCurrentAnimatorStateInfo(0).length);

        if (repeatAnimation)
        {
            // Add a slight delay before setting pulsatingPoint to true
            yield return new WaitForSeconds(0.5f);
            pulsatingPoint.SetActive(true);
        }

        isAnimationPlaying = false;
    }
}
