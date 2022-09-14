using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisActivateAfter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeadDellay(this.gameObject));
    }
 

    public IEnumerator DeadDellay(GameObject g)
    {
        

        yield return new WaitForSeconds(4);
        Destroy(g);
        Destroy(gameObject);
    }
}
