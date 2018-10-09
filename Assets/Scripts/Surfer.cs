using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurferRun.TaskCoroutine;

namespace SurferRun
{
    public class Surfer : MonoBehaviour
    {

        public enum State { Idle, Up, Down };

        public event System.Action<State> OnStateChanged = delegate {};

        [Header("Move Settings")]
        [SerializeField] private float deltaMove = 1.0f;
        [SerializeField] private float timeMove = 0.2f;
        [SerializeField] private float timeFreeze = 1.0f;

        private Vector3 startPos, upPos, downPos;
        private PingPongTask upMoveTask, downMoveTask;
        private State state = State.Idle;

        [Header("Blinking Settings")]
        [SerializeField] new private SpriteRenderer renderer;
        [SerializeField] private float blinkingTime = 5.0f;
        [SerializeField] private float blinkPhaseDelta = 0.05f;
        [SerializeField] private float blinkDelay = 0.5f;
        private bool blinking;
        private InfinityPingPongTask blinkingTask;
        private DelayInvokeTask blinkCooldownTask;

        public State SurferState
        {
            get { return state; }
            private set
            {
                state = value;
                OnStateChanged(state);
            }
        }

        public bool Blinking
        {
            get { return blinking; }
            set
            {
                blinking = value;
                if (blinking)
                {
                    blinkingTask.Start();
                    blinkCooldownTask.Start();
                }
                else
                {
                    blinkingTask.Stop();
                    renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1.0f);
                }
            }
        }

        public float DeltaMove
        {
            get { return deltaMove; }
            set { deltaMove = value; }
        }

        public float TimeMove
        {
            get { return timeMove; }
            set { timeMove = value; }
        }

        public float TimeFreeze
        {
            get { return timeFreeze; }
            set { timeFreeze = value; }
        }

        void Start()
        {

            startPos = transform.position;
            upPos = new Vector3(startPos.x, startPos.y + deltaMove, startPos.z);
            downPos = new Vector3(startPos.x, startPos.y - deltaMove, startPos.z);

            //Tasks for surfer move
            upMoveTask = new PingPongTask((t) =>
            {
                transform.position = Vector3.Lerp(startPos, upPos, t);
            }, timeMove, timeFreeze, false);

            downMoveTask = new PingPongTask((t) =>
            {
                transform.position = Vector3.Lerp(startPos, downPos, t);
            }, timeMove, timeFreeze, false);

            blinkingTask = new InfinityPingPongTask(t => renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, t),
                                                    blinkPhaseDelta, blinkDelay, false);

            blinkCooldownTask = new DelayInvokeTask(blinkingTime, () => { Blinking = false; }, false);

            upMoveTask.OnStop += OnStable;
            downMoveTask.OnStop += OnStable;
        }

        private void OnStable(object sender, System.EventArgs e)
        {
            SurferState = State.Idle;
        }

        public void MoveUp()
        {
            if (state != State.Idle) return;

            upMoveTask.Start();
            SurferState = State.Up;
        }

        public void MoveDown()
        {
            if (state != State.Idle) return;

            downMoveTask.Start();
            SurferState = State.Down;
        }

        public void Restart()
        {
            upMoveTask.Stop();
            downMoveTask.Stop();

            Blinking = false;
            SurferState = State.Idle;

            transform.position = startPos;
        }
    }
}
