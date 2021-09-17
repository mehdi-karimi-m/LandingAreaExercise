using System;

namespace LandingPlatform.Core.Exceptions
{
    public class InvalidPlatformSizeException : Exception
    {
        private const string InvalidPlatformSizeMessage = "Invalid platform size.";
        public InvalidPlatformSizeException() :base(InvalidPlatformSizeMessage) { }
    }
}
