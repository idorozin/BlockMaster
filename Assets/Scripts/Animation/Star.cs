using System.Diagnostics;
using Boo.Lang;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

public class Star : MonoBehaviour
{
    private static List<GameObject> stars = new List<GameObject>();
    
    [SerializeField] private float spacing = 1f;

    private Vector3 borders;
    public static Camera camera;
    void Start()
    {
        stars.Add(gameObject);
        if(camera == null)
            camera = Camera.main;
        borders = camera.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0f));
        borders.y = 5f;
        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        stars.Remove(gameObject);
    }

    private float startPos;
    private bool done;
    [SerializeField]
    private bool parrallax = false;
    private void Update()
    {
        if (NotInPlace())
        {
            GeneratePostion();
        }

        float dist = camera.transform.position.y * effect;
        if(parrallax)
            transform.position = new Vector3(transform.position.x , startPos + dist , transform.position.z);
    }

    private void GeneratePostion()
    {
        transform.position = GetRandomPosition();
        float localScale = Random.Range(0.05f, 0.2f);
        transform.localScale = new Vector3(localScale , localScale);		
    }

    private bool first = true;
    [SerializeField]
    private float effect = 0.3f;

    private Vector3 GetRandomPosition()
    {
        Vector3 pos;
        bool neighbours;
        do
        {
            if(!first)
                pos = new Vector3(
                    Random.Range(-borders.x, borders.x),
                    Random.Range(camera.transform.position.y+borders.y, camera.transform.position.y + borders.y + 10f),
                    0f);
            else
                pos = new Vector3(
                    Random.Range(-borders.x, borders.x),
                    Random.Range(camera.transform.position.y+borders.y, camera.transform.position.y + borders.y + borders.y*2),
                    0f);
            neighbours = false;
            foreach (var star in stars)
            {
                neighbours = Vector3.Distance(star.transform.position, pos) < spacing;
                if(neighbours)
                    break;
            }
        } while (neighbours);

        effect = Random.Range(0.1f, 1f);
        startPos = pos.y;
        first = false;
        return pos;
    }
    
    
    private bool NotInPlace()
    {
        if (camera.transform.position.y > transform.position.y+borders.y)
            return true;
        return false;
    }
}