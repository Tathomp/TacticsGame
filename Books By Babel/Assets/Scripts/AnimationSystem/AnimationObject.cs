using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationObject : MonoBehaviour
{
    public Animator anim;
    public int movementSpeed = 10;

    public PlayingAnimationInputState state;
    public bool lastObj;

    public delegate void Destroyed();
    public event Destroyed AnimationObjectDestroyed;

    public bool startAtSource;
    public float waitToDestroy;

    public void InitAnimatorObject(string animationid, int destx, int desty, bool lastObj, PlayingAnimationInputState state)
    {
        //this.transform.position = Globals.GridToWorld(source);

        string path = animationid;

        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AnimationControllers/"+path);


        this.lastObj = lastObj;
        this.state = state;

        if (startAtSource)
        {
            StartCoroutine(SmoothMovement(Globals.GridToWorld(destx, desty)));
        }
        else
        {
            StartCoroutine(PlayOutAnimation());
        }
    }

    // Update is called once per frame
    void Update()
    { }


    private IEnumerator PlayOutAnimation()
    {
        yield return new WaitForSeconds(waitToDestroy);
        GameObject.Destroy(this.gameObject);
    }

    private IEnumerator SmoothMovement(Vector3 target)
    {
        float remainingDist = (transform.position - target).sqrMagnitude;

        while (remainingDist > float.Epsilon)
        {

            transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
            remainingDist = (transform.position - target).sqrMagnitude;
            yield return null;
        }

        GameObject.Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if(lastObj)
        {
            state.ApplyCombatDamage();
        }

        if(AnimationObjectDestroyed != null)
        {
            AnimationObjectDestroyed.Invoke();
            AnimationObjectDestroyed = null;
        }
    }

}
