using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Transform gunPointImitation;
    [SerializeField] Camera cam;
    InputSystem_Actions action;

    void OnEnable()
    {
        action = InputManager.Instance.inputSystemActions;
        action.Player.Attack.performed += Shoot;
    }

    void Shoot(InputAction.CallbackContext context)
    {
        AudioManager.Instance.PlayActionAudio(BulletManager.Instance.ammoType);

        var ammunition = BulletManager.Instance.GetCurrentPrefab();

        ammunition.transform.position = gunPointImitation.position;
        ammunition.transform.rotation = gunPointImitation.rotation;
        Vector3 directionOfShoot = gunPointImitation.forward * BulletManager.Instance.ammoForcePower;

        ammunition.SetActive(true);
        Rigidbody rb = ammunition.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(directionOfShoot, ForceMode.Impulse);
    }

    void OnDisable()
    {
        action.Player.Attack.performed -= Shoot;
    }
}