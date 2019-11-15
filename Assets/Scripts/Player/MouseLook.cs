using UnityEngine;

namespace RPG.Player
{
    [AddComponentMenu("RPG/Player/Mouse Look")]
    public class MouseLook : MonoBehaviour
    {
        //A Enum is used to seperated and create different variables
        //Used in inventory systems mostly
        public enum RotationalAxis
        {
            MouseX,
            MouseY
        }
        [Header("Rotation Variables")]
        //Mouse X axis is loacted on the player component
        //Created a reference for the X axis
        public RotationalAxis axis = RotationalAxis.MouseX;
        //Restricts the rotation on the X/Y axis
        [Range(0, 60)]
        //Made a variable for the sense
        public float sensitivity = 15;
        //Set the Max and Min rotation
        public float minY = -60, maxY = 60;
        //Made a variable for the rotation for the Y axis
        private float _rotY;

        void Start()
        {
            //For if we wanted to use a Physics based movement system
            //Checks for a rigidbody component
            if (GetComponent<Rigidbody>())
            {
                //Allows the game object to not rotate
                GetComponent<Rigidbody>().freezeRotation = true;
            }
            Cursor.lockState = CursorLockMode.Locked;
            //makes the cursor invisible when playing the game
            Cursor.visible = false;

            //Mouse Y is loacted on the Camera component
            //Checks for a Camera component
            if (GetComponent<Camera>())
            {
                //Created a reference for the Y axis
                axis = RotationalAxis.MouseY;
            }
        }
        void Update()
        {
            if (!PlayerHandler.isDead)
            {
                //If the MouseX function is used.....
                if (axis == RotationalAxis.MouseX)
                {
                    //It makes the player able to move on the X axis
                    transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime, 0);
                }
                else
                {
                    // Allows for rotation on the Y axis
                    _rotY += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
                    //Clamps the rotation on the Y axis
                    _rotY = Mathf.Clamp(_rotY, minY, maxY);
                    //This is the actual function that lets us move on the Y axis
                    transform.localEulerAngles = new Vector3(-_rotY, 0, 0);
                }
            }
        }
    }
}
