namespace ToyRobot
{
    public sealed class PlaceCommandParser : ICommandParser
    {
        private readonly ICommandPerformerFactory commandPerformerFactory;
        private readonly IOrientationParser orientationParser;

        public PlaceCommandParser(
            ICommandPerformerFactory commandPerformerFactory,
            IOrientationParser orientationParser)
        {
            this.commandPerformerFactory = commandPerformerFactory;
            this.orientationParser = orientationParser;
        }
        
        public bool TryGetCommandPerformer(
            string unparsedCommand, out ICommandPerformer commandPerformer)
        {
            var placeAndState = unparsedCommand.Split(' ');
            if (placeAndState.Length != 2 || placeAndState[0] != "PLACE")
            {
                commandPerformer = null;
                return false;
            }

            var unparsedState = placeAndState[1].Split(',');
            if (unparsedState.Length != 3)
            {
                commandPerformer = null;
                return false;
            }

            int xCoordinate;
            if (!int.TryParse(unparsedState[0], out xCoordinate))
            {
                commandPerformer = null;
                return false;
            }

            int yCoordinate;
            if (!int.TryParse(unparsedState[1], out yCoordinate))
            {
                commandPerformer = null;
                return false;
            }

            IOrientation orientation;
            if (!this.orientationParser.TryParse(unparsedState[2], out orientation))
            {
                commandPerformer = null;
                return false;
            }

            commandPerformer = this.commandPerformerFactory.CreatePlacePerformer(
                xCoordinate, yCoordinate, orientation);
            return true;
        }
    }
}
