namespace Lab3.data
{
    internal class Store
    {
        private State _state = new State(ScreenType.Points, new List<Point>());
        public State State
        {
            get { return _state; }
        }
        public event Action<State>? StateUpdate;

        public void updateState(Func<State, State> function)
        {
            _state = function(_state);
            StateUpdate?.Invoke(_state);
        }
    }
}
