using System;
using System.Collections.Generic;
using UnityEngine;

namespace CustomInput
{
    public class InputHandler
    {
        public delegate void InputActionDelegate(InputAction action);
        private Dictionary<InputAction, InputActionDelegate> inputActions = new Dictionary<InputAction, InputActionDelegate>();

        private IInputDevice _inputDevice;

        public InputHandler(IInputDevice inputDevice)
        {
            Debug.Log("InputHandler " + inputDevice.GetType());

            _inputDevice = inputDevice;
        }

        public void StartUpdate()
        {
            // Handle input actions
            if (_inputDevice.IsMoveForward())
            {
                NotifyInputAction(InputAction.MoveForward);
            }
            if (_inputDevice.IsMoveBackward())
            {
                NotifyInputAction(InputAction.MoveBackward);
            }
            if (_inputDevice.IsMoveLeft())
            {
                NotifyInputAction(InputAction.MoveLeft);
            }
            if (_inputDevice.IsMoveRight())
            {
                NotifyInputAction(InputAction.MoveRight);
            } 
            if (_inputDevice.IsMoveMouse())
            {
                NotifyInputAction(InputAction.MoveMouse);
            }   
            if (_inputDevice.IsMoveFinger())
            {
                NotifyInputAction(InputAction.MoveFinger);
            }
        }

        public void SubscribeToInputAction(InputAction action, InputActionDelegate callback)
        {
            if (inputActions.ContainsKey(action))
            {
                inputActions[action] += callback;
            }
            else
            {
                inputActions[action] = callback;
            }
        }

        public void UnsubscribeFromInputAction(InputAction action, InputActionDelegate callback)
        {
            if (inputActions.ContainsKey(action))
            {
                inputActions[action] -= callback;
            }
        }

        private void NotifyInputAction(InputAction action)
        {
            if (inputActions.ContainsKey(action))
            {
                inputActions[action](action);
            }
        }
    }
}