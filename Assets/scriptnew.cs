using UnityEngine;

public class scriptnew : MonoBehaviour
{
    [SerializeField] Transform capsule;

    void Start()
    {
        // ������ ����������� �� �������� ������� � capsule
        var vectorToCube = capsule.position - transform.position;

        // ���� � �������� ������������ ��� z
        var angleInRad = Mathf.Atan2(vectorToCube.x, vectorToCube.z);

        // ��������� ���� � �������
        var angleInDegree = Mathf.Rad2Deg * angleInRad;

        // ������������ ������ ������ ��� Y
        transform.rotation = Quaternion.Euler(0, angleInDegree, 0);
    }
}
