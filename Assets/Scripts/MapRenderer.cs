using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Pipe
{
    Transform _Head;
    Transform _Body;
    public Pipe(Transform Head, Transform Body)
    {
        _Head = Head;
        _Body = Body;
    }
    public void Move(float distance)
    {
        Vector3 newPos = Vector3.left * distance * Time.deltaTime;
        _Head.position += newPos;
        _Body.position += newPos;
    }
    public float getXPos { get => _Head.position.x; }
    public void Destory()
    {
        Object.Destroy(_Head.gameObject);
        Object.Destroy(_Body.gameObject);
        
    }
}

public class MapRenderer : MonoBehaviour
{
    private const float HeadHeight = 5f;
    [SerializeField]
    private Camera Camera;
    private List<Pipe> PipeComponents = new List<Pipe>();
    private void Start()
    {
        newPipe(45, 50f);
        //newPipe(30, 0f);
    }
    private void Update()
    {
        // Pipes Movement
        for (int i=0;i<PipeComponents.Count;i++)
        {
            Pipe pipeComponent = PipeComponents[i];
            pipeComponent.Move(20);
            if (pipeComponent.getXPos < -130f)
            {
                pipeComponent.Destory();
                PipeComponents.Remove(pipeComponent);
            }
        }
            
    }
    private void newPipe(float gapY, float xPos, float size = 25)
    {
        PipeComponents.Add(newRawPipe(gapY - size * .5f, xPos));
        PipeComponents.Add(newRawPipe(Camera.orthographicSize * 2f - gapY - size * .5f, xPos, true));
    }
    private Pipe newRawPipe(float Height, float xPosition, bool isTop = false)
    {
        // Configure Pipe's Head
        Transform Head = Instantiate(AssetManager.instance.HeadPipeSprite);
        float headYPos = -Camera.orthographicSize + Height - HeadHeight * .5f;
        if (isTop)
            headYPos = Camera.orthographicSize - Height + HeadHeight * .5f;
        Head.position = new Vector2(xPosition, headYPos);
        // Configure Pipe's Body
        Transform Body = Instantiate(AssetManager.instance.BodyPipeSprite);
        float bodyYPos = -Camera.orthographicSize;
        if (isTop) {
            bodyYPos = Camera.orthographicSize;
            Body.localScale = new Vector2(1,-1);
        }            
        Body.position = new Vector2(xPosition, bodyYPos);
        SpriteRenderer BodySprite = Body.GetComponent<SpriteRenderer>();
        BodySprite.size = new Vector2(BodySprite.size.x, Height);
        BoxCollider2D BodyCollider = Body.GetComponent<BoxCollider2D>();
        BodyCollider.offset = Vector2.up * Height * .5f;
        BodyCollider.size = new Vector2(BodyCollider.size.x, Height);
        // Add pipes to the Pipes list
        return new Pipe(Head, Body);
    }
}
