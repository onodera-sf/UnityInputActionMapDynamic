using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputActionMap : MonoBehaviour
{
  /// <summary>情報を表示させるテキストオブジェクト。</summary>
  [SerializeField] private Text TextObject;

  /// <summary>Move アクション用の定義。</summary>
  public InputAction MoveAction { get; set; }

  /// <summary>Attack アクション用の定義。</summary>
  public InputAction AttackAction { get; set; }

  private void Awake()
  {
    // Move アクションの作成
    MoveAction = new InputAction("Move");

    // バインド(キー割り当て)の追加
    // 設定する文字列の形式はアクションマップ設定画面の Path に表示される文字列となる
    MoveAction.AddBinding("<Gamepad>/leftStick");

    // キーボードに上下左右を割り当てるにはコンポジットの 2DVector を設定する
    MoveAction.AddCompositeBinding("2DVector")
        .With("Up", "<Keyboard>/upArrow")
        .With("Down", "<Keyboard>/downArrow")
        .With("Left", "<Keyboard>/leftArrow")
        .With("Right", "<Keyboard>/rightArrow");

    // Attack アクションの作成
    AttackAction = new InputAction("Attack");

    // バインド(キー割り当て)の追加
    AttackAction.AddBinding("<Gamepad>/buttonSouth");
    AttackAction.AddBinding("<Keyboard>/z");

    // 操作時のイベントを設定
    MoveAction.performed += context => OnMove(context);
    MoveAction.canceled += context => OnMove(context);
    AttackAction.performed += context => OnAttack(context);
    AttackAction.canceled += context => OnAttack(context);
  }

  private void OnEnable()
  {
    // オブジェクトが有効になったときにアクションマップを有効にする
    MoveAction.Enable();
    AttackAction.Enable();
  }

  private void OnDisable()
  {
    // アクションマップが誤動作しないようにオブジェクト終了時に無効にする
    MoveAction.Disable();
    AttackAction.Disable();
  }

  /// <summary>
  /// Move 操作を行ったときに呼ばれる。
  /// </summary>
  /// <param name="context">コールバック内容。</param>
  private void OnMove(InputAction.CallbackContext context)
  {
    var vec = context.ReadValue<Vector2>();
    TextObject.text = $"Move:({vec.x:f2}, {vec.y:f2})\n{TextObject.text}";
  }

  /// <summary>
  /// Move 操作を行ったときに呼ばれる。
  /// </summary>
  /// <param name="context">コールバック内容。</param>
  private void OnAttack(InputAction.CallbackContext context)
  {
    var value = context.ReadValueAsButton();
    TextObject.text = $"Attack:{value}\n{TextObject.text}";
  }

  /// <summary>
  /// ボタンをクリックしたときに呼ばれる。
  /// </summary>
  public void OnClickButton()
  {
    TextObject.text = "アクションマップを変更しました。";

    // Move アクションのキーを置き換える
    MoveAction.ApplyBindingOverride(new InputBinding { path = "<Gamepad>/leftStick", overridePath = "<Gamepad>/dpad" } );
    MoveAction.ApplyBindingOverride(new InputBinding { path = "<Keyboard>/upArrow", overridePath = "<Keyboard>/w" });
    MoveAction.ApplyBindingOverride(new InputBinding { path = "<Keyboard>/downArrow", overridePath = "<Keyboard>/s" });
    MoveAction.ApplyBindingOverride(new InputBinding { path = "<Keyboard>/leftArrow", overridePath = "<Keyboard>/a" });
    MoveAction.ApplyBindingOverride(new InputBinding { path = "<Keyboard>/rightArrow", overridePath = "<Keyboard>/d" });

    // Attack アクションのキーを置き換える
    AttackAction.ApplyBindingOverride(new InputBinding { path = "<Gamepad>/buttonSouth", overridePath = "<Gamepad>/buttonEast" });
    AttackAction.ApplyBindingOverride(new InputBinding { path = "<Keyboard>/z", overridePath = "<Keyboard>/space" });
  }
}
