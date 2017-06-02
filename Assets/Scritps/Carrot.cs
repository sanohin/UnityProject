using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Bomb
{
    public float speed = 5;
    public float secondsAlive = 3.0f;
    void Start()
    {
        StartCoroutine(DelayedDestroy());
    }
    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(secondsAlive);
        Destroy(this.gameObject);
    }
    public void Launch(Vector2 direction)
    {
        Rigidbody2D myBody = this.GetComponent<Rigidbody2D>();
        Vector3 pos = this.transform.position;
        myBody.velocity = direction.normalized * speed;
        if (direction.x >= 0)
        {
            this.transform.position += new Vector3(1, 1, 0);
        }
        else
        {
            this.transform.position += new Vector3(1, -1, 0);
        }
    }
}
