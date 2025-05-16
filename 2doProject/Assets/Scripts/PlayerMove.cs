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

            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveInput = moveInput * sprintSpeed;
            }
            else
            {
                moveInput = moveInput * moveSpeed;
            }

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

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, currentLean);
            cameraTransform.rotation = Quaternion.Euler(cameraTransform.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));

            if (Input.GetMouseButtonDown(0) && activeGun.fireCounter <= 0)
            {
                RaycastHit hit;

                if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 200f))
                {
                    firePoint.LookAt(hit.point);
                }
                else
                {
                    firePoint.LookAt(cameraTransform.position + (cameraTransform.forward * 40f));
                }

                FireShot();
            }

            if (Input.GetMouseButton(0) && activeGun.canAutoFire)
            {
                if (activeGun.fireCounter <= 0)
                {
                    RaycastHit hit;

                    if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 200f))
                    {
                        firePoint.LookAt(hit.point);
                    }
                    else
                    {
                        firePoint.LookAt(cameraTransform.position + (cameraTransform.forward * 40f));
                    }

                    FireShot();
                }
            }

            //if (Input.GetKeyDown(KeyCode.R))
            //{
            //    SwitchGun();
            //}

            animator.SetFloat("moveSpeed", moveInput.magnitude);
            animator.SetBool("onGround", canJump);
        }
    }

    public void FireShot()
    {
        if (activeGun.currentAmmunition > 0)
        {
            activeGun.ConsumeAmmo();

            RaycastHit hit;
            Vector3 targetPoint;

            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 200f))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = cameraTransform.position + cameraTransform.forward * 100f;
            }

            Vector3 shootDirection = (targetPoint - firePoint.position).normalized;
            Quaternion bulletRotation = Quaternion.LookRotation(shootDirection);
            Instantiate(activeGun.bullet, firePoint.position, bulletRotation);

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
}

