namespace BudgetFirst.SharedInterfaces.Persistence
{
    using BudgetFirst.SharedInterfaces.Messaging;

    /// <summary>
    /// Represents the master vector clock
    /// </summary>
    public class MasterVectorClock : IVectorClock
    {
        /// <summary>
        /// Current vector clock
        /// </summary>
        private VectorClock vectorClock;

        /// <summary>
        /// Current device Id
        /// </summary>
        private DeviceId deviceId;

        /// <summary>
        /// Initialises a new instance of the <see cref="MasterVectorClock"/> class.
        /// </summary>
        /// <param name="deviceId">Device id</param>
        public MasterVectorClock(DeviceId deviceId)
        {
            this.deviceId = deviceId;
            this.vectorClock = new VectorClock();
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="MasterVectorClock"/> class.
        /// </summary>
        /// <param name="deviceId">Device id</param>
        /// <param name="vectorClock">Current vector clock</param>
        private MasterVectorClock(DeviceId deviceId, VectorClock vectorClock)
        {
            this.deviceId = deviceId;
            this.vectorClock = vectorClock.Copy();
        }

        /// <summary>
        /// Set the current state
        /// </summary>
        /// <param name="vectorClock">Vector clock</param>
        public void SetState(VectorClock vectorClock)
        {
            this.vectorClock = vectorClock.Copy();
        }

        /// <summary>
        /// Increment the current vector clock
        /// </summary>
        public void Increment()
        {
            var newValue = this.vectorClock.Increment(this.deviceId.ToString()); // use device id specific tostring
            this.vectorClock = newValue;
        }

        /// <summary>
        /// Create a copy of this vector clock
        /// </summary>
        /// <returns>A clone of this vector clock</returns>
        public IVectorClock Clone()
        {
            return new MasterVectorClock(this.deviceId, this.vectorClock);
        }

        /// <summary>
        /// Get the current vector clock
        /// </summary>
        /// <returns>Underlying vector clock</returns>
        public VectorClock GetVectorClock()
        {
            return this.vectorClock.Copy();
        }

        /// <summary>
        /// Update the vector clock to a new value
        /// </summary>
        /// <param name="value">New value to set</param>
        public void Update(IVectorClock value)
        {
            this.vectorClock = value.GetVectorClock().Copy();
        }
    }
}