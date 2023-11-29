using UnityEngine;

public class WalkerAnimation : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator.Play(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, 0, Random.Range(0f, 1f));
    }
}