using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCtrl : MonoBehaviour
{
    #region Camera
        [SerializeField] private Vector2 screenBounds;
        [SerializeField] private float xclampOffset = 1.5f;
        [SerializeField] private float yclampOffset = 1.0f;
    #endregion
    
    #region Movement
        [SerializeField] private float sideSpeed = 0.1f;
        [SerializeField] private float forwardSpeed = 0.1f;
        [SerializeField] private float upSpeed = 0.1f;
        [SerializeField] private float H = 0f;
        [SerializeField] private float V = 0f;
    #endregion

    #region Pitch, Yaw, Roll
        [SerializeField] private float pitchFactor = -50f;
        [SerializeField] private float rollFactor = -70f;
    #endregion

    #region Super Roll
        [SerializeField] private bool isSuperRolling = false;
        [SerializeField] private bool isRollingStopped = true;
        [SerializeField] private float superRollSpeed = 10f;
        [SerializeField] private float superRollLimit = 100f;
        [SerializeField] private float superRollTime = 1f;
        [SerializeField] private bool isInRight = false;
        [SerializeField] private Camera cam;
    #endregion

    void Start() {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        cam = Camera.main;
        if(!cam) Debug.LogError("Camera not found!!");;
    }

    void Update() {
        Move();
        Rotate();

        if(Input.GetKey(KeyCode.LeftShift)) StartCoroutine(SetSuperRoll());

        isInRight = (cam.WorldToScreenPoint(this.transform.position).x > Screen.width / 2) ? true : false;
    }
    private void FixedUpdate() {
        
    }

    void Move() {
        if(!isSuperRolling) {
            H = Input.GetAxis("Horizontal") * sideSpeed;
            V = Input.GetAxis("Vertical") * upSpeed;
        }

        Vector3 clampedPos = new Vector3(transform.position.x + H, transform.position.y + V, transform.position.z + forwardSpeed);
        clampedPos.x = Mathf.Clamp(clampedPos.x, screenBounds.x + xclampOffset, (screenBounds.x * -1) - xclampOffset);
        clampedPos.y = Mathf.Clamp(clampedPos.y, screenBounds.y + yclampOffset, screenBounds.y * -1 - yclampOffset);

        transform.position = clampedPos;
    }

    void Rotate() {
        float pitch = V * pitchFactor * 3.5f;
        float yaw = 0f;
        float roll = H * rollFactor;
        if(!isSuperRolling){

            if(!isRollingStopped) {
                Quaternion newRot = Quaternion.Euler(0, 0, 0);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, newRot, superRollSpeed * Time.deltaTime);
                if(transform.localRotation.z == 0) isRollingStopped = true;
            } else {
                transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
            }

        } else {
            Quaternion newRot = Quaternion.Euler(0, 0, superRollLimit * (isInRight ? -1 : 1));
            transform.localRotation = Quaternion.Lerp(transform.localRotation, newRot, superRollSpeed * Time.deltaTime);
        }
    }

    IEnumerator SetSuperRoll() {
        if(isRollingStopped) isRollingStopped = false;
        if(!isSuperRolling) isSuperRolling = true;
        yield return new WaitForSeconds(superRollTime);
        if(isSuperRolling) isSuperRolling = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("obstacle")) {
            Debug.Log("Game Over");
            // Time.timeScale = 0;
            // Destroy(this.gameObject, 5);
        }
    }
}
