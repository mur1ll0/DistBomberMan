using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Mirror;


public class movement : NetworkBehaviour
{
    public GameObject Bomba;
    public GameObject animDie;
    private Animator anim;

    public int canDeploy = 1;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    
    }


    //Spawnar bomba
    [Command]
    void DropBomb()
    {
        GameObject newBomb = Instantiate(Bomba,
            new Vector2(
                Mathf.RoundToInt(transform.position.x),
                Mathf.RoundToInt(transform.position.y)
            ),
            Bomba.transform.rotation
        );
    }


    // Update is called once per frame
    [Client]
    void Update()
    {
        if (this.isLocalPlayer)
        {
            anim.SetInteger("Direction", 0);

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector2.left * 3f * Time.deltaTime);
                transform.eulerAngles = new Vector2(0, 180);
                anim.SetInteger("Direction", 1);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector2.left * 3f * Time.deltaTime);
                transform.eulerAngles = new Vector2(0, 0);
                anim.SetInteger("Direction", 1);
            }
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector2.up * 3f * Time.deltaTime);
                anim.SetInteger("Direction", 2);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(Vector2.down * 3f * Time.deltaTime);
                anim.SetInteger("Direction", 3);
            }

            if (Input.GetKeyDown(KeyCode.Space) && canDeploy == 1)
            {

                DropBomb();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Explosion")
        {
            GameObject die = Instantiate(animDie, transform.position, transform.rotation);
            Destroy(die, 0.9f);
            gameObject.SetActive(false);
            Destroy(gameObject, .3f);
        }

        if (other.gameObject.name == "bomb(Clone)")
        {
            canDeploy = 0;
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

}
