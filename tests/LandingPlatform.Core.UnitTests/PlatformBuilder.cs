using LandingPlatform.Core.Model;

namespace LandingPlatform.Core.UnitTests
{
    public class PlatformBuilder
    {
        private int _topLeftX;
        private int _topLeftY;
        private int _bottomRightX;
        private int _bottomRightY;

        public PlatformBuilder()
        {
            ResetState();
        }
        private void ResetState()
        {
            _topLeftX = 5;
            _topLeftY = 5;
            _bottomRightX = 10;
            _bottomRightY = 10;
        }

        public Platform Build()
        {
            var platform = new Platform(new Position(_topLeftX, _topLeftY), 
                new Position(_bottomRightX, _bottomRightY));
            ResetState();
            return platform;
        }

        public PlatformBuilder WithTopLeftX(int topLeftX)
        {
            _topLeftX = topLeftX;
            return this;
        }

        public PlatformBuilder WithTopLeftY(int topLeftY)
        {
            _topLeftY = topLeftY;
            return this;
        }

        public PlatformBuilder WithBottomRightX(int bottomRightX)
        {
            _bottomRightX = bottomRightX;
            return this;
        }

        public PlatformBuilder WithBottomRightY(int bottomRightY)
        {
            _bottomRightY = bottomRightY;
            return this;
        }
    }
}
