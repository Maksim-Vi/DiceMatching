using System;
using UnityEngine;

namespace CustomInput
{
    public class MobileInputDevice : IInputDevice
    {
        public bool IsMoveBackward()
        {
            return false;
        }

        public bool IsMoveForward()
        {
            return false;
        }

        public bool IsMoveLeft()
        {
            return false;
        }

        public bool IsMoveRight()
        {
            return false;
        }

        public bool IsMoveMouse()
        {
            return false;
        }

        public bool IsMoveFinger()
        {
            return false;
        }
    }
}