using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;  
    
    public float rotateSpeed = 120f;
    
    [Header("Camera Settings")]

    public float cameraSmoothness = 1f;

    [Header("References")]
    public Transform cameraTransform;
    
    private Rigidbody rb;
    private float currentRotationInput;
    private Vector3 initialCameraLocalPosition;
    private Quaternion initialCameraLocalRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; 
        

        cameraTransform = cameraTransform ?? Camera.main?.transform;
        

        if (cameraTransform != null)
        {
            Transform originalParent = cameraTransform.parent;
            cameraTransform.SetParent(transform);
            initialCameraLocalPosition = cameraTransform.localPosition;
            initialCameraLocalRotation = cameraTransform.localRotation;
            cameraTransform.SetParent(originalParent);
        }
    }
    
    void Update()
    {

        currentRotationInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(currentRotationInput) > 0.1f)
        {
            float rotationAmount = currentRotationInput * rotateSpeed * Time.deltaTime;
            transform.Rotate(0, rotationAmount, 0);
        }
    }
    
    void FixedUpdate()
    {

        float verticalInput = Input.GetAxis("Vertical");
        if (Mathf.Abs(verticalInput) > 0.1f)
        {
            Vector3 moveDirection = transform.forward * verticalInput;
            Vector3 targetVelocity = moveDirection * moveSpeed;
            

            rb.velocity = new Vector3(
                targetVelocity.x, 
                rb.velocity.y, 
                targetVelocity.z
            );
        }
        else
        {

            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    void LateUpdate()
    {
        if (cameraTransform == null) return;
        

        Vector3 targetPosition = transform.TransformPoint(initialCameraLocalPosition);
        Quaternion targetRotation = transform.rotation * initialCameraLocalRotation;
        

        cameraTransform.position = Vector3.Lerp(
            cameraTransform.position,
            targetPosition,
            cameraSmoothness * Time.deltaTime
        );
        
        cameraTransform.rotation = Quaternion.Slerp(
            cameraTransform.rotation,
            targetRotation,
            cameraSmoothness * Time.deltaTime
        );
    }
}