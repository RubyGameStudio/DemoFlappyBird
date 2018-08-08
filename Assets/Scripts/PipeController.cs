using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{

    private static float pipeHeight = 6.5f;
    private float speed { get { return GameManager.Instance.GameSpeed; } }

    [SerializeField]
    private GameObject topPipe;

    [SerializeField]
    private GameObject bottomPipe;

    public void Setup(float minDistanceBetweenPipes, float maxDistanceBetweenPipes)
    {
        var distance = Random.Range(minDistanceBetweenPipes, maxDistanceBetweenPipes);
        var newPipePlacing = pipeHeight + distance * .5f;

        topPipe.transform.localPosition = new Vector3(0, newPipePlacing, 0);
        bottomPipe.transform.localPosition = new Vector3(0, -newPipePlacing, 0);
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.IsGameOver)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        transform.position += Vector3.left * speed * Time.fixedDeltaTime;

        if (transform.position.x < -5f)
        {
            DestroyImmediate(gameObject);
        }
    }
}
