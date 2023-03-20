using UnityEngine;
using System.Collections;

namespace GameSystemsCookbook
{
    /// <summary>
    /// ScriptableObject-based Command that moves a Transform from start to end
    /// position. This raises events when executing and undoing commands.
    /// </summary>
    [CreateAssetMenu(fileName = "MoveCommand", menuName = "PaddleBall/MoveCommand")]
    public class MoveCommandSO : ScriptableObject, ICommand
    {
        [Tooltip("Begin at 3D position")]
        [SerializeField] private Vector3 m_StartPosition;
        [Tooltip("Final target 3D position")]
        [SerializeField] private Vector3 m_EndPosition;
        [Tooltip("Units per second")]
        [SerializeField] private float m_MoveSpeed = 5f;

        [Header("Broadcast on Event Channels")]
        [Tooltip("Invoked on Execute")]
        [SerializeField] private VoidEventChannelSO m_CommandExecuted;
        [Tooltip("Invoked on Undo")]
        [SerializeField] private VoidEventChannelSO m_CommandUndone;

        private Transform m_TransformToMove;

        // external MonoBehaviour (e.g. CommandManager) to start and stop coroutines
        private MonoBehaviour m_RoutineExecutor;

        private bool m_IsExecuting;
        private bool m_IsInitialized;

        // Properties
        public bool IsExecuting => m_IsExecuting;
        public bool IsInitialized => m_IsInitialized;

        // Set up dependencies for the Transform to move and the script invoking the command. The
        // routineExecutor can be any MonoBehaviour but likely will be the CommandManager.
        public void Initialize(Transform transformToMove, MonoBehaviour routineExecutor)
        {
            this.m_TransformToMove = transformToMove;
            this.m_RoutineExecutor = routineExecutor;
        }

        public void Execute()
        {
            Move(m_StartPosition, m_EndPosition);

            // option event channel to invoke
            if (m_CommandExecuted != null)
                m_CommandExecuted.RaiseEvent();
        }

        // Reverse the movement and raise an optional event channel.
        public void Undo()
        {
            Move(m_EndPosition, m_StartPosition);

            if (m_CommandUndone != null)
                m_CommandUndone.RaiseEvent();
        }

        // Move the Transform from the start to end position if we are
        // not already in progress. The MoveRoutine coroutine manages movement.
        private void Move(Vector3 start, Vector3 end)
        {
 
            if (m_TransformToMove == null || m_IsExecuting)
                return;

            m_TransformToMove.position = m_StartPosition;

            if (m_RoutineExecutor != null)
                m_RoutineExecutor.StartCoroutine(MoveRoutine(start, end));
        }

        public IEnumerator MoveRoutine(Vector3 start, Vector3 end)
        {
            m_IsExecuting = true;

            float timeElapsed = 0f;

            // distance in units from start to end
            float magnitude = (start - end).magnitude;

            // total time to move
            float duration = magnitude / m_MoveSpeed;

            while (timeElapsed < duration)
            {
                // interpolate based on percentage time to complete
                m_TransformToMove.position = Vector3.Lerp(start, end, timeElapsed / duration);

                // wait for one frame
                yield return null;
                timeElapsed += Time.deltaTime;
            }

            m_IsExecuting = false;

        }
    }
}