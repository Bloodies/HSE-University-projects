using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ������ ��� ������
 * ������ ���� ������
 * � ����� ������� ������� �� ���
 * ������ �� ��������� ������ �������� �� ������������ �������
 * ������� ������������ ��������
 * BoundsMap
 */

[RequireComponent(typeof(Camera))]
public class Camera_script : MonoBehaviour
{
    private enum Mode { Player, Cursor };

    [SerializeField] private Mode face;                // ������ ��������, ������������ "����" ��������� ��� ��������� �������
    [SerializeField] private float smooth = 2.5f;      // ����������� ��� ���������� �� ����������
    [SerializeField] private float offset;             // �������� �������� 
    [SerializeField] private SpriteRenderer boundsMap; // ������, � ������ �������� ����� ������������ ������
    [SerializeField] private bool useBounds = true;    // ������������ ��� ���, ������� ��� ������

    private Transform player;
    private Vector3 min, max, direction;
    private static Camera_script _use;
    private Camera cam;

    public static Camera_script use
    {
        get { return _use; }
    }

    void Awake()
    {
        _use = this;
        cam = GetComponent<Camera>();
        cam.orthographic = true;
        FindPlayer();
        CalculateBounds();
    }

    // �������������, ��� ������������� �� ������� ������
    public void UseCameraBounds(bool value)
    {
        useBounds = value;
    }

    // ����� ���������
    // ��� ������ �� ������� ������, �����: Camera2DFollowTDS.use.FindPlayer();
    public void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player) transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }

    // ���� � �������� ����, ���� �������� ���������� ������
    // ��� �������� "Orthographic Size", �� ������� ������� ����� ������ ������� ��������
    public void CalculateBounds()
    {
        if (boundsMap == null) return;
        Bounds bounds = Camera2DBounds();
        min = bounds.max + boundsMap.bounds.min;
        max = bounds.min + boundsMap.bounds.max;
    }

    Bounds Camera2DBounds()
    {
        float height = cam.orthographicSize * 2;
        return new Bounds(Vector3.zero, new Vector3(height * cam.aspect, height, 0));
    }

    Vector3 MoveInside(Vector3 current, Vector3 pMin, Vector3 pMax)
    {
        if (!useBounds || boundsMap == null) return current;
        current = Vector3.Max(current, pMin);
        current = Vector3.Min(current, pMax);
        return current;
    }

    Vector3 Mouse()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = -transform.position.z;
        return cam.ScreenToWorldPoint(mouse);
    }

    //������������ ������ �������� ������������ ������
    void Follow()
    {
        if (face == Mode.Player) direction = player.right; else direction = (Mouse() - player.position).normalized;
        Vector3 position = player.position + direction * offset;
        position.z = transform.position.z;
        position = MoveInside(position, new Vector3(min.x, min.y, position.z), new Vector3(max.x, max.y, position.z));
        transform.position = Vector3.Lerp(transform.position, position, smooth * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (player)
        {
            Follow();
        }
    }
}