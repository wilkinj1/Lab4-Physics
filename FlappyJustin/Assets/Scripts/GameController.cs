using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

  public Canvas gameCanvas;

  public GameObject pipeGroupPrefab;
  public float pipeSpeedPerSecond = 10.0f;

  public PlayerInfo player;
  public Text scoreText;
  
  public Vector2 spawnTimeRange;
  float _remainingTime = 0.0f;

  List<GameObject> _pipeGroups = new List<GameObject>();
  List<GameObject> _pipesToClear = new List<GameObject>();
  
  // Update is called once per frame
  void Update () {
    if(player.isAlive) {

      scoreText.text = "Score: " + player.score;

      _remainingTime -= Time.deltaTime;
      if(_remainingTime <= 0.0f) {
        SpawnPipes();
        CalculateSpawnTime();
      }

      foreach(GameObject pipeGroup in _pipeGroups) {
        RectTransform pipeGroupRTransform = pipeGroup.transform as RectTransform;
        MovePipe(pipeGroupRTransform);
        bool onScreen = CheckIfPipeOnScreen(pipeGroupRTransform);
        if(onScreen == false) {
          _pipesToClear.Add(pipeGroup);
        }
      }

      CleanupExpiredPipes();
    }
  }

  public void CalculateSpawnTime() {
    _remainingTime = Random.RandomRange(spawnTimeRange.x, spawnTimeRange.y);
  }

  public void SpawnPipes() {
    RectTransform gameCanvasRTransform = gameCanvas.transform as RectTransform;

    GameObject newPipeGroup = GameObject.Instantiate(pipeGroupPrefab);
    RectTransform newPipeGroupRTransform = newPipeGroup.transform as RectTransform;
    newPipeGroupRTransform.SetParent(gameCanvas.transform);
    newPipeGroupRTransform.SetAsLastSibling();

    newPipeGroupRTransform.localScale = Vector3.one;

    Vector3 newPosition = Vector3.zero;
    newPosition.x = (gameCanvasRTransform.rect.width * gameCanvasRTransform.localScale.x) + (newPipeGroupRTransform.rect.width * 0.5f);
    newPosition.y = gameCanvasRTransform.localPosition.y;
    newPipeGroupRTransform.position = newPosition;

    _pipeGroups.Add(newPipeGroup);
  }

  public void MovePipe(RectTransform groupTransform) {
    groupTransform.Translate(new Vector3(-pipeSpeedPerSecond * Time.deltaTime, 0.0f, 0.0f));
  }

  public bool CheckIfPipeOnScreen(RectTransform groupTransform) {
    Bounds gameCanvasBounds = new Bounds(gameCanvas.transform.position, new Vector3(gameCanvas.pixelRect.width, gameCanvas.pixelRect.height));
    Bounds pipeGroupBounds = new Bounds(groupTransform.position, new Vector3(groupTransform.rect.width, groupTransform.rect.height));

    return pipeGroupBounds.Intersects(gameCanvasBounds);
  }

  public void CleanupExpiredPipes() {
    if(_pipesToClear.Count > 0) {
      foreach(GameObject pipeGroup in _pipesToClear) {
        _pipeGroups.Remove(pipeGroup);
        GameObject.Destroy(pipeGroup);

      }
      _pipesToClear.Clear();
    }
  }
}
