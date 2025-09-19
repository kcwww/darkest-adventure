using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerCustomInput
{
    public class PlayerCustomInput : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

        [Header("Interaction")]
        public bool interact;
        public bool interactUnLocked = true;

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
        {
            if (UIManager.Instance != null && !UIManager.Instance.isWatching)
            {
                MoveInput(value.Get<Vector2>());
            }
        }
        public void OnLook(InputValue value)
        {
            if (cursorInputForLook) LookInput(value.Get<Vector2>());
        }
        public void OnJump(InputValue value) => JumpInput(value.isPressed);
        public void OnSprint(InputValue value) => SprintInput(value.isPressed);
        public void OnInteract(InputValue value)
        {
            if (interactUnLocked) InteractInput(value.isPressed);
        }
#endif

        public void MoveInput(Vector2 newMoveDirection) => move = newMoveDirection;
        public void LookInput(Vector2 newLookDirection) => look = newLookDirection;
        public void JumpInput(bool newJumpState) => jump = newJumpState;
        public void SprintInput(bool newSprintState) => sprint = newSprintState;
        public void InteractInput(bool newInteractState) => interact = newInteractState;

        private void OnApplicationFocus(bool hasFocus) => SetCursorState(cursorLocked);

        /// <summary>
        /// 커서와 카메라 회전 입력을 동시에 제어하는 함수
        /// </summary>
        public void EnableLook(bool enable)
        {
            cursorInputForLook = enable;
            cursorLocked = enable;
            SetCursorState(enable);
            Cursor.visible = !enable; // enable=true → 마우스 숨김, enable=false → 마우스 보임
        }

        public void SetInteractState(bool state)
        {
            interactUnLocked = state;
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}
