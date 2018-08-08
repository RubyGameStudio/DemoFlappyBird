using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeFactory : MonoBehaviour {

    [SerializeField]
    private float maxYPosition = 2f;

    [SerializeField]
    private float minYPosition = -2f;

    [SerializeField]
    private float minDistanceBetweenPipes = 1.5f;
   
    [SerializeField]
    private float maxDistanceBetweenPipes = 3.5f;

    [SerializeField]
    private PipeController pipeTemplate;

    private float timer = 0;
    private int spawnedPipeCount = 0;

    private bool canGenerate;

	void Start () 
    {
        spawnedPipeCount = 0;
        timer = 0;
        canGenerate = false;
	}

    public void StartGeneration()
    {
        SpawnPipe();
        canGenerate = true;
    }

    public void StopGeneration()
    {
        canGenerate = false;
    }

    public void Update()
    {
        if (!canGenerate) return;


        //Add 1 every frame
        timer += Time.deltaTime * 30f *GameManager.Instance.GameSpeed;

        if(timer > 100f)
        {
            SpawnPipe();
            timer = 0f;
        }

    }

    void SpawnPipe()
    {
        var pipe = Instantiate(pipeTemplate, transform);
        pipe.Setup(minDistanceBetweenPipes, maxDistanceBetweenPipes);

        var yPosition = Random.Range(minYPosition, maxYPosition);
        pipe.transform.localPosition = new Vector3(0, yPosition, 0);

        spawnedPipeCount++;
    }

}
