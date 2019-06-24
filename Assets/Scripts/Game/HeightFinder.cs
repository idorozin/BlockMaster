using System;
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
        rb2d.velocity = new Vector2(0, -2);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
        if(col.gameObject.name != surface.name && rb !=null && rb.velocity.x<0.1 && rb.velocity.y<0.1 && rb.velocity.x>-0.1 && rb.velocity.y>-0.1 && col.isTrigger==false)
        {
            if(transform.position.y>0)
                ScoreChanged(col.transform.position.y);
        }
        rb2d.position = new Vector3(camera.position.x,camera.position.y+5f,camera.position.z);  
    }
}
