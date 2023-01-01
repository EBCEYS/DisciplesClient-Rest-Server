namespace DisciplesClient_Update_Service.LogicLayer.ModsLayer.Exceptions
{
    /// <summary>
    /// Throws if mod update date time is wrong.
    /// </summary>
    public class WrongModUpdateDateTimeException : Exception
    {
        /// <summary>
        /// Last mod's update date time.
        /// </summary>
        public DateTimeOffset LastUpdateDateTime { get; set; }
        /// <summary>
        /// Date time tried to set.
        /// </summary>
        public DateTimeOffset CurrentUpdateDateTime { get; set; }
        /// <summary>
        /// Throws <see cref="WrongModUpdateDateTimeException"/>.
        /// </summary>
        public WrongModUpdateDateTimeException(DateTimeOffset lastUpdateDateTime, DateTimeOffset currentUpdateDateTime) : base()
        {
            this.LastUpdateDateTime = lastUpdateDateTime;
            this.CurrentUpdateDateTime = currentUpdateDateTime;
        }
    }
}
