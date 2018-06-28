using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mowerController : MonoBehaviour
{

    private Rigidbody rb;
    public float speed;
    private int count;
    private float countPercent;
    private float grassAmount;
    private float grassRemaining;
    public Slider slider;
    
    public Text countText;
    public Text winText;
    public Text countPercentText;
    public float boostSpeed = 5.0f;
    public float boostCooldown = 2.0f;
    public float motorSpeed = 5.0f;
    private float lastBoost;


    //newMovement experimental variables
    private float gasInput;
    private float turnInput;
    public float turnSpeed = 5f;
    public float nuSpeed = 25f;

    //3rdmove varis
    private float angularSpeed;


    void Start()
    {
        lastBoost = Time.time - boostCooldown;
        rb = GetComponent<Rigidbody>();
        
        // count = 0;

        grassAmount = GameObject.FindGameObjectsWithTag("GrassCube").Length;
       // SetcountText();
        winText.text = "";
        
    }

    private void Update()
    {
        newMovementInput();
        //thirdwaveInput();

    }

    //physics in fixed
    private void FixedUpdate()
    {
        newMovement();
        //thirdwaveMovement();
        //getMovement();
        //print(Input.GetAxis("Horizontal"));

        //TODOshould make a cool function for normal and jump movements
        //horiz is manipulated on the normal x plane, which will be used to simulate normalmovement for the player
        //horizonalMovement=  transform.Translate(1f * Time.deltaTime, 0f, 0f);
        //vertJump movement is manipulating the y plane, which will be used to simulate a jump movement for the player
        //vertJumpMovement = transform.Translate(0f, 1f * Time.deltaTime, 0f);

        //-goodcode (un comment bottom two lines)
        //float moveHoriz = Input.GetAxis("Horizontal");
        //float moveVert = Input.GetAxis("Vertical");


        //This successfull moves cube! (next q is why no vector3 or rigibody and will the lack of a rigidbody have collision issues?
        //transform.Translate(speed*moveHoriz * Time.deltaTime, 0f, speed*moveVert * Time.deltaTime);

        //-goodcode (un comment bottom two lines)
        //rb.AddForce(Physics.gravity);

        //-goodcode (un comment bottom 3 lines)
        //rb.velocity = transform.forward * motorSpeed;
        // Vector3 movement = new Vector3(speed*moveHoriz*Time.deltaTime, 0.0f, speed*moveVert*Time.deltaTime);
        //rb.AddForce(movement * speed);

    }

    void getMovement()
    {
  
        //TODOshould make a cool function for normal and jump movements
        float moveHoriz = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");
        rb.AddForce(Physics.gravity);
        rb.velocity = transform.forward * motorSpeed;

        Vector3 movement = new Vector3(speed * moveHoriz * Time.deltaTime, 0.0f, speed * moveVert * Time.deltaTime);
        rb.AddForce(movement * speed);

    }

    void newMovementInput()
    {
        gasInput = 1;
        //gasInput = Input.GetAxis("Vertical");
        //gasInput = transform.forward * nuSpeed;
        turnInput = Input.GetAxis("Horizontal");
        

        //turnInput = Input.
        //rb.AddForce(Physics.gravity);
    }

    void newMovement()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            
           // if (hit.collider.gameObject.tag == "levelPlane")
             //{
            //rb.AddForce(Physics.gravity);
            // }
            

        //  rb.AddForce(Physics.gravity, ForceMode.Acceleration);
        }
        //rb.AddForce(Physics.gravity);
        Debug.Log(turnInput);
        //rb.AddForce(Physics.gravity, ForceMode.Acceleration);


        //this all works almost perfect
       
        rb.velocity = transform.forward * motorSpeed;
        rb.AddRelativeForce(0f, 0f, gasInput * nuSpeed);
        rb.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);

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


        //countText.text = "testCount: " + count.ToString();
        //countPercent = 0;
        grassRemaining = grassAmount - count;
        //countPercent = grassRemaining / grassAmount;
        //countPercent = (count / grassAmount)*100;
        //countPercentText.text = "grass%: " + countPercent.ToString();
        //while (grassAmount != 0)
        //{
        //    countPercent = (count / grassAmount)*100;
        //}

        float progress = Mathf.Clamp01(count / grassAmount);
        //Debug.Log(progress);

        slider.value = progress;


        countText.text = count.ToString() + " / " + grassAmount.ToString() + " ";
        //countPercent = countPercent * 100;
        //countPercentText.text = count.ToString() + " / " + grassAmount.ToString();
        countPercentText.text = " ";
        //182 is current ammount of grasscubes in test environment 'baseLevel' , eventually need a cool grassCount function to count and pass in so it works for any level
       
       // print("currentGrass"+grassAmount.ToString());
        //print("currentCount"+count.ToString());
        if (count >= grassAmount)
        {
            winText.text = "YOU WIN!";
        }
      
    }
    void thirdwaveInput()
    {
        //need to place in normal update
        Vector3 moveVector = Vector3.zero;

        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.y = Input.GetAxis("Vertical");

        float angle = Vector3.Angle(moveVector, Vector3.forward);
        Vector3 cross = Vector3.Cross(moveVector, Vector3.forward).normalized;
        angularSpeed = angle;
        //i believe cross is AngulkarSpeed which is needed by the thirdwavemeovment Quaternion function below
    }
    void thirdwaveMovement()
    {
        Quaternion deltaAngle =
Quaternion.AngleAxis(Mathf.Rad2Deg * angularSpeed*Time.deltaTime, transform.forward);
        //transform.Rotate(deltaAngle.eulerAngles);

        Vector3 v = rb.velocity;
        v = deltaAngle * v;
        rb.velocity = v;

    }

}
