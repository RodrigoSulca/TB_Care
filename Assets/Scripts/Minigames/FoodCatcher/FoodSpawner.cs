using System.Collections;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public float sizeX;
    public float sizeY;
    public float spawnInterval;
    public GameObject[] foods;
    public BacteriumGController bacteriumGController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnFood());
    }

    void Update()
    {
        if(bacteriumGController.timerSlider.value <= 0.5)
        {
            spawnInterval = 0.8f;
        }
    }
    private IEnumerator SpawnFood()
    {
        int num = Random.Range(0,foods.Length);

        float x = Random.Range(-sizeX / 2f, sizeX / 2f);
        float y = Random.Range(-sizeY / 2f, sizeY / 2f);

        Vector3 spawnPos = transform.position + new Vector3(x, y, 0);
        Instantiate(foods[num], spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(spawnInterval);
        StartCoroutine(SpawnFood());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(sizeX, sizeY, 0));
    }
}
