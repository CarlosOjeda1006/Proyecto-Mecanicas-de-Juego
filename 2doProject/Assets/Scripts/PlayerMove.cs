using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;

    public float moveSpeed, gravityForce, jumpForce, sprintSpeed;
    public CharacterController characterController;

    private Vector3 moveInput;
    public Transform cameraTransform;

    public float mouseSensitivity;

    private bool canJump;
    public Transform groundCheckPoint;
    public LayerMask ground;

    public Animator animator;

    public Transform firePoint;

    public Gun activeGun;

    public List<Gun> allGuns = new List<Gun>();

    public int currentGun;

    public GameObject muzzleFlah;

    public float leanAngle = 15f;
    public float leanSpeed = 5f;
    private float currentLean = 0f;

    private Vector2 recoilRotation = Vector2.zero;
    private Vector2 currentRecoil = Vector2.zero;

    public float recoilReturnSpeed = 5f;
    public float recoilApplySpeed = 15f;

    private float verticalLookRotation = 0f;
    public float minLookAngle = -60f;
    public float maxLookAngle = 60f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);

        UI.instance.ammunitionText.text = "" + activeGun.currentAmmunition;
    }

    void Update()
    {
        if (!UI.instance.pauseScreen.activeInHierarchy)
        {
            float yVelocity = moveInput.y;

            Vector3 verticalMove = transform.forward * Input.GetAxis("Vertical");
            Vector3 horizontalMove = transform.right * Input.GetAxis("Horizontal");

            moveInput = horizontalMove + verticalMove;
            moveInput.Normalize();

            moveInput *= Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

            moveInput.y = yVelocity;
            moveInput.y += Physics.gravity.y * gravityForce * Time.deltaTime;

            if (characterController.isGrounded)
            {
                moveInput.y = Physics.gravity.y * gravityForce * Time.deltaTime;
            }

            canJump = Physics.OverlapSphere(groundCheckPoint.position, 0.2f, ground).Length > 0;

            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                moveInput.y = jumpForce;
            }

            characterController.Move(moveInput * Time.deltaTime);

            Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

            if (Input.GetKey(KeyCode.E))
            {
                currentLean = Mathf.Lerp(currentLean, -leanAngle, Time.deltaTime * leanSpeed);
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                currentLean = Mathf.Lerp(currentLean, leanAngle, Time.deltaTime * leanSpeed);
            }
            else
            {
                currentLean = Mathf.Lerp(currentLean, 0f, Time.deltaTime * leanSpeed);
            }

            transform.Rotate(Vector3.up * mouseInput.x);

            recoilRotation = Vector2.Lerp(recoilRotation, Vector2.zero, recoilReturnSpeed * Time.deltaTime);
            currentRecoil = Vector2.Lerp(currentRecoil, recoilRotation, recoilApplySpeed * Time.deltaTime);

            verticalLookRotation += -mouseInput.y - currentRecoil.x;
            verticalLookRotation = Mathf.Clamp(verticalLookRotation, minLookAngle, maxLookAngle);

            cameraTransform.localRotation = Quaternion.Euler(verticalLookRotation, 0f, currentLean);

            if (Input.GetMouseButtonDown(0) && activeGun.fireCounter <= 0)
            {
                AimAndShoot();
            }

            if (Input.GetMouseButton(0) && activeGun.canAutoFire && activeGun.fireCounter <= 0)
            {
                AimAndShoot();
            }

            animator.SetFloat("moveSpeed", moveInput.magnitude);
            animator.SetBool("onGround", canJump);
        }
    }

    private void AimAndShoot()
    {
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 200f))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = cameraTransform.position + (cameraTransform.forward * 100f);
        }

        firePoint.LookAt(targetPoint);
        FireShot();
    }

    public void FireShot()
    {
        if (activeGun.currentAmmunition > 0)
        {
            activeGun.ConsumeAmmo();

            Vector3 shootDirection = (firePoint.forward).normalized;
            Quaternion bulletRotation = Quaternion.LookRotation(shootDirection);
            Instantiate(activeGun.bullet, firePoint.position, bulletRotation);

            CameraShake.instance.Shake(0.1f, 0.1f);
            ApplyRecoil(0.01f, 0.03f);

            activeGun.fireCounter = activeGun.fireRate;

            StartCoroutine(WaitAndSetActiveFalse());
        }
    }

    IEnumerator WaitAndSetActiveFalse()
    {
        muzzleFlah.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        muzzleFlah.SetActive(false);
    }

    public void SwitchGun()
    {
        activeGun.gameObject.SetActive(false);

        currentGun++;
        if (currentGun >= allGuns.Count)
        {
            currentGun = 0;
        }

        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);

        UI.instance.ammunitionText.text = "" + activeGun.currentAmmunition;
    }

    public void ApplyRecoil(float vertical, float horizontal)
    {
        recoilRotation += new Vector2(vertical, Random.Range(-horizontal, horizontal));
    }
}


