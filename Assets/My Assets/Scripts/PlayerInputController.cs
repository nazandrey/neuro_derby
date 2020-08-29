using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private PlayerHierarchy playerHierarchy;

    private Rigidbody2D rigidbody;

    public float speed;             
    public Text xText;
    public Text yText;
    public Text hDirectionText;
    public Text vDirectionText;

    public MoveEvent MoveEvent { get; private set; }

    private void Awake()
    {
        MoveEvent = new MoveEvent();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        var axisPostfix = playerHierarchy.PlayerNum == 0 ? "" : "2";
        float moveHorizontal = Input.GetAxis("Horizontal" + axisPostfix);
        float moveVertical = Input.GetAxis("Vertical" + axisPostfix);
        
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        
        rigidbody.AddForce(movement * speed);

        MoveEvent.Dispatch(new MoveEventData
        {
            PlayerNum = playerHierarchy.PlayerNum,
            X = transform.position.x,
            Y = transform.position.y,
            HDirection = moveHorizontal,
            VDirection = moveVertical
        });

        xText.text = transform.position.x.ToString();
        yText.text = transform.position.y.ToString();
        hDirectionText.text = moveHorizontal == 0 ? "o" : ((moveHorizontal > 0) ? "→" : "←");
        vDirectionText.text = moveVertical == 0 ? "o" : ((moveVertical > 0) ? "↑" : "↓");
    }
}
