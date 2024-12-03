using UnityEngine;

public class scriptnew : MonoBehaviour
{
    [SerializeField] Transform capsule;

    void Start()
    {
        // Вектор направления от текущего объекта к capsule
        var vectorToCube = capsule.position - transform.position;

        // Угол в радианах относительно оси z
        var angleInRad = Mathf.Atan2(vectorToCube.x, vectorToCube.z);

        // Переводим угол в градусы
        var angleInDegree = Mathf.Rad2Deg * angleInRad;

        // Поворачиваем объект вокруг оси Y
        transform.rotation = Quaternion.Euler(0, angleInDegree, 0);
    }
}
