using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorHandler : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer floorSprite;

    private float floorSpeed { get { return -GameManager.Instance.GameSpeed; }}

    [SerializeField]
    private float floorLoop;

    private float floorOffset = 0f;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameStarted && !GameManager.Instance.IsGameOver)
        {
            //TODO transform uzerinden calis.
            floorOffset += Time.deltaTime * floorSpeed;
            floorOffset = Mathf.Repeat(floorOffset, floorLoop);
            floorSprite.transform.localPosition = new Vector3(floorOffset, 0f, 0f);
        }
    }
}
