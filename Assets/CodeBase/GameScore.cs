namespace BlackBall
{
    public class GameScore
    {
        public int Score => (int)_passedDistance;
        private float _passedDistance;

        public void UpdateScore(float passedDistance)
        {
            _passedDistance += passedDistance;
        }
    }
}