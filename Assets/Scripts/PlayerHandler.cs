using ProtoBuf;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[ProtoContract]
public enum GameState
{
    [ProtoEnum]
    WaitToStart,
    [ProtoEnum]
    Playing,
    [ProtoEnum]
    Dead,
}
public class PlayerHandler : MonoBehaviour
{
    private PlayerInput PlrInput;
    private static PlayerHandler instance;
    private GameState _GameState = GameState.WaitToStart;
    public static GameState GameState { get => instance._GameState; set => instance._GameState = value; }
    private Rigidbody2D rb;
    private void Awake()
    {
        PlrInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Jump()
    {
        if(GameState == GameState.Playing)
        {
            SoundHandler.PlaySound(SoundOption.Wing);
            rb.velocity = Vector2.up * 65;
        }
    }
    public void JumpInput(InputAction.CallbackContext cb)
    {
        if (GameState == GameState.WaitToStart)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            GameState = GameState.Playing;
        }
        if(cb.phase == InputActionPhase.Started)
            Jump();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameState != GameState.Playing) return;
        // Death Handler
        if (collision.name.Contains("Pipe"))
        {
            GameState = GameState.Dead;
            StartCoroutine(DeathHandler());
            if (MapRenderer.getScore > SaveManager.Data.HighScore)
                SaveManager.Data.HighScore = MapRenderer.getScore;
            SaveManager.SaveToDisk();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameState != GameState.Playing) return;
        // Death Handler
        if (collision.collider.name.Contains("Ground"))
        {
            GameState = GameState.Dead;
            StartCoroutine(DeathHandler());
            if (MapRenderer.getScore > SaveManager.Data.HighScore)
                SaveManager.Data.HighScore = MapRenderer.getScore;
            SaveManager.SaveToDisk();
        }
    }
    private IEnumerator DeathHandler()
    {
        SoundHandler.PlaySound(SoundOption.Hit);
        yield return new WaitForSeconds(.5f);
        SoundHandler.PlaySound(SoundOption.Die);
        yield return new WaitForSeconds(.3f);
        UIServiceHandler.instance.ShowDeadMenu();

    }

}
