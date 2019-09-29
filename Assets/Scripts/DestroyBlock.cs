using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlock : MonoBehaviour
{
    public GameObject blockExplode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Explosion")
        {
            GameObject bExp = Instantiate(blockExplode, transform.position, transform.rotation);
            Destroy(bExp, 0.9f);
            gameObject.SetActive(false);
            Destroy(gameObject, .3f);
        }
    }

}
