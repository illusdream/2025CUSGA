using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler
{
        public InputActionTracker<Vector2> Move { get;private set; }
        public InputActionTracker UseProp { get;private set; }
        public InputActionTracker DigTile { get;private set; }
        public InputActionTracker PlaceTile { get;private set; }

        public Vector2 LastActiveMoveDirection { get;private set; }
        public PlayerInputHandler(InputAction move, InputAction useProp, InputAction digTile, InputAction placeTile)
        {
                Move = new InputActionTracker<Vector2>(move);
                Move._trackedAction.performed += TrackedActionOnperformed;
                UseProp = new InputActionTracker(useProp);
                DigTile = new InputActionTracker(digTile);
                PlaceTile = new InputActionTracker(placeTile);
        }

        private void TrackedActionOnperformed(InputAction.CallbackContext obj)
        {
                var value = obj.ReadValue<Vector2>();
                LastActiveMoveDirection = value != Vector2.zero ? value : LastActiveMoveDirection;
        }
}