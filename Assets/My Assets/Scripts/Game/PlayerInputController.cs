using System;
using UnityEngine;
using UnityEngine.UI;

namespace NeuroDerby.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private PlayerHierarchy playerHierarchy;

        private Rigidbody2D _rigidbody;
        private Vector2 _accumulatedInput;

        public float speed;
        public Text xText;
        public Text yText;
        public Text hDirectionText;
        public Text vDirectionText;

        public MoveEvent MoveEvent { get; private set; }

        private void Awake()
        {
            MoveEvent = new MoveEvent();
            _rigidbody = GetComponent<Rigidbody2D>();
            _accumulatedInput = Vector2.zero;
        }

        private void Update()
        {
            var axisPostfix = playerHierarchy.PlayerNum == 0 ? "" : "2";
            var moveHorizontal = Input.GetAxis("Horizontal" + axisPostfix);
            var moveVertical = Input.GetAxis("Vertical" + axisPostfix);
            _accumulatedInput += new Vector2(moveHorizontal, moveVertical) * Time.deltaTime;
            
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

        private void FixedUpdate()
        {
            _rigidbody.AddForce(_accumulatedInput * speed);
            _accumulatedInput = Vector2.zero;
        }
    }
}