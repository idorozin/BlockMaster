using System;
using UnityEditor;
using UnityEngine;

public class HeightFinder : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d;
    [SerializeField]
    private GameObject surface;
    [SerializeField] 
    private Transform camera;

    public static event OnHeightChanged ScoreChanged = delegate {};
    public delegate void OnHeightChanged(float height);



    void Update()
    {
        rb2d.velocity = new Vector2(0, -4);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
        if(col.gameObject.name != surface.name && rb !=null && rb.constraints != RigidbodyConstraints2D.FreezeAll&&rb.velocity.x<0.1 && rb.velocity.y<0.1 && rb.velocity.x>-0.1 && rb.velocity.y>-0.1 && col.isTrigger==false)
        {
            if(transform.position.y > 0)
                ScoreChanged(transform.position.y - 0.905f);
        }
        rb2d.position = new Vector3(camera.position.x,camera.position.y+4f,camera.position.z);  
    }

    public static bool IsNotMoving(Rigidbody2D rb)
    {
        return rb == null ||
               rb.constraints == RigidbodyConstraints2D.FreezeAll || 
               (rb.velocity.x < 0.05 && rb.velocity.y < 0.05 &&
               rb.velocity.x > -0.05 && rb.velocity.y > -0.05 && 
                rb.velocity.magnitude < 0.05 &&
                !rb.gameObject.GetComponent<Collider2D>().isTrigger);
    }
}
