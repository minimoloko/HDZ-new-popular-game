using UnityEngine;
using System.Collections;

public class AnimationHandler : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private string idleState = "Pavel_Idle";

    void Awake() => anim = GetComponent<Animator>();

    public void PlaySpecial(string animName)
    {
        StopAllCoroutines();
        StartCoroutine(Routine(animName));
    }

    IEnumerator Routine(string name)
    {
        anim.Play(name);

        yield return null; // wait frame

        float duration = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(duration);

        anim.Play(idleState);
    }


    public void PlaySpecialAndChangeIdleSingle(string data)
    {
        string[] parts = data.Split('|');
        if (parts.Length == 2)
        {
            PlaySpecialAndChangeIdle(parts[0], parts[1]);
        } // tak nazivaemiy kostil'. Pishi  po tipu Pasha_Idle|Pahsa_Run
    }

    public void PlaySpecialAndChangeIdle(string animName, string  newIdle)
    {
        StopAllCoroutines();
        StartCoroutine(Routine2(animName, newIdle));
    }

    IEnumerator Routine2(string animName, string newIdle)
    {
        anim.Play(animName);

        while (!anim.GetCurrentAnimatorStateInfo(0).IsName(animName))
        {
            yield return null;
        }
        float duration = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(duration);

        idleState = newIdle;
        anim.Play(idleState);
    }

}
