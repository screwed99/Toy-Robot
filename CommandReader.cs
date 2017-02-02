using System.Collections.Generic;

namespace ToyRobot
{
    public interface ICommandReader
    {
        void Read(IReadOnlyCollection<string> commands);
    }
    
    public sealed class CommandReader : ICommandReader
    {
        private IToyRobot toyRobot;
        private ICommandPerformerFactory commandPerformerFactory;
        private IOrientationFactory orientationFactory;
        
        public CommandReader(
            IToyRobot toyRobot,
            ICommandPerformerFactory commandPerformerFactory,
            IOrientationFactory orientationFactory)
        {
            this.toyRobot = toyRobot;
            this.commandPerformerFactory = commandPerformerFactory;
            this.orientationFactory = orientationFactory;
        }

        public void Read(IReadOnlyCollection<string> unparsedCommands)
        {
            foreach (var unparsedCommand in unparsedCommands)
            {
                ICommandPerformer commandPerformer;
                if (this.TryGetCommandPerformer(unparsedCommand, out commandPerformer))
                {
                    this.toyRobot.Update(commandPerformer); 
                }
            }
        }
        
        private bool TryGetCommandPerformer
            (string unparsedCommand,
             out ICommandPerformer commandPerformer)
        {
            if (unparsedCommand == "REPORT")
            {
                commandPerformer = this.commandPerformerFactory.CreateReportPerformer();
                return true;
            }
            else if (unparsedCommand == "MOVE")
            {
                commandPerformer = this.commandPerformerFactory.CreateMovePerformer();
                return true;
            }
            else if (unparsedCommand == "LEFT")
            {
                commandPerformer = this.commandPerformerFactory.CreateTurnPerformer
                    (TurnDirection.Left);
                return true;
            }
            else if (unparsedCommand == "RIGHT")
            {
                commandPerformer = this.commandPerformerFactory.CreateTurnPerformer
                    (TurnDirection.Right);
                return true;
            }
            else
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
                var orientation = this.orientationFactory.Create(unparsedState[2]);
                if (!orientation.IsValid())
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
}
