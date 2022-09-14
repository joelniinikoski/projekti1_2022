using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector2 originalsize = transform.localScale;
        float xy = Random.Range(originalsize.x-1f, originalsize.x+1f);
        transform.localScale = new Vector3(xy, xy, 0);
        Destroy(gameObject, 2f);
    }

}
