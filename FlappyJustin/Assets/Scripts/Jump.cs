using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerInfo))]
public class Jump : MonoBehaviour {

  PlayerInfo playerInfo;

  public Rigidbody2D _rigidbody;
  public float jumpForce = 1.0f;

  // Use this for initialization
  void Start () {
    playerInfo = GetComponent<PlayerInfo>();
  }
  
  // Update is called once per frame
  void Update () {
    if(playerInfo.isAlive == true) {
      if(Input.GetKeyDown(KeyCode.Space)) {
        // We zero out the existing velocity to avoid decreasing the amount of upward force we're experiencing.
        _rigidbody.velocity = Vector2.zero;

        // We divide the amount of force by the amount of time that has passed since the last frame to generate an impulse.
        _rigidbody.AddForce(new Vector2(0.0f, jumpForce / Time.fixedDeltaTime));
      }
    }
  }
}
