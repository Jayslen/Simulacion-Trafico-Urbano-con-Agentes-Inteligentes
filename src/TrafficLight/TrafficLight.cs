using TrafficSimParallel.Types;

namespace TrafficSimParallel.Models
{
    public class TrafficLight
    {
        private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private LightState _state;
        public Position Position { get; init; }

        public TrafficLight(Position pos, LightState initialState)
        {
            Position = pos;
            _state = initialState;
        }

        public LightState GetState()
        {
            _lock.EnterReadLock();
            try
            {
                return _state;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void ToggleState()
        {
            _lock.EnterWriteLock();
            try
            {
                _state = (_state == LightState.Green) ? LightState.Red : LightState.Green;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}
