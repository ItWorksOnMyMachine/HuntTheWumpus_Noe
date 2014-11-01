using System;

namespace HuntTheWumpus.Game.Exceptions
{
	public class MissingRoomException : Exception 
	{
		public int MissingRoomNumber { get; private set; }
		public MissingRoomException(int room)
		{
			MissingRoomNumber = room;
		}
	}

	public class MissingConnectionException : Exception 
	{
		public int Room { get; private set; }
		public string MissingDirection { get; private set; }
		public MissingConnectionException(int room, string direction)
		{
			Room = room;
			MissingDirection = direction;
		}
	}

	public class PlayerNotInRoomException : Exception {}
	public class WumpusNotInRoomException : Exception {}
}

