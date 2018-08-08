using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private new Rigidbody2D rigidbody { get { return _rigidbody ?? (_rigidbody = GetComponent<Rigidbody2D>()); } }

    [SerializeField]
    private float maxSpeedY = 1f, minSpeedY = -3f;

    [SerializeField]
    private float maxRot = 60, minRot = -60;

    [SerializeField]
    private float jumpPower = 10f;

    private bool isGameStarted { get { return GameManager.Instance.IsGameStarted; } }
    private bool isGameOver { get { return GameManager.Instance.IsGameOver; } }

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("BirdColor", Random.Range(0, 3));
        animator.SetTrigger("SceneStart");
    }

    public void OnGameStart()
    {
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        rigidbody.velocity = Vector2.up * jumpPower;
        animator.SetTrigger("Flap");
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStarted && !isGameOver)
        {
            HandleControls();
            HandleRotation();
        }
    }

    private void HandleRotation()
    {
        var veloY = rigidbody.velocity.y;
        var angleT = Mathf.InverseLerp(minSpeedY, maxSpeedY, veloY);
        var angle = Mathf.Lerp(minRot, maxRot, angleT);

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void HandleControls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rigidbody.velocity = Vector2.up * jumpPower;
            animator.SetTrigger("Flap");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pipe" || collision.tag == "Floor")
        {
            OnObstacleHit();
        }
        else if (collision.tag == "PipePoint")
        {
            GameManager.Instance.ScoreChange();
        }
    }

    private void OnObstacleHit()
    {
        if (!isGameOver)
        {
            GameManager.Instance.OnGameOver();
            rigidbody.velocity = Vector2.zero;
        }
    }
}
