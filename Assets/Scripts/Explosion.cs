using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explodeCenter;
    public GameObject explodeTrackCenter;
    public GameObject explodeTrackEnd;

    public LayerMask levelMask;
    public int BombPower;
    private bool exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Explode());
        Invoke("Explode", 2.9f);
    }

    //IEnumerator Explode()
    //{
        //yield return new WaitForSeconds(2.9f);
    void Explode()
    {
        RaycastHit2D hit;
        Vector2 pos = transform.position;

        GameObject exCenter = Instantiate(explodeCenter, pos, Quaternion.identity);
        Destroy(exCenter, 0.8f);

        for (int i = 1; i <= BombPower; i++)
        {
            hit = Physics2D.Raycast(transform.position, Vector3.right, i, levelMask);
            if (!hit.collider)
            {
                GameObject exTrCent = Instantiate(explodeTrackCenter, new Vector2(pos.x + i, pos.y), Quaternion.Euler(0, 0, 0));
                Destroy(exTrCent, 0.8f);
            }

            hit = Physics2D.Raycast(transform.position, Vector3.left, i, levelMask);
            if (!hit.collider)
            {
                GameObject exTrCent2 = Instantiate(explodeTrackCenter, new Vector2(pos.x - i, pos.y), Quaternion.Euler(0, 0, 180));
                Destroy(exTrCent2, 0.8f);
            }

            hit = Physics2D.Raycast(transform.position, Vector3.up, i, levelMask);
            if (!hit.collider)
            {
                GameObject exTrCent3 = Instantiate(explodeTrackCenter, new Vector2(pos.x, pos.y + i), Quaternion.Euler(0, 0, 90));
                Destroy(exTrCent3, 0.8f);
            }

            hit = Physics2D.Raycast(transform.position, Vector3.down, i, levelMask);
            if (!hit.collider)
            {
                GameObject exTrCent4 = Instantiate(explodeTrackCenter, new Vector2(pos.x, pos.y - i), Quaternion.Euler(0, 0, 270));
                Destroy(exTrCent4, 0.8f);
            }
        }

        hit = Physics2D.Raycast(transform.position, Vector3.right, BombPower + 1, levelMask);
        if (!hit.collider)
        {
            GameObject exTrEnd = Instantiate(explodeTrackEnd, new Vector2(pos.x + BombPower+1, pos.y), Quaternion.Euler(0, 0, 0));
            Destroy(exTrEnd, 0.8f);
        }

        hit = Physics2D.Raycast(transform.position, Vector3.left, BombPower + 1, levelMask);
        if (!hit.collider)
        {
            GameObject exTrEnd2 = Instantiate(explodeTrackEnd, new Vector2(pos.x - BombPower-1, pos.y), Quaternion.Euler(0, 0, 180));
            Destroy(exTrEnd2, 0.8f);
        }

        hit = Physics2D.Raycast(transform.position, Vector3.up, BombPower + 1, levelMask);
        if (!hit.collider)
        {
            GameObject exTrEnd3 = Instantiate(explodeTrackEnd, new Vector2(pos.x, pos.y + BombPower+1), Quaternion.Euler(0, 0, 90));
            Destroy(exTrEnd3, 0.8f);
        }

        hit = Physics2D.Raycast(transform.position, Vector3.down, BombPower + 1, levelMask);
        if (!hit.collider)
        {
            GameObject exTrEnd4 = Instantiate(explodeTrackEnd, new Vector2(pos.x, pos.y - BombPower-1), Quaternion.Euler(0, 0, 270));
            Destroy(exTrEnd4, 0.8f);
        }

        gameObject.SetActive(false); //Deactivate bomb
        exploded = true;
        Destroy(gameObject, .3f); //Destroy the actual bomb in 0.3 seconds, after all coroutines have finished
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((!exploded) && (other.gameObject.tag == "Explosion"))
        { //If not exploded yet and this bomb is hit by an explosion...
            CancelInvoke("Explode"); //Cancel the already called Explode, else the bomb might explode twice 
            Explode(); //Finally, explode!
        }
    }  

    // Update is called once per frame
    void Update()
    {

    }
}
