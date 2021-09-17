using System;
using FluentAssertions;
using LandingPlatform.Core.Exceptions;
using LandingPlatform.Core.Model;
using Xunit;

namespace LandingPlatform.Core.UnitTests
{
    public class PlatformTests
    {
        private const string OkForLanding = "ok for landing";
        private const string OutOfPlatform = "out of platform";
        private const string Clash = "clash";

        private readonly PlatformBuilder _platformBuilder = new();
        
        [Fact]
        public void Should_return_Ok_for_landing()
        {
            //Arrange
            var coordinate = new Position(6, 6);
            var sut = _platformBuilder
                .WithTopLeftX(5)
                .WithTopLeftY(5)
                .WithBottomRightX(10)
                .WithBottomRightY(10)
                .Build();
            //Act
            var result = sut.IsItLandable(coordinate);
            //Assert
            result.Should().Be(OkForLanding);
        }

        [Fact]
        public void Should_return_Out_of_platform()
        {
            //Arrange
            var coordinate = new Position(20, 20);
            var sut = _platformBuilder
                .WithTopLeftX(5)
                .WithTopLeftY(5)
                .WithBottomRightX(10)
                .WithBottomRightY(10)
                .Build();
            //Act
            var result = sut.IsItLandable(coordinate);
            //Assert
            result.Should().Be(OutOfPlatform);
        }

        [Fact]
        public void Should_return_Clash_when_position_has_previously_been_checked_by_another_rocket()
        {
            //Arrange
            var coordinate = new Position(5, 6);
            var sut = _platformBuilder
                .WithTopLeftX(5)
                .WithTopLeftY(5)
                .WithBottomRightX(10)
                .WithBottomRightY(10)
                .Build();
            //Act
            sut.IsItLandable(coordinate);
            var result = sut.IsItLandable(coordinate);
            //Assert
            result.Should().Be(Clash);
        }

        [Theory]
        [InlineData(6, 6)]
        [InlineData(6, 7)]
        [InlineData(6, 8)]
        [InlineData(7, 6)]
        [InlineData(7, 8)]
        [InlineData(8, 6)]
        [InlineData(8, 7)]
        [InlineData(8, 8)]
        public void Should_return_Clash_when_position_is_located_next_to_a_position_that_has_previously_been_checked_by_another_rocket(int x, int y)
        {
            //Arrange
            var previousCoordinate = new Position(7, 7);
            var sut = _platformBuilder
                .WithTopLeftX(5)
                .WithTopLeftY(5)
                .WithBottomRightX(10)
                .WithBottomRightY(10)
                .Build();
            var newCoordinate = new Position(x, y);
            //Act
            sut.IsItLandable(previousCoordinate);
            var result = sut.IsItLandable(newCoordinate);
            //Assert
            result.Should().Be(Clash);
        }

        [Fact]
        public void Should_throw_argument_null_exception_when_position_is_null()
        {
            //Arrange
            var sut = _platformBuilder.Build();
            //Act
            Action isItLandable = () => sut.IsItLandable(null);
            //Assert
            isItLandable.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Should_create_Platform()
        {
            //Arrange
            //Act
            var sut = _platformBuilder.Build();
            //Assert
            sut.Should().NotBeNull();
        }

        [Fact]
        public void Should_throw_InvalidPlatformSizeException_when_top_left_corner_is_greater_bottom_right_corner()
        {
            //Arrange
            //Act
            Action sut = () => _platformBuilder
                .WithTopLeftX(8)
                .WithTopLeftY(9)
                .WithBottomRightX(4)
                .WithBottomRightY(6)
                .Build();
            //Assert
            sut.Should().Throw<InvalidPlatformSizeException>();
        }

        [Fact]
        public void Should_throw_InvalidPlatformSizeException_when_top_left_corner_is_equal_bottom_right_corner()
        {
            //Arrange
            //Act
            Action sut = () => _platformBuilder
                .WithTopLeftX(5)
                .WithTopLeftY(5)
                .WithBottomRightX(5)
                .WithBottomRightY(5)
                .Build();
            //Assert
            sut.Should().Throw<InvalidPlatformSizeException>();
        }
    }
}
