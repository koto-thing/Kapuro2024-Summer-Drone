using System.Collections.Generic;
using System.Numerics;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class ObstacleFactryController : MonoBehaviour
{
    [SerializeField] private List<AbstractBird> birdsList;
    [SerializeField] private Canvas obstacleCanvas;
    
    [Header("timer")]
    [SerializeField] private float birdSpawnTimer;
    
    [Header("BirdParameter")]
    [SerializeField] private List<AbstractBird> birds;
    [SerializeField] private float birdSpawnInterval;
    [SerializeField] private int maxBirdObstacles;
    [SerializeField] private int currentBirdObstacles;
    
    public async UniTask Initialize()
    {
        await UniTask.CompletedTask;
    }

    public void ObstacleFactoryUpdate()
    {
        SetSpawnTimer();
        if (birds.Count != 0)
        {
            foreach(var bird in birds)
                bird.BirdUpdate();
        }
    }

    // @brief ランダムな数のランダムな種類の鳥を生成する
    public void SpawnBird()
    {
        int birdNum = Random.Range(0, (maxBirdObstacles - currentBirdObstacles) < 0 ? 0 : maxBirdObstacles - currentBirdObstacles);
        currentBirdObstacles += birdNum;
        
        for (int i = 0; i < birdNum; i++)
        {
            int birdIndex = Random.Range(0, birdsList.Count);
            Vector3 spawnPosition = SetRandomPositionOutSideCamera();
            Quaternion rotation = SetRotationTowardsCenter(spawnPosition);
            AbstractBird newBird = Instantiate(birdsList[birdIndex], spawnPosition, rotation, obstacleCanvas.transform);
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
            newBird.Initialize();
            newBird.SetParameter(direction);
            birds.Add(newBird);
        }
    }
    
    // @brief 鳥を削除
    public void RemoveBird(AbstractBird bird)
    {
        if (birds.Contains(bird))
        {
            birds.Remove(bird);
            currentBirdObstacles--;
        }
    }

    private void SetSpawnTimer()
    {
        if(birdSpawnTimer > birdSpawnInterval)
        {
            SpawnBird();
            birdSpawnTimer = 0;
        }
        else
        {
            birdSpawnTimer += Time.deltaTime;
        }
    }

    // @brief カメラの外側にランダムな位置を設定
    private Vector3 SetRandomPositionOutSideCamera()
    {
        Vector3 spawnPosition = Vector3.zero;
        float cameraHeight = 2f * Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        // ランダムにカメラの外側の位置を決定
        int side = Random.Range(0, 4);
        switch (side)
        {
            case 0: // 上
                spawnPosition = new Vector3(Random.Range(-cameraWidth / 2, cameraWidth / 2), Camera.main.transform.position.y + cameraHeight / 2 + 1, 0);
                break;
            case 1: // 下
                spawnPosition = new Vector3(Random.Range(-cameraWidth / 2, cameraWidth / 2), Camera.main.transform.position.y - cameraHeight / 2 - 1, 0);
                break;
            case 2: // 左
                spawnPosition = new Vector3(Camera.main.transform.position.x - cameraWidth / 2 - 1, Random.Range(-cameraHeight / 2, cameraHeight / 2), 0);
                break;
            case 3: // 右
                spawnPosition = new Vector3(Camera.main.transform.position.x + cameraWidth / 2 + 1, Random.Range(-cameraHeight / 2, cameraHeight / 2), 0);
                break;
        }
        
        return spawnPosition;
    }

    // @brief 中心に向かう回転を取得
    private Quaternion SetRotationTowardsCenter(Vector3 position)
    {
        Vector3 direction = (Camera.main.transform.position - position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ランダムなオフセットを追加
        float randomOffset = Random.Range(-30.0f, 30.0f);
        angle += randomOffset;

        return Quaternion.Euler(new Vector3(0, 0, angle + 180));
    }
}