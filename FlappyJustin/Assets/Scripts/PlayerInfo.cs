using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {

  public bool isAlive { get; set; }
  public int score = 0;

  // Use this for initialization
  void Start () {
    isAlive = true;
  }

  void OnTriggerEnter2D(Collider2D trigger) {
    if(trigger.tag == "Pipe") {
      HandlePipeCollision();
    }
    else if(trigger.tag == "Score") {
      HandleScoreCollision();
    }
  }

  public void HandlePipeCollision() {
    isAlive = false;
  }

  public void HandleScoreCollision() {
    score++;
  }
}
