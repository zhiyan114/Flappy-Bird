using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MapRenderer : MonoBehaviour
{
    private const float HeadHeight = 5f;
    [SerializeField]
    private Camera Camera;
    [SerializeField]
    private Transform HeadPipeSprite;
    [SerializeField]
    private Transform BodyPipeSprite;
    private float Pipe_Add_Pos = 200f;
    private float SpawnHeightLimit = 10f;
    // Pipe Runtime Data
    private List<Pipe> PipeComponents = new List<Pipe>();
    private float pipeSpawnTimer;
    private int SpawnedPipes;
    private int PlayerScore;
    private static MapRenderer instance;
    public static int getScore { get => instance.PlayerScore; }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if(PlayerHandler.GameState == GameState.Playing)
        {
            // Pipes Movement
            for (int i = 0; i < PipeComponents.Count; i++)
            {
                Pipe pipeComponent = PipeComponents[i];
                bool isToTheRightOfBird = pipeComponent.getXPos > 0f;
                pipeComponent.Move(50);
                if (isToTheRightOfBird && pipeComponent.getXPos <= 0f && pipeComponent.isTop)
                {
                    SoundHandler.PlaySound(SoundOption.Point);
                    PlayerScore += 1;
                    UIServiceHandler.instance.ScoreUI = PlayerScore;
                }
                if (pipeComponent.getXPos < -Pipe_Add_Pos)
                {
                    pipeComponent.Destory();
                    PipeComponents.Remove(pipeComponent);
                    i--;
                }
            }
            // Pipe Spawner
            pipeSpawnTimer -= Time.deltaTime;
            if (pipeSpawnTimer < 0)
            {
                pipeSpawnTimer += 2f;
                SpawnedPipes += 1;
                float SpawnSize = CalculateSizeDifficulty();
                float minHeight = SpawnSize / 2f + SpawnHeightLimit;
                float maxHeight = (Camera.orthographicSize * 2f) - (SpawnSize * .5f) - SpawnHeightLimit;
                newPipe(Random.Range(minHeight, maxHeight), Pipe_Add_Pos, SpawnSize);
            }
        }
    }
     private float CalculateSizeDifficulty()
    {
        if (SpawnedPipes >= 100) return 20;
        if (SpawnedPipes >= 50) return 25;
        if (SpawnedPipes >= 40) return 30;
        if (SpawnedPipes >= 30) return 35;
        if (SpawnedPipes >= 20) return 40;
        if (SpawnedPipes >= 10) return 45;
        return 50;
    }
    private void newPipe(float gapY, float xPos, float size = 25)
    {
        newRawPipe(gapY - size * .5f, xPos);
        newRawPipe(Camera.orthographicSize * 2f - gapY - size * .5f, xPos, true);
    }
    private void newRawPipe(float Height, float xPosition, bool isTop = false)
    {
        // Configure Pipe's Head
        Transform Head = Instantiate(HeadPipeSprite);
        float headYPos = -Camera.orthographicSize + Height - HeadHeight * .5f;
        if (isTop)
            headYPos = Camera.orthographicSize - Height + HeadHeight * .5f;
        Head.position = new Vector2(xPosition, headYPos);
        // Configure Pipe's Body
        Transform Body = Instantiate(BodyPipeSprite);
        float bodyYPos = -Camera.orthographicSize;
        if (isTop) {
            bodyYPos = Camera.orthographicSize;
            Body.localScale = new Vector2(1,-1);
        }            
        Body.position = new Vector2(xPosition, bodyYPos);
        SpriteRenderer BodySprite = Body.GetComponent<SpriteRenderer>();
        BodySprite.size = new Vector2(BodySprite.size.x, Height - HeadHeight);
        BoxCollider2D BodyCollider = Body.GetComponent<BoxCollider2D>();
        BodyCollider.offset = Vector2.up * (Height - HeadHeight) * .5f;
        BodyCollider.size = new Vector2(BodyCollider.size.x, Height-HeadHeight);
        // Add pipes to the Pipes list
        PipeComponents.Add(new Pipe(Head, Body, isTop));
    }
    private class Pipe
    {
        Transform _Head;
        Transform _Body;
        public bool isTop { get; set; }
        public Pipe(Transform Head, Transform Body, bool isTopPipe = false)
        {
            _Head = Head;
            _Body = Body;
            isTop = isTopPipe;
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
            Destroy(_Head.gameObject);
            Destroy(_Body.gameObject);
        }
    }
}
