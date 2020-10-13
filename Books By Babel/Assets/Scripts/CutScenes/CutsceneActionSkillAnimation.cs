using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CutsceneActionSkillAnimation : CutSceneAction
{
    public string uid;
    public string skillEffect;
    public MapCoords spawnPosition, destposition;

    public bool wait;

    public CutsceneActionSkillAnimation(string uid, string skillEffect, MapCoords spawn, MapCoords dest, bool wait = true)
    {
        this.uid = uid;
        this.skillEffect = skillEffect;
        this.spawnPosition = spawn;
        this.destposition = dest;
        this.wait = wait;
    }

    public override CutSceneAction Copy()
    {
        return new CutsceneActionSkillAnimation(uid, skillEffect, spawnPosition, destposition);
    }

    public override IEnumerator ExecuteAction(CutsceneController controller, bool playNextNode = true)
    {

        Skill skill = Globals.campaign.contentLibrary.skillDatabase.GetData(skillEffect);


        yield return controller.StartCoroutine(PlayAnimations(skill, controller, playNextNode));

    }

    IEnumerator PlayAnimations(Skill skill, CutsceneController controller, bool playNext)
    {

        int count = skill.animControllerID.Count;
        int curr = 0;

        foreach (string item in skill.GetAnimControllerID())
        {
            curr++;

            AnimationObject animation = Globals.GenerateAnimationObject(item, spawnPosition, destposition);
            animation.transform.SetParent(controller.spawnPoint);

            controller.skillObjects.Add(animation);

            if(curr == count & playNext)
            {
                animation.AnimationObjectDestroyed += controller.NextNode;
            }

            animation.InitAnimatorObject(item, destposition.X, destposition.Y, false, null);


            SFXController.sfxInstance.ChangeSong(skill.GetSFXKey());

            yield return new WaitForSeconds(0.5f);

        }


    }

}
