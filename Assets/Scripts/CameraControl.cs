/*  Resources
 *  https://docs.unity3d.com/ScriptReference/Input.GetAxis.html
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
  public float speed = 5.0f;
  public float horizontalSpeed = 2.5f;
  public float verticalSpeed = 2.5f;
  // public GameObject terrainObject;

  // private float gap = 15f;

  // Start is called before the first frame update
  void Start()
  {
      Application.targetFrameRate = 300;
  }

  // Update is called once per frame
  void Update()
  {
    // W, A, S, D to control directional movement [translate]
    float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
    float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

    transform.Translate(x, 0.0f, z);

    // // Q, E keys to control roll movement [rotate]
    // transform.Rotate(0.0f, 0.0f, Input.GetAxis("Roll"));

    // Mouse movement to control pitch and yaw
    float h = horizontalSpeed * Input.GetAxis("Mouse X");
    float v = verticalSpeed * Input.GetAxis("Mouse Y");

    transform.Rotate(v, h, 0);

    // Bound();
  }

  // private void Bound() {
  //   TerrainDS terrain = terrainObject.GetComponent<TerrainDS>();
  //
  //   float h = terrain.Get(transform.position.x, transform.position.z) + gap;
  //   Vector3 newPos = new Vector3(transform.positionx, transform.position.y, transform.position.z);
  //   if (transform.position.y < h) {
  //     newPos.y = h;
  //   }
  //
  //   if (newPos < gap) {
  //     newPos.x = gap;
  //   }
  //   if (newPos.z < gap) {
  //     newPos.z = gap;
  //   }
  //   if (newPos.x > terrain.Size - gap) {
  //       newPos.x = terrain.Size - gap;
  //   }
  //   if (newPos.z > terrain.Size - gap) {
  //       newPos.z = terrain.Size - gap;
  //   }
  //   transform.position = newPos;
  // }
}
