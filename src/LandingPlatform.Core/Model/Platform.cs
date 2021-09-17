using System;
using LandingPlatform.Core.Exceptions;

namespace LandingPlatform.Core.Model
{
    public class Platform
    {
        private const string OkForLanding = "ok for landing";
        private const string OutOfPlatform = "out of platform";
        private const string Clash = "clash";

        private readonly Position _topLefCorner;
        private readonly Position _bottomRightCorner;
        private Position _latestReservedPosition;
        private readonly object _syncObject = new();

        public Platform(Position topLefCorner, Position bottomRightCorner)
        {
            if (topLefCorner >= bottomRightCorner) throw new InvalidPlatformSizeException();

            _topLefCorner = topLefCorner;
            _bottomRightCorner = bottomRightCorner;
        }

        public string IsItLandable(Position position)
        {
            MakeSurePositionIsNotNull(position);

            if (IsInSidePlatform(position)) return OutOfPlatform;

            lock (_syncObject)
            {
                if (HasPositionPreviouslyBeenCheckedByAnotherRocket(position)) return Clash;

                if (IsPositionLocatedNextToAPositionThatHasPreviouslyBeenCheckedByAnotherRocket(position)) return Clash;

                _latestReservedPosition = position;
            }

            return OkForLanding;
        }
        private static void MakeSurePositionIsNotNull(Position position)
        {
            if (position == null) throw new ArgumentNullException(nameof(position));
        }
        private bool IsInSidePlatform(Position position)
        {
            return position < _topLefCorner || position > _bottomRightCorner;
        }
        private bool HasPositionPreviouslyBeenCheckedByAnotherRocket(Position position)
        {
            return _latestReservedPosition != null && _latestReservedPosition.Equals(position);
        }
        private bool IsPositionLocatedNextToAPositionThatHasPreviouslyBeenCheckedByAnotherRocket(Position position)
        {
            return _latestReservedPosition != null && _latestReservedPosition.IsItOnPerimeter(position);
        }
    }
}