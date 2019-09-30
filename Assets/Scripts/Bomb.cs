using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Mirror;

public class Bomb : NetworkBehaviour
{
    public GameObject Bomba;

    public int canDeploy = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space) && canDeploy == 1)
            {

                GameObject newBomb = Instantiate(Bomba,
                    new Vector2(
                        Mathf.RoundToInt(transform.position.x),
                        Mathf.RoundToInt(transform.position.y)
                    ),
                    Bomba.transform.rotation
                );
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "bomb(Clone)")
        {
            canDeploy = 1;
            other.isTrigger = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "bomb(Clone)")
        {
            canDeploy = 0;
        }
    }

}
