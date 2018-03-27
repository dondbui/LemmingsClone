using Model;
using UnityEngine;
using UnityEngine.Tilemaps;
using WorldObjects;

[RequireComponent(typeof(Collider2D))]
public class CharWalker : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D bc;

    private float walkSpeed = Constants.SPEED_WALKER;

    private bool isDragging = false;

    private Vector3 scratchPos = new Vector3();
    private Vector3 scratchRefPos = new Vector3();
    private Vector3 scratchScale = new Vector3(1, 1, 1);

    // Use this for initialization
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();

        Debug.Log("Character Spawned");
    }
    
    // Update is called once per frame
    public void Update()
    {
        if (isDragging)
        {
            return;
        }

        // If we're falling then dont' walk forward
        if (rb.velocity.y != 0)
        {
            return;
        }

        Tilemap tileMap = Engine.tileMap;
        if (tileMap == null)
        {
            return;
        }

        scratchPos = transform.position;
        scratchPos.x += walkSpeed;

        scratchRefPos = scratchPos;

        // If we're moving to the right
        if (walkSpeed > 0)
        {
            scratchRefPos.x -= bc.size.x / 2;
            
            Vector3Int cellPos = tileMap.WorldToCell(scratchRefPos);

            Vector3Int cellRight = new Vector3Int(cellPos.x + 1, cellPos.y, cellPos.z);
            if (tileMap.HasTile(cellRight))
            {
                SwitchDirection();
            }
        }
        // If we're moving to the left
        else if (walkSpeed < 0)
        {
            scratchRefPos.x += bc.size.x / 2;
            Vector3Int cellPos = tileMap.WorldToCell(scratchRefPos);

            Vector3Int cellLeft = new Vector3Int(cellPos.x - 1, cellPos.y, cellPos.z);
            if (tileMap.HasTile(cellLeft))
            {
                SwitchDirection();
            }
        }
        
        transform.position = scratchPos;

        if (transform.position.y < Constants.DEATH_Y)
        {
            Debug.Log("Character Fell too far!! RIP");
            GameObject.Destroy(this.gameObject);
        }
    }

    public void OnMouseDown()
    {
        //Debug.Log("Character: " + name + " has been clicked");
    }

    public void OnMouseDrag()
    {
        isDragging = true;

        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = this.transform.position.z;

        this.transform.position = newPos;
    }

    public void OnMouseUp()
    {
        isDragging = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDragging)
        {
            return;
        }

        

        GameObject colObj = collision.gameObject;

        Debug.Log("Character Hit: " + colObj.name);
        Debug.Log("Tag: " + colObj.tag);

        // Skip tilemap and only focus on the World Objects
        if (colObj.tag == "TileMap")
        {
            return;
        }

        if (colObj.tag == "WorldObject")
        {
            Debug.Log("Character Hit WorldObject: " + colObj.name);

            if (colObj.name == "goal")
            {
                Debug.Log("Goal Reached");
                GameObject.Destroy(this.gameObject);
                return;
            }

            if (collision.relativeVelocity.y != 0)
            {
                return;
            }

            HandleWorldObjectCollision(colObj);
            
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (isDragging)
        {
            return;
        }
    }

    private void HandleWorldObjectCollision(GameObject colObj)
    {
        BaseWorldObject baseObjComp = colObj.GetComponent<BaseWorldObject>();

        Tagger tagger = baseObjComp.tagger;

        if (tagger.tags.Contains(TagTypes.Obstacle))
        {
            // we hit an obstacle so switch direction
            SwitchDirection();
        }
    }

    private void SwitchDirection()
    {
        walkSpeed *= -1;
        scratchScale.x *= -1;
        transform.localScale = scratchScale;
    }
}
