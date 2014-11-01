using System;
using System.Collections.Generic;
using System.Linq;

namespace HuntTheWumpus.Game
{
	public class WumpusGame
	{
		Dictionary<int, Dictionary<string, int>> _connections = new Dictionary<int, Dictionary<string, int>>();
		int _currentPlayerRoom = 0;
		int _currentWumpusRoom = 0;

		public void ConnectRoomToEast(int fromRoom, int toRoom)
		{
			ConnectRooms (fromRoom, toRoom, "E");
		}

		public void ConnectRoomToWest(int fromRoom, int toRoom)
		{
			ConnectRooms (fromRoom, toRoom, "W");
		}

		public void ConnectRoomToNorth(int fromRoom, int toRoom)
		{
			ConnectRooms (fromRoom, toRoom, "N");
		}

		public void ConnectRoomToSouth(int fromRoom, int toRoom)
		{
			ConnectRooms (fromRoom, toRoom, "S");
		}

		private void ConnectRooms(int fromRoom, int toRoom, string direction)
		{
			Dictionary<string, int> roomConnections;
			if (!_connections.TryGetValue (fromRoom, out roomConnections)) {
				roomConnections = new Dictionary<string, int> ();
				_connections.Add (fromRoom, roomConnections);
			}

			roomConnections.Add (direction, toRoom);
		}
			
		public void MovePlayer (string direction)
		{
			if (_currentPlayerRoom == 0)
				throw new Exceptions.PlayerNotInRoomException ();

			Dictionary<string, int> roomConnections;
			if (_connections.TryGetValue (_currentPlayerRoom, out roomConnections)) {
				int connectedRoom;
				if (roomConnections.TryGetValue (direction, out connectedRoom))
					_currentPlayerRoom = connectedRoom;
				else
					throw new Exceptions.MissingConnectionException (_currentPlayerRoom, direction);
			}
		}

		public void MovePlayerEast ()
		{
			MovePlayer ("E");
		}

		public void MovePlayerWest ()
		{
			MovePlayer ("W");
		}

		public void MovePlayerNorth ()
		{
			MovePlayer ("N");
		}

		public void MovePlayerSouth ()
		{
			MovePlayer ("S");
		}

		public int PlayerRoom
		{
			get
			{
				if (_currentPlayerRoom == 0)
					throw new Exceptions.PlayerNotInRoomException ();
				return _currentPlayerRoom;
			}
			set
			{
				if (!_connections.ContainsKey (value))
					throw new Exceptions.MissingRoomException (value);

				_currentPlayerRoom = value;
			}
		}

		public int WumpusRoom 
		{
			get 
			{
				if (_currentWumpusRoom == 0)
					throw new Exceptions.WumpusNotInRoomException ();
				return _currentWumpusRoom;
			}
			set
			{
				if (!_connections.ContainsKey (value))
					throw new Exceptions.MissingRoomException (value);

				_currentWumpusRoom = value;
			}
		}

		public bool RoomHasBlood(int room)
		{
			var roomsToCheck = new List<int>();
			GetRoomsToCheck (room, 2, roomsToCheck);
			return roomsToCheck.Any(r => r == _currentWumpusRoom);
		}

		private void GetRoomsToCheck (int room, int connectionLevel, List<int> roomsToCheck)
		{
			Dictionary<string, int> roomConnections;
			if (_connections.TryGetValue (room, out roomConnections)) {
				roomsToCheck.AddRange (roomConnections.Values);
			}

			if((connectionLevel - 1) > 0)
			{
				foreach(var connectedRoom in roomConnections.Values)
					GetRoomsToCheck(connectedRoom, connectionLevel - 1, roomsToCheck);
			}
		}
	}
}

