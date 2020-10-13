using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTransitionTest : MonoBehaviour
{

    public float scale;

    // Start is called before the first frame update
    public void StartEnlarge(float speedRate = 1f)
    {
        this.StartCoroutine(Enlarge(speedRate));
    }
    
    private IEnumerator Enlarge(float speedFactor)
    {
        float remainingDist = (this.gameObject.transform.localScale - new Vector3(1, 1, 1)).sqrMagnitude;
        gameObject.transform.localScale = new Vector3(.1f,.1f,.1f);
        Debug.Log(remainingDist);

        while (this.transform.localScale.x < 1)
        {
            gameObject.transform.localScale += new Vector3(speedFactor * Time.deltaTime, speedFactor * Time.deltaTime, speedFactor * Time.deltaTime);
            yield return null;

        }


        //gameObject.transform.localScale = new Vector3(1, 1, 1);
    }
}
