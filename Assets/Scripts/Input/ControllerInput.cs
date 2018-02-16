using System;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

namespace Unity_Framework.ControllerInput
{
    public class ControllerInput
    {
        // Variabili per il funzionamento dei controller
        PlayerIndex playerIndex;
        GamePadState state;
        GamePadState prevState;

        public ControllerInput(int _playerIndex)
        {
            switch (_playerIndex)
            {
                case 0:
                    playerIndex = PlayerIndex.One;
                    break;
                case 1:
                    playerIndex = PlayerIndex.Two;
                    break;
                case 2:
                    playerIndex = PlayerIndex.Three;
                    break;
                case 3:
                    playerIndex = PlayerIndex.Four;
                    break;
            }
        }

        #region API
        /// <summary>
        /// Ritorna l'input del player nella forma di struttura
        /// </summary>
        /// <returns></returns>
        public InputStatus GetPlayerInputStatus()
        {
            InputStatus inputStatus = ReadControllerInput();
            return inputStatus;
        }

        public void SetControllerVibration(float _leftMotor, float _rightMotor)
        {
            GamePad.SetVibration(playerIndex, _leftMotor, _rightMotor);
        }
        #endregion

        #region Read Input
        /// <summary>
        /// Controlla l'input da controller (usando il plugin XInputDotNetPure)
        /// </summary>
        InputStatus ReadControllerInput()
        {
            InputStatus inputStatus = new InputStatus();

            prevState = state;
            state = GamePad.GetState(playerIndex, GamePadDeadZone.Circular);

            if (!state.IsConnected)
            {
                inputStatus.IsConnected = state.IsConnected;
                return inputStatus;
            }
            else
            {
                inputStatus.IsConnected = state.IsConnected;
            }

            ReadTriggers(inputStatus);
            ReadShoulders(inputStatus);
            ReadThumbSticks(inputStatus);
            ReadStartSelect(inputStatus);
            ReadABXY(inputStatus);
            ReadDPads(inputStatus);

            return inputStatus;
        }

        void ReadStartSelect(InputStatus _inputStatus)
        {
            if (state.Buttons.Start == XInputDotNetPure.ButtonState.Pressed)
            {
                if(prevState.Buttons.Start == XInputDotNetPure.ButtonState.Released)
                    _inputStatus.Start = ButtonState.Pressed;
                else
                    _inputStatus.Start = ButtonState.Held;
            }

            if (state.Buttons.Back == XInputDotNetPure.ButtonState.Pressed)
            {
                if(prevState.Buttons.Back == XInputDotNetPure.ButtonState.Released)
                    _inputStatus.Select = ButtonState.Pressed;
                else
                    _inputStatus.Select = ButtonState.Held;
            }
        }

        void ReadThumbSticks(InputStatus _inputStatus)
        {
            // ThumbSticks float values
            _inputStatus.LeftThumbSticksAxisX = state.ThumbSticks.Left.X;
            _inputStatus.LeftThumbSticksAxisY = state.ThumbSticks.Left.Y;

            _inputStatus.RightThumbSticksAxisX = state.ThumbSticks.Right.X;
            _inputStatus.RightThumbSticksAxisY = state.ThumbSticks.Right.Y;

            // button values
            if (state.Buttons.LeftStick == XInputDotNetPure.ButtonState.Pressed)
            {
                if(prevState.Buttons.LeftStick == XInputDotNetPure.ButtonState.Released)
                    _inputStatus.LeftSticks = ButtonState.Pressed;
                else
                    _inputStatus.LeftSticks = ButtonState.Held;
            }

            if (state.Buttons.RightStick == XInputDotNetPure.ButtonState.Pressed)
            {
                if(prevState.Buttons.RightStick == XInputDotNetPure.ButtonState.Released)
                    _inputStatus.RightSticks = ButtonState.Pressed;
                else
                    _inputStatus.RightSticks = ButtonState.Held;
            }
        }

        ButtonState rightTriggerOldState;
        ButtonState leftTriggerOldState;

        void ReadTriggers(InputStatus _inputStatus)
        {
            // triggers float values
            _inputStatus.LeftTriggerAxis = state.Triggers.Left;
            _inputStatus.RightTriggerAxis = state.Triggers.Right;

            // Left Trigger as button
            if (_inputStatus.LeftTriggerAxis <= 0.1f)
            {
                leftTriggerOldState = _inputStatus.LeftTrigger = ButtonState.Released;
            }
            else
            {
                if (leftTriggerOldState == ButtonState.Released)
                    leftTriggerOldState = _inputStatus.LeftTrigger = ButtonState.Pressed;

                else
                    leftTriggerOldState = _inputStatus.LeftTrigger = ButtonState.Held;
            }

            // Right Trigger as button
            if (_inputStatus.RightTriggerAxis <= 0.1f)
            {
                rightTriggerOldState = _inputStatus.RightTrigger = ButtonState.Released;
            }
            else
            {
                if (rightTriggerOldState == ButtonState.Released)
                    rightTriggerOldState = _inputStatus.RightTrigger = ButtonState.Pressed;

                else
                    rightTriggerOldState = _inputStatus.RightTrigger = ButtonState.Held;
            }
        }

        void ReadABXY(InputStatus _inputStatus)
        {
            if (state.Buttons.A == XInputDotNetPure.ButtonState.Pressed)
            {
                if(prevState.Buttons.A == XInputDotNetPure.ButtonState.Released)
                    _inputStatus.A = ButtonState.Pressed;
                else
                    _inputStatus.A = ButtonState.Held;
            }

            if (state.Buttons.B == XInputDotNetPure.ButtonState.Pressed)
            {
                if(prevState.Buttons.B == XInputDotNetPure.ButtonState.Released)
                    _inputStatus.B = ButtonState.Pressed;
                else
                    _inputStatus.B = ButtonState.Held;
            }

            if (state.Buttons.X == XInputDotNetPure.ButtonState.Pressed)
            {
                if(prevState.Buttons.X == XInputDotNetPure.ButtonState.Released)
                    _inputStatus.X = ButtonState.Pressed;
                else
                    _inputStatus.X = ButtonState.Held;
            }

            if (state.Buttons.Y == XInputDotNetPure.ButtonState.Pressed)
            {
                if(prevState.Buttons.Y == XInputDotNetPure.ButtonState.Released)
                    _inputStatus.Y = ButtonState.Pressed;
                else
                    _inputStatus.Y = ButtonState.Held;
            }
        }

        void ReadShoulders(InputStatus _inputStatus)
        {
            if (state.Buttons.LeftShoulder == XInputDotNetPure.ButtonState.Pressed)
            {
                if(prevState.Buttons.LeftShoulder == XInputDotNetPure.ButtonState.Released)
                    _inputStatus.LeftShoulder = ButtonState.Pressed;
                else
                    _inputStatus.LeftShoulder = ButtonState.Held;
            }

            if (state.Buttons.RightShoulder == XInputDotNetPure.ButtonState.Pressed)
            {
                if(prevState.Buttons.RightShoulder == XInputDotNetPure.ButtonState.Released)
                    _inputStatus.RightShoulder = ButtonState.Pressed;
                else
                    _inputStatus.RightShoulder = ButtonState.Held;
            }
        }

        void ReadDPads(InputStatus _inputStatus)
        {
            if (state.DPad.Up == XInputDotNetPure.ButtonState.Pressed)
            {
                if(prevState.DPad.Up == XInputDotNetPure.ButtonState.Released)
                    _inputStatus.DPadUp = ButtonState.Pressed;
                else
                    _inputStatus.DPadUp = ButtonState.Held;
            }

            if (state.DPad.Right == XInputDotNetPure.ButtonState.Pressed)
            {
                if(prevState.DPad.Right == XInputDotNetPure.ButtonState.Released)
                    _inputStatus.DPadRight = ButtonState.Pressed;
                else
                    _inputStatus.DPadRight = ButtonState.Held;
            }

            if (state.DPad.Down == XInputDotNetPure.ButtonState.Pressed)
            {
                if(prevState.DPad.Down == XInputDotNetPure.ButtonState.Released)
                    _inputStatus.DPadDown = ButtonState.Pressed;
                else
                    _inputStatus.DPadDown = ButtonState.Held;
            }

            if (state.DPad.Left == XInputDotNetPure.ButtonState.Pressed)
            {
                if(prevState.DPad.Left == XInputDotNetPure.ButtonState.Released)
                    _inputStatus.DPadLeft = ButtonState.Pressed;
                else
                    _inputStatus.DPadLeft = ButtonState.Held;
            }
        }
        #endregion
    }

    /// <summary>
    /// Stato del bottone
    /// </summary>
    public enum ButtonState
    {
        Released = 0,
        Pressed = 1,
        Held = 2
    }

    /// <summary>
    /// Struttura che contine tutti i comandi del joystick
    /// </summary>
    public class InputStatus
    {
        public bool IsConnected;

        public float LeftTriggerAxis;
        public float RightTriggerAxis;

        public float LeftThumbSticksAxisX;
        public float LeftThumbSticksAxisY;

        public float RightThumbSticksAxisX;
        public float RightThumbSticksAxisY;

        public ButtonState A;
        public ButtonState B;
        public ButtonState X;
        public ButtonState Y;

        public ButtonState LeftShoulder;
        public ButtonState RightShoulder;

        public ButtonState LeftTrigger;
        public ButtonState RightTrigger;

        public ButtonState LeftSticks;
        public ButtonState RightSticks;

        public ButtonState DPadUp;
        public ButtonState DPadLeft;
        public ButtonState DPadDown;
        public ButtonState DPadRight;

        public ButtonState Start;
        public ButtonState Select;

        /// <summary>
        /// Reset the value of each field as default
        /// </summary>
        public void Reset()
        {
            IsConnected = false;

            LeftTriggerAxis = 0;
            RightTriggerAxis = 0;

            LeftThumbSticksAxisX = 0;
            LeftThumbSticksAxisY = 0;

            RightThumbSticksAxisX = 0;
            RightThumbSticksAxisY = 0;

            A = ButtonState.Released;
            B = ButtonState.Released;
            X = ButtonState.Released;
            Y = ButtonState.Released;

            LeftShoulder = ButtonState.Released;
            RightShoulder = ButtonState.Released;

            LeftTrigger = ButtonState.Released;
            RightTrigger = ButtonState.Released;

            LeftSticks = ButtonState.Released;
            RightSticks = ButtonState.Released;

            DPadUp = ButtonState.Released;
            DPadLeft = ButtonState.Released;
            DPadDown = ButtonState.Released;
            DPadRight = ButtonState.Released;

            Start = ButtonState.Released;
            Select = ButtonState.Released;
        }
    }
}