using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject Bomba;
    public GameObject Player;

    public int canDeploy = 1;
    public float explodeTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("bomber");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDeploy == 1)
        {

            GameObject newBomb = Instantiate(Bomba,
                new Vector2(
                    Mathf.RoundToInt(Player.transform.position.x),
                    Mathf.RoundToInt(Player.transform.position.y)
                ),
                Bomba.transform.rotation
            );

            Destroy(newBomb, explodeTime);
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
