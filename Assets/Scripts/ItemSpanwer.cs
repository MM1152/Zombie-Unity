using UnityEngine;
using UnityEngine.AI;

public class ItemSpanwer : MonoBehaviour
{
    public Item[] items;
    public GameManager gm;
    private float lastSpawnTime;
    private float spawnCoolTime = 5f;

    private void Update()
    {
        if (gm.IsGameOver) return;

        SpawnItem();
    }

    private void SpawnItem()
    {
        if(lastSpawnTime + spawnCoolTime < Time.time)
        {
            lastSpawnTime = Time.time;
            var item = Instantiate(items[Random.Range(0, items.Length)]);

            Vector2 rand = Random.insideUnitCircle * 10f;
            Vector3 point = new Vector3(rand.x, 0f, rand.y);
            NavMeshHit hit;

            if (NavMesh.SamplePosition(point, out hit, 1f, NavMesh.AllAreas))
            {
                item.transform.position = hit.position;
            }
            else
            {
                Destroy(item);
            }
        }
    }
}
