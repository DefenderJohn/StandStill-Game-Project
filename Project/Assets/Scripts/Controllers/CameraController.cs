using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float ratio = 0.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkKeyBoardInput();
    }

    private void checkKeyBoardInput() {
        if (Input.GetKey(KeyCode.A))
            this.transform.Rotate(Vector3.down * ratio);
        if (Input.GetKey(KeyCode.D))
            this.transform.Rotate(Vector3.up * ratio);
        if (Input.GetKey(KeyCode.W))
            this.transform.position += this.transform.forward * ratio;
        if (Input.GetKey(KeyCode.S))
            this.transform.position += this.transform.forward * -ratio;
        if (Input.GetKey(KeyCode.LeftShift))
            this.transform.position += this.transform.up * ratio;
        if (Input.GetKey(KeyCode.LeftControl))
            this.transform.position += this.transform.up * -ratio;
    }


}
