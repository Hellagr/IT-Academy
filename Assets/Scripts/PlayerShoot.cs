using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Transform gunPointImitation;
    [SerializeField] Camera cam;
    Projectile projectile;
    InputSystem_Actions action;

    void OnEnable()
    {
        action = InputManager.Instance.inputSystemActions;
        action.Player.Attack.performed += Shoot;
    }

    void Shoot(InputAction.CallbackContext context)
    {
        AudioManager.Instance.PlayActionAudio(BulletManager.Instance.ammoType);

        Projectile ammunition = BulletManager.Instance.GetCurrentPrefab();

        ammunition.transform.position = gunPointImitation.position;
        ammunition.transform.rotation = gunPointImitation.rotation;
        Vector3 directionOfShoot = gunPointImitation.forward * BulletManager.Instance.ammoForcePower;
        ammunition.gameObject.SetActive(true);

        ammunition.Shoot(directionOfShoot, BulletManager.Instance.ammoForcePower);
    }

    void OnDisable()
    {
        action.Player.Attack.performed -= Shoot;
    }
}