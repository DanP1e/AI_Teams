using UnityEngine;
public class Player : AliveTeamMember
{
    private CharacterController _characterController;
    [SerializeField] private TeamsCollection _unitTeamsCollection;
    [SerializeField] private Teams _teamType;

    [Header("Controls")]
    public float Speed = 6.0f;
    public float JumpSpeed = 8.0f;
    public float Gravity = 20.0f;

    private Vector3 _moveDirection = Vector3.zero;

    public override int TeamId => (int)_teamType;

    private void Start()
    {
        _unitTeamsCollection.AddUniqueMember(this);
        _characterController = GetComponent<CharacterController>();
        // Привязываем метод "убийства" игрока к событию смерти унаследованного от Entity
        Dying += Kill;
    }

    private void Update()
    {
        // Если на земле
        if (_characterController.isGrounded)
        {
            // Передвижение
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            _moveDirection *= Speed;
            // Прыжок на пробел 
            if (Input.GetButton("Jump"))
            {
                _moveDirection.y = JumpSpeed;
            }
        }
        // Влияние гравитации
        _moveDirection.y -= Gravity * Time.deltaTime;

        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    // Функция смерти
    public void Kill(IAlive player) 
    {
        Camera.main.transform.SetParent(null);
        Destroy(gameObject);
        Debug.Log($"The {this.name} was killed");
    } 
}


