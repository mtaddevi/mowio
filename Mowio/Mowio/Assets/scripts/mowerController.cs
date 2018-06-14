using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mowerController : MonoBehaviour {

    private Rigidbody rb;
    public float speed;
    private int count;
    public Text countText;
    public  Text winText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
       SetcountText();
        winText.text = "";
    }

    //physics in fixed
    private void FixedUpdate()
    {
        //print(Input.GetAxis("Horizontal"));

        //TODOshould make a cool function for normal and jump movements
        //horiz is manipulated on the normal x plane, which will be used to simulate normalmovement for the player
        //horizonalMovement=  transform.Translate(1f * Time.deltaTime, 0f, 0f);
        //vertJump movement is manipulating the y plane, which will be used to simulate a jump movement for the player
        //vertJumpMovement = transform.Translate(0f, 1f * Time.deltaTime, 0f);
        float moveHoriz = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");
        //This successfull moves cube! (next q is why no vector3 or rigibody and will the lack of a rigidbody have collision issues?
        //transform.Translate(speed*moveHoriz * Time.deltaTime, 0f, speed*moveVert * Time.deltaTime);

      

       //Vector3 movement = new Vector3(moveHoriz, 0.0f, moveVert);
        Vector3 movement = new Vector3(speed*moveHoriz*Time.deltaTime, 0.0f, speed*moveVert*Time.deltaTime);

       rb.AddForce(movement * speed);

    }

    void getMovement()
    {
        //TODOshould make a cool function for normal and jump movements
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GrassCube"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetcountText();

        }
    }

    void SetcountText()
    {
        countText.text = "Count: " + count.ToString();
         if (count >= 99)
        {
            winText.text = "YOU WIN!";
        }
    }

}   
