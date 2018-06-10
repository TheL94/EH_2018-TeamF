using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class Movement : MonoBehaviour
    {
        public float MovementSpeed { get { return character.MovementSpeed; } }
        public float RotationSpeed { get { return character.Data.RotationSpeed; } }

        Character character;
        Rigidbody playerRigidbody;
        ParticlesController particle;

        #region AnimVariables

        Animator anim;
        Transform cam;
        Vector3 camForward;
        Vector3 move;
        Vector3 moveInput;

        float forwardAmmount;
        float turnAmmount;

        #endregion


        public void Init(Character _character, DashStruct _dashData)
        {
            character = _character;
            cam = Camera.main.transform;
            anim = GetComponentInChildren<Animator>();
            playerRigidbody = GetComponent<Rigidbody>();
            particle = GetComponentInChildren<ParticlesController>();
            dashData = _dashData;
            chargeCount = dashData.ChargeCount;
        }

        public void Move(Vector3 _position)
        {
            /// Calcolo dell'animazione da riprodurre

            if (cam != null)
            {
                camForward = Vector3.Scale(cam.up, new Vector3(1, 0, 1)).normalized;
                move = _position.z * camForward + _position.x * cam.right;
            }
            else
            {
                move = _position.z * Vector3.forward + _position.x * Vector3.right;
            }

            ManageAnimations(move);

            Vector3 position = _position.normalized * MovementSpeed * Time.deltaTime;
            playerRigidbody.MovePosition(transform.position + position);
        }

        public void Turn()
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;

            if (Physics.Raycast(mouseRay, out floorHit, float.PositiveInfinity, 1 << LayerMask.NameToLayer("MouseRaycast")))
            {
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0;
                Quaternion newRotatation = Quaternion.LookRotation(playerToMouse, transform.up);
                playerRigidbody.MoveRotation(newRotatation);
            }
        }

        /// <summary>
        /// Normalizza il vettore dato, chiama le funzioni per trasformare il vettore di movimento da global a local e passa i valori all'animator
        /// </summary>
        /// <param name="_move"></param>
        void ManageAnimations(Vector3 _move)
        {
            if (_move.magnitude > 1)
                _move.Normalize();

            moveInput = _move;

            ConvertMoveInput();
            UpdateAnimator();
        }

        /// <summary>
        /// Converte MoveInput da global a local e salva i valori di X e Z in due variabili float
        /// </summary>
        void ConvertMoveInput()
        {
            Vector3 localMove = transform.InverseTransformDirection(moveInput);

            if (localMove.z < -.6f && localMove.x != 0)
            {
                turnAmmount = -localMove.x;
                forwardAmmount = localMove.z;
            }
            else
            {
                turnAmmount = localMove.x;
                forwardAmmount = localMove.z;
            }
        }

        /// <summary>
        /// Setta l'animator con i valori float salvati nella funzione ConvertMoveInput
        /// </summary>
        void UpdateAnimator()
        {
            // Setta l'animator
            anim.SetFloat("Forward", forwardAmmount, .1f, Time.deltaTime);
            anim.SetFloat("Turn", turnAmmount, .1f, Time.deltaTime);
        }

        #region Dash
        DashStruct dashData;
        int chargeCount;            // Le cariche di dash eseguibili
        float coolDown;             // Il timer che allo scadere viene rigenerata una tacca di dash

        public void Dash(Vector3 _direction)
        {
            if (chargeCount > 0)
            {
                particle.ActivateParticles(ParticlesController.ParticleType.Dash);

                GameManager.I.AudioMng.PlaySound(Clips.CharacterDash);

                playerRigidbody.AddForce(_direction.normalized * dashData.DashForce, ForceMode.Impulse);

                if (_direction.x == 0 && _direction.z == 0)
                    playerRigidbody.AddForce(transform.forward.normalized * dashData.DashForce * 1.5f, ForceMode.Impulse);

                chargeCount--;
            }
        }

        private void Update()
        {
            if (chargeCount < dashData.ChargeCount)
            {
                coolDown += Time.deltaTime;
                if (coolDown >= dashData.ChargeCooldown)
                {
                    chargeCount++;
                    coolDown = 0;
                }
            }
        }
        #endregion
    }
}