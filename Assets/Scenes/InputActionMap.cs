using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputActionMap : MonoBehaviour
{
  /// <summary>����\��������e�L�X�g�I�u�W�F�N�g�B</summary>
  [SerializeField] private Text TextObject;

  /// <summary>Move �A�N�V�����p�̒�`�B</summary>
  public InputAction MoveAction { get; set; }

  /// <summary>Attack �A�N�V�����p�̒�`�B</summary>
  public InputAction AttackAction { get; set; }

  private void Awake()
  {
    // Move �A�N�V�����̍쐬
    MoveAction = new InputAction("Move");

    // �o�C���h(�L�[���蓖��)�̒ǉ�
    // �ݒ肷�镶����̌`���̓A�N�V�����}�b�v�ݒ��ʂ� Path �ɕ\������镶����ƂȂ�
    MoveAction.AddBinding("<Gamepad>/leftStick");

    // �L�[�{�[�h�ɏ㉺���E�����蓖�Ă�ɂ̓R���|�W�b�g�� 2DVector ��ݒ肷��
    MoveAction.AddCompositeBinding("2DVector")
        .With("Up", "<Keyboard>/upArrow")
        .With("Down", "<Keyboard>/downArrow")
        .With("Left", "<Keyboard>/leftArrow")
        .With("Right", "<Keyboard>/rightArrow");

    // Attack �A�N�V�����̍쐬
    AttackAction = new InputAction("Attack");

    // �o�C���h(�L�[���蓖��)�̒ǉ�
    AttackAction.AddBinding("<Gamepad>/buttonSouth");
    AttackAction.AddBinding("<Keyboard>/z");

    // ���쎞�̃C�x���g��ݒ�
    MoveAction.performed += context => OnMove(context);
    MoveAction.canceled += context => OnMove(context);
    AttackAction.performed += context => OnAttack(context);
    AttackAction.canceled += context => OnAttack(context);
  }

  private void OnEnable()
  {
    // �I�u�W�F�N�g���L���ɂȂ����Ƃ��ɃA�N�V�����}�b�v��L���ɂ���
    MoveAction.Enable();
    AttackAction.Enable();
  }

  private void OnDisable()
  {
    // �A�N�V�����}�b�v���듮�삵�Ȃ��悤�ɃI�u�W�F�N�g�I�����ɖ����ɂ���
    MoveAction.Disable();
    AttackAction.Disable();
  }

  /// <summary>
  /// Move ������s�����Ƃ��ɌĂ΂��B
  /// </summary>
  /// <param name="context">�R�[���o�b�N���e�B</param>
  private void OnMove(InputAction.CallbackContext context)
  {
    var vec = context.ReadValue<Vector2>();
    TextObject.text = $"Move:({vec.x:f2}, {vec.y:f2})\n{TextObject.text}";
  }

  /// <summary>
  /// Move ������s�����Ƃ��ɌĂ΂��B
  /// </summary>
  /// <param name="context">�R�[���o�b�N���e�B</param>
  private void OnAttack(InputAction.CallbackContext context)
  {
    var value = context.ReadValueAsButton();
    TextObject.text = $"Attack:{value}\n{TextObject.text}";
  }

  /// <summary>
  /// �{�^�����N���b�N�����Ƃ��ɌĂ΂��B
  /// </summary>
  public void OnClickButton()
  {
    TextObject.text = "�A�N�V�����}�b�v��ύX���܂����B";

    // Move �A�N�V�����̃L�[��u��������
    MoveAction.ApplyBindingOverride(new InputBinding { path = "<Gamepad>/leftStick", overridePath = "<Gamepad>/dpad" } );
    MoveAction.ApplyBindingOverride(new InputBinding { path = "<Keyboard>/upArrow", overridePath = "<Keyboard>/w" });
    MoveAction.ApplyBindingOverride(new InputBinding { path = "<Keyboard>/downArrow", overridePath = "<Keyboard>/s" });
    MoveAction.ApplyBindingOverride(new InputBinding { path = "<Keyboard>/leftArrow", overridePath = "<Keyboard>/a" });
    MoveAction.ApplyBindingOverride(new InputBinding { path = "<Keyboard>/rightArrow", overridePath = "<Keyboard>/d" });

    // Attack �A�N�V�����̃L�[��u��������
    AttackAction.ApplyBindingOverride(new InputBinding { path = "<Gamepad>/buttonSouth", overridePath = "<Gamepad>/buttonEast" });
    AttackAction.ApplyBindingOverride(new InputBinding { path = "<Keyboard>/z", overridePath = "<Keyboard>/space" });
  }
}
