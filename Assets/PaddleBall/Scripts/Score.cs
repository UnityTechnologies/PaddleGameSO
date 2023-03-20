namespace GameSystemsCookbook.Demos.PaddleBall
{
    /// <summary>
    /// This object includes basic methods to reset and increment the score value.
    /// </summary>
    public class Score
    {
        private int m_Value;
        public int Value => m_Value;

        public void ResetScore()
        {
            m_Value = 0;
        }

        public void IncrementScore()
        {
            m_Value++;
        }
    }
}