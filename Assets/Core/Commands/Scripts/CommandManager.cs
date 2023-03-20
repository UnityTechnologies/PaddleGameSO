using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This structure pairs each MoveCommandSO with a Transform. The delay pauses n seconds
    /// before executing movement.
    /// </summary>
    // 
    // TO-DO: Combine the delay/transform into the MoveCommandSO and create an Editor script that
    // shows the SO data in the Inspector
    [System.Serializable]
    public struct MoveTransformCommand
    {
        public float delay;
        public Transform transformToMove;
        public MoveCommandSO moveCommand;
    }

    /// <summary>
    /// This component manages a collection of ScriptableObject-based commands.
    /// </summary>
    public class CommandManager : MonoBehaviour
    {

        [Header("Broadcast on Event Channels")]
        [Tooltip("Send current index for active command")]
        [SerializeField] private IntEventChannelSO m_CommandUpdated;

        [Header("Listen to Event Channels")]
        [Tooltip("Undo the executed command and go back")]
        [SerializeField] private VoidEventChannelSO m_UndoButtonClicked;
        [Tooltip("Execute the next command")]
        [SerializeField] private VoidEventChannelSO m_ExecuteButtonClicked;
        [Tooltip("Predetermined list of commands to execute in order")]
        [SerializeField] private List<MoveTransformCommand> m_CommandSequence = new List<MoveTransformCommand>();

        // The next index to execute from command sequence
        private int m_CurrentIndex = 0;

        private void OnEnable()
        {
            if (m_ExecuteButtonClicked != null)
                m_ExecuteButtonClicked.OnEventRaised += OnExecute;

            if (m_UndoButtonClicked != null)
                m_UndoButtonClicked.OnEventRaised += OnUndo;
        }

        private void OnDisable()
        {
            if (m_ExecuteButtonClicked != null)
                m_ExecuteButtonClicked.OnEventRaised -= OnExecute;

            if (m_UndoButtonClicked != null)
                m_UndoButtonClicked.OnEventRaised -= OnUndo;
        }

        private void Start()
        {
            InitializeCommandList();
        }

        // Set up dependencies for each move command
        private void InitializeCommandList()
        {
            foreach (MoveTransformCommand command in m_CommandSequence)
            {
                command.moveCommand.Initialize(command.transformToMove, this);
            }
        }

        // Execute each move command after a delay
        private IEnumerator ExecuteRoutine(MoveTransformCommand commandToExecute)
        {
            // raise event with current command index from list
            if (m_CommandUpdated != null)
                m_CommandUpdated.RaiseEvent(m_CurrentIndex);

            yield return new WaitForSeconds(commandToExecute.delay);

            if (commandToExecute.moveCommand != null)
                commandToExecute.moveCommand.Execute();

            m_CurrentIndex++;
        }

        private IEnumerator UndoRoutine(MoveTransformCommand commandToUndo)
        {
            yield return new WaitForSeconds(commandToUndo.delay);

            // raise event with current command index from list
            if (m_CommandUpdated != null)
                m_CommandUpdated.RaiseEvent(m_CurrentIndex);

            if (commandToUndo.moveCommand != null)
                commandToUndo.moveCommand.Undo();

            m_CurrentIndex--;
        }

        // event-handling methods

        // Undo logic after receiving button click event. Decrements the currently active index and
        // gets the matching command. Then starts the UndoRoutine coroutine.
        private void OnUndo()
        {
            if (m_CurrentIndex <= 0)
                return;

            MoveTransformCommand activeCommand = m_CommandSequence[m_CurrentIndex - 1];

            if (activeCommand.transformToMove == null || activeCommand.moveCommand == null)
                return;

            StartCoroutine(UndoRoutine(activeCommand));
        }

        // Executes logic after receiving button click event. Increments the currently active index and
        // gets the matching command. Then starts the ExecuteRoutine coroutine.
        private void OnExecute()
        {
            if (m_CurrentIndex >= m_CommandSequence.Count)
                return;

            MoveTransformCommand activeCommand = m_CommandSequence[m_CurrentIndex];

            if (activeCommand.transformToMove == null || activeCommand.moveCommand == null)
                return;

            StartCoroutine(ExecuteRoutine(activeCommand));
        }

    }
}