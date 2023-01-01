namespace DisciplesClient_Update_Service.LogicLayer.UsersLogic.Exceptions
{
    /// <summary>
    /// The user not found exception.
    /// </summary>
    public class UserNotFoundException : Exception
    {
        /// <summary>
        /// The user not found!
        /// </summary>
        public UserNotFoundException() : base() { }
        /// <summary>
        /// The user not found!
        /// </summary>
        /// <param name="message">The message.</param>
        public UserNotFoundException(string message) : base(message) { }
    }
}
