using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Скрипт для передвижения персонажа 
 * и анимаций
 * Скрипт предназначен для персонажа в доме
 */

public class Character : MonoBehaviour
{
    [SerializeField]
    private int life = 1;           // объявление количества хп
    [SerializeField]
    private float speed = 3.0F;     // начальная скорость

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
        //до того пока не сделано действий - бездействие
        State = CharacterState.Idle;
        //передает в переменную данные о нажатой кнопке
        float v = Input.GetAxis("Vertical");

        if (Input.GetButton("Horizontal"))  // ЕСЛИ входные данные - кнопки A или D
        {                                   // ТО
            RunLR();                        // Движение влево/вправо
        }
        if (Input.GetButton("Vertical"))    // ЕСЛИ же входные данные - кнопки W или S
        {                                   // ТО
            RunUD(v);                       // Движение вверх/вниз
        }

        if (Input.GetKeyDown(KeyCode.E) && ready)
        {
            SceneManager.LoadScene(Level); ; // устаревший метод
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
        //Получение данных о нажатой кнопке
        //Getting information about the pressed button 
        Vector2 VertDirection = transform.forward * Input.GetAxis("Vertical"); //Vertical = кнопки W, S
        //Движение вверх или вниз взависимости от нажатой кнопки
        //Move up or down depending on the button pressed
        transform.Translate(new Vector2(0, speed * vertical * Time.fixedDeltaTime));
        //Прорисовка анимации
        //Drawing the animation
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) // ЕСЛИ ввод S или кнопка вниз
            &&                                                           // И
            !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))) // НЕ кнопка W или кнопка вверх
        {                                                                // ТО
            State = CharacterState.Movement_down;                        // Анимация движения вниз
        }
        else                                                             // ИНАЧЕ
        {
            State = CharacterState.Movement_up;                          // Анимация движения вверх
        }
        #region
        //Отменяем анимацию
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