using System;
using UnityEngine;

namespace CustomInput
{
    public class DesktopInputDevice : IInputDevice
    {
        public bool IsMoveBackward()
        {
            return false;
        }

        public bool IsMoveForward()
        {
            return false;
        }

        public bool IsMoveRight()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                return true;
            }           
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                return true;
            }

            return false;
        }

        public bool IsMoveLeft()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                return true;
            }           
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                return true;
            }

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