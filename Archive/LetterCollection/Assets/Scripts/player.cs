using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class player : MonoBehaviour
{
    // Start is called before the first frame update

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 20.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    public GameObject t1, t2, t3, t4, t5;
    public Text text1;

    void Start()
    {
      controller = gameObject.AddComponent<CharacterController>();
      t5.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
      groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
          controller.Move(move * Time.deltaTime * playerSpeed);

          if (move != Vector3.zero)
          {
              gameObject.transform.forward = move;
          }

          // Changes the height position of the player..
          if (Input.GetButtonDown("Jump") && groundedPlayer)
          {
              playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
          }

          playerVelocity.y += gravityValue * Time.deltaTime;
          controller.Move(playerVelocity * Time.deltaTime);

    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.name == "Letter1")
          {
              t1.SetActive(false);
              text1.text+= " O";
              check();
          }
        if (col.gameObject.name == "Letter2")
          {
              t2.SetActive(false);
              text1.text+= " W";
              check();
          }
        if (col.gameObject.name == "Letter3")
          {
              t3.SetActive(false);
              text1.text+= " R";
              check();
          }
        if (col.gameObject.name == "Letter4")
          {
              t4.SetActive(false);
              text1.text+= " D";
              check();
          }
    }

    void check(){
      if (t1.activeSelf==false && t2.activeSelf==false && t3.activeSelf==false && t4.activeSelf==false){
        t5.SetActive(true);
      }
    }
}
