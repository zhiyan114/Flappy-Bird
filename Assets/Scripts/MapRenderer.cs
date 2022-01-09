using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRenderer : MonoBehaviour
{
    private const float HeadHeight = 4.8f;
    private void Start()
    {
        newPipe(10, 0f);
    }
    private void newPipe(float Height, float xPosition)
    {
        // Configure Pipe's Head
        Transform Head = Instantiate(AssetManager.instance.HeadPipeSprite);
        Head.position = new Vector2(xPosition, Height - HeadHeight * .5f);
        // Cibfugyre Pipe's Body
        Transform Body = Instantiate(AssetManager.instance.BodyPipeSprite);
        Body.position = new Vector2(xPosition, 0f);
        SpriteRenderer BodySprite = Body.GetComponent<SpriteRenderer>();
        BodySprite.size = new Vector2(BodySprite.size.x, Height);
        BoxCollider2D BodyCollider = Body.GetComponent<BoxCollider2D>();
        BodyCollider.offset = Vector2.up * Height * .5f;
        BodyCollider.size = new Vector2(BodyCollider.size.x, Height);
    }
}
