using UnityEngine;

namespace Client {
    struct TouchComponent {
        public Vector2 Direction, Position, InitialPosition;
        public TouchPhase Phase;
    }
}