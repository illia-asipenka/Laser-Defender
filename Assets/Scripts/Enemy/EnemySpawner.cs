using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float speed = 1f;
    public float padding = 1f;
    private float xMin;
    private float xMax;
    private int direction = 1;
    public float spawnDelay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        //Spawn();
        SpawnUntilFull();

        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 RightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xMin = leftMost.x;
        xMax = RightMost.x;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += Vector3.right * Time.deltaTime * speed * direction;

        //if enemies reached the bounds they change direction
        if (transform.position.x - (0.5f*width) <= xMin)
        {
            transform.position = new Vector3(xMin + 0.5f * width, transform.position.y, transform.position.x);
            direction = -direction;
            Debug.Log("edge point achieved");
        }
        else if(transform.position.x + (0.5f * width) >= xMax)
        {
            transform.position = new Vector3(xMax - 0.5f * width, transform.position.y, transform.position.x);
            direction = -direction;
            Debug.Log("edge point achieved");
        }

        if(AllMembersAreDead())
        {
            Debug.Log("Empty Formation");
            SpawnUntilFull();
        }

        //float newX = Mathf.Clamp(transform.position.x, xMin, xMax);

        //transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    bool AllMembersAreDead()
    {
        foreach(Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }

    Transform NextFreePosition()
    {
        foreach (Transform childPos in transform)
        {
            if (childPos.childCount == 0)
            {
                return childPos;
            }
        }
        return null;
    }

    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if(freePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if(NextFreePosition())
        {
            Invoke("SpawnUntilFull", spawnDelay);
        }
        
    }

    void Spawn()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }
}
