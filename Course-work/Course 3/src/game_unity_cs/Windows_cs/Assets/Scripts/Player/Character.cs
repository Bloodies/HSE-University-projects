using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* ������ ��� ������������ ��������� 
 * � ��������
 * ������ ������������ ��� ��������� � ����
 */

public class Character : MonoBehaviour
{
    [SerializeField]
    private int life = 1;           // ���������� ���������� ��
    [SerializeField]
    private float speed = 3.0F;     // ��������� ��������

    public int Level;

    new private Rigidbody rigidbody;
    private new Animator animation;
    private SpriteRenderer sprite;
    public bool ready = false;

    private Canvas canv;

    private CharacterState State
    {

        get
        {
            return (CharacterState)animation.GetInteger("State");
        }
        set
        {
            animation.SetInteger("State", (int)value);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EndLevel")
        {
            ready = true;
        }
    }
    private void Start()
    {
        canv = GetComponent<Canvas>();
        rigidbody = GetComponent<Rigidbody>();
        animation = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        //Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        //�� ���� ���� �� ������� �������� - �����������
        State = CharacterState.Idle;
        //�������� � ���������� ������ � ������� ������
        float v = Input.GetAxis("Vertical");

        if (Input.GetButton("Horizontal"))  // ���� ������� ������ - ������ A ��� D
        {                                   // ��
            RunLR();                        // �������� �����/������
        }
        if (Input.GetButton("Vertical"))    // ���� �� ������� ������ - ������ W ��� S
        {                                   // ��
            RunUD(v);                       // �������� �����/����
        }

        if (Input.GetKeyDown(KeyCode.E) && ready)
        {
            SceneManager.LoadScene(Level); ; // ���������� �����
        }
    }
    private void RunLR()
    {
        Vector3 HorizontDirection = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + HorizontDirection, speed * Time.deltaTime);

        sprite.flipX = HorizontDirection.x < 0.0F;

        State = CharacterState.Movement_right;
    }
    private void RunUD(float vertical)
    {
        //��������� ������ � ������� ������
        //Getting information about the pressed button 
        Vector2 VertDirection = transform.forward * Input.GetAxis("Vertical"); //Vertical = ������ W, S
        //�������� ����� ��� ���� ������������ �� ������� ������
        //Move up or down depending on the button pressed
        transform.Translate(new Vector2(0, speed * vertical * Time.fixedDeltaTime));
        //���������� ��������
        //Drawing the animation
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) // ���� ���� S ��� ������ ����
            &&                                                           // �
            !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))) // �� ������ W ��� ������ �����
        {                                                                // ��
            State = CharacterState.Movement_down;                        // �������� �������� ����
        }
        else                                                             // �����
        {
            State = CharacterState.Movement_up;                          // �������� �������� �����
        }
        #region
        //�������� ��������
        //Undo animation
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            &&
            !(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            State = CharacterState.Movement_up;
        }
        else
        {
            State = CharacterState.Movement_down;
        }
        #endregion
    }
}

public enum CharacterState
{
    Idle,
    Movement_right,
    Movement_left,
    Movement_up,
    Movement_down
}