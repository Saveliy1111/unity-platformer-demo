using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class JumpPadTests
{
    private GameObject _jumpPadDummy;
    private GameObject _playerDummy;

    [SetUp]
    public void Setup()
    {
        _jumpPadDummy = new GameObject("JumpPad");
        _jumpPadDummy.AddComponent<JumpPad>();
        
        BoxCollider2D padCollider = _jumpPadDummy.AddComponent<BoxCollider2D>();
        padCollider.isTrigger = true;
        
        _jumpPadDummy.transform.position = Vector3.zero;

        _playerDummy = new GameObject("TestPlayer");
        _playerDummy.tag = Constants.PLAYER_TAG;
        
        Rigidbody2D playerRb = _playerDummy.AddComponent<Rigidbody2D>();
        playerRb.gravityScale = 0f;
        
        BoxCollider2D playerCollider = _playerDummy.AddComponent<BoxCollider2D>();
        
        _playerDummy.transform.position = Vector3.zero; 
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(_jumpPadDummy);
        Object.Destroy(_playerDummy);
    }

    [UnityTest]
    public IEnumerator JumpPad_WhenPlayerEnters_ChangesPlayerYPosition()
    {
        float initialY = _playerDummy.transform.position.y;

        yield return new WaitForSeconds(1f);

        float newY = _playerDummy.transform.position.y;

        Assert.Greater(newY, initialY, "Y position didn't go up after contact with the jump pad!");
    }
}