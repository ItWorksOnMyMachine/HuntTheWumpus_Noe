using System;
using NUnit;
using NUnit.Framework;
using HuntTheWumpus.Game;
using HuntTheWumpus.Game.Exceptions;

namespace HuntTheWumpus.Tests
{
	[TestFixture ()]
	public class GameTests
	{
		[TestCase ()]
		public void EnsureExceptionThrownIfPlayerNotInRoomAndCurrentPlayerRoomRequested ()
		{
			WumpusGame g = new WumpusGame ();
			Assert.Throws<PlayerNotInRoomException>(() => {var room = g.PlayerRoom;});
		}

		[TestCase ()]
		public void EnsureExceptionThrownIfWumpusNotInRoomAndCurrentWumpusRoomRequested ()
		{
			WumpusGame g = new WumpusGame ();
			Assert.Throws<WumpusNotInRoomException>(() => {var room = g.WumpusRoom;});
		}

		[TestCase ()]
		public void TestMoveWestToEast ()
		{
			WumpusGame g = new WumpusGame ();
			g.ConnectRoomToEast (4, 5);
			g.PlayerRoom = 4;
			g.MovePlayerEast ();
			Assert.AreEqual (5, g.PlayerRoom);
		}

		[TestCase ()]
		public void TestMoveEastToWest ()
		{
			WumpusGame g = new WumpusGame ();
			g.ConnectRoomToWest (5, 4);
			g.PlayerRoom = 5;
			g.MovePlayerWest ();
			Assert.AreEqual (4, g.PlayerRoom);
		}

		[TestCase ()]
		public void TestMoveNorthToSouth ()
		{
			WumpusGame g = new WumpusGame ();
			g.ConnectRoomToSouth (5, 11);
			g.PlayerRoom = 5;
			g.MovePlayerSouth ();
			Assert.AreEqual (11, g.PlayerRoom);
		}

		[TestCase ()]
		public void TestMoveSouthToNorth ()
		{
			WumpusGame g = new WumpusGame ();
			g.ConnectRoomToNorth (11, 5);
			g.PlayerRoom = 11;
			g.MovePlayerNorth ();
			Assert.AreEqual (5, g.PlayerRoom);
		}

		[TestCase ()]
		public void TestPlayerCantBePutInInvalidRoom ()
		{
			WumpusGame g = new WumpusGame ();
			Assert.Throws<MissingRoomException>(() => g.PlayerRoom = 4 );
		}

		[TestCase ()]
		public void TestPlayerCantBeMovedInInvalidEastWestDirection ()
		{
			WumpusGame g = new WumpusGame ();
			g.ConnectRoomToSouth (5, 11);
			g.ConnectRoomToNorth (11, 5);
			g.PlayerRoom = 11;
			Assert.Throws<MissingConnectionException>(() => g.MovePlayerEast() );
			Assert.Throws<MissingConnectionException>(() => g.MovePlayerWest() );
		}
		[TestCase ()]
		public void TestPlayerCantBeMovedInInvalidNorthSouthDirection ()
		{
			WumpusGame g = new WumpusGame ();
			g.ConnectRoomToEast (4, 5);
			g.ConnectRoomToWest (5, 4);
			g.PlayerRoom = 5;
			Assert.Throws<MissingConnectionException>(() => g.MovePlayerNorth() );
			Assert.Throws<MissingConnectionException>(() => g.MovePlayerSouth() );
		}

		[TestCase ()]
		public void TestPlayerCantBeMovedWhenNotInARoom ()
		{
			WumpusGame g = new WumpusGame ();
			Assert.Throws<PlayerNotInRoomException>(() => g.MovePlayerNorth() );
			Assert.Throws<PlayerNotInRoomException>(() => g.MovePlayerSouth() );
			Assert.Throws<PlayerNotInRoomException>(() => g.MovePlayerEast() );
			Assert.Throws<PlayerNotInRoomException>(() => g.MovePlayerWest() );
		}

		private void AddRoomAndConnections(WumpusGame game, int fromRoom, int eastRoom, int southRoom, int westRoom, int northRoom)
		{
			game.ConnectRoomToEast (fromRoom, eastRoom);
			game.ConnectRoomToWest (fromRoom, westRoom);
			game.ConnectRoomToNorth (fromRoom, northRoom);
			game.ConnectRoomToSouth (fromRoom, southRoom);
		}

		private void AddAllRoomsToGame(WumpusGame game)
		{
			AddRoomAndConnections (game, 1, 2, 6, 5, 33);
			AddRoomAndConnections (game, 2, 3, 6, 1, 34);
			AddRoomAndConnections (game, 3, 35, 7, 2, 35);
			AddRoomAndConnections (game, 4, 5, 10, 36, 36);
			AddRoomAndConnections (game, 5, 1, 11, 4, 33);
			AddRoomAndConnections (game, 6, 2, 12, 11, 1);
			AddRoomAndConnections (game, 7, 8, 14, 13, 3);
			AddRoomAndConnections (game, 8, 9, 15, 7, 9);
			AddRoomAndConnections (game, 9, 10, 16, 8, 8);
			AddRoomAndConnections (game, 10, 4, 17, 9, 35);
			AddRoomAndConnections (game, 11, 6, 19, 18, 5);
			AddRoomAndConnections (game, 12, 13, 20, 19, 6);
			AddRoomAndConnections (game, 13, 14, 21, 12, 7);
			AddRoomAndConnections (game, 14, 15, 22, 13, 7);
			AddRoomAndConnections (game, 15, 16, 23, 14, 8);
			AddRoomAndConnections (game, 16, 17, 24, 15, 9);
			AddRoomAndConnections (game, 17, 18, 25, 16, 10);
			AddRoomAndConnections (game, 18, 19, 26, 17, 11);
			AddRoomAndConnections (game, 19, 12, 27, 18, 11);
			AddRoomAndConnections (game, 20, 21, 28, 27, 12);
			AddRoomAndConnections (game, 21, 22, 29, 20, 13);
			AddRoomAndConnections (game, 22, 23, 29, 21, 14);
			AddRoomAndConnections (game, 23, 24, 30, 22, 15);
			AddRoomAndConnections (game, 24, 25, 30, 23, 16);
			AddRoomAndConnections (game, 25, 26, 31, 24, 17);
			AddRoomAndConnections (game, 26, 27, 32, 25, 18);
			AddRoomAndConnections (game, 27, 20, 32, 26, 19);
			AddRoomAndConnections (game, 28, 29, 33, 32, 20);
			AddRoomAndConnections (game, 29, 22, 34, 28, 21);
			AddRoomAndConnections (game, 30, 31, 36, 23, 24);
			AddRoomAndConnections (game, 31, 32, 36, 30, 25);
			AddRoomAndConnections (game, 32, 27, 28, 31, 26);
			AddRoomAndConnections (game, 33, 34, 1, 5, 28);
			AddRoomAndConnections (game, 34, 35, 2, 33, 29);
			AddRoomAndConnections (game, 35, 3, 3, 34, 10);
			AddRoomAndConnections (game, 36, 4, 4, 30, 31);
		}

		[TestCase ()]
		public void TestWumpusIsInRoom()
		{
			var g = new WumpusGame ();
			AddAllRoomsToGame (g);
			g.WumpusRoom = 14;
			Assert.AreEqual (14, g.WumpusRoom);
		}

		[TestCase ()]
		public void TestWumpusCantBePutInInvalidRoom ()
		{
			WumpusGame g = new WumpusGame ();
			Assert.Throws<MissingRoomException>(() => g.WumpusRoom = 4);
		}

		[TestCase ()]
		public void TestBloodIsInCorrectLocation()
		{
			var g = new WumpusGame ();
			AddAllRoomsToGame (g);
			g.WumpusRoom = 14;
			Assert.AreEqual (false, g.RoomHasBlood (1));
			Assert.AreEqual (false, g.RoomHasBlood (2));
			Assert.AreEqual (true, g.RoomHasBlood (3));
			Assert.AreEqual (false, g.RoomHasBlood (4));
			Assert.AreEqual (false, g.RoomHasBlood (5));
			Assert.AreEqual (false, g.RoomHasBlood (6));
			Assert.AreEqual (true, g.RoomHasBlood (7));
			Assert.AreEqual (true, g.RoomHasBlood (8));
			Assert.AreEqual (false, g.RoomHasBlood (9));
			Assert.AreEqual (false, g.RoomHasBlood (10));
			Assert.AreEqual (false, g.RoomHasBlood (11));
			Assert.AreEqual (true, g.RoomHasBlood (12));
			Assert.AreEqual (true, g.RoomHasBlood (13));
			Assert.AreEqual (true, g.RoomHasBlood (14));
			Assert.AreEqual (true, g.RoomHasBlood (15));
			Assert.AreEqual (true, g.RoomHasBlood (16));
			Assert.AreEqual (false, g.RoomHasBlood (17));
			Assert.AreEqual (false, g.RoomHasBlood (18));
			Assert.AreEqual (false, g.RoomHasBlood (19));
			Assert.AreEqual (false, g.RoomHasBlood (20));
			Assert.AreEqual (true, g.RoomHasBlood (21));
			Assert.AreEqual (true, g.RoomHasBlood (22));
			Assert.AreEqual (true, g.RoomHasBlood (23));
			Assert.AreEqual (false, g.RoomHasBlood (24));
			Assert.AreEqual (false, g.RoomHasBlood (25));
			Assert.AreEqual (false, g.RoomHasBlood (26));
			Assert.AreEqual (false, g.RoomHasBlood (27));
			Assert.AreEqual (false, g.RoomHasBlood (28));
			Assert.AreEqual (true, g.RoomHasBlood (29));
			Assert.AreEqual (false, g.RoomHasBlood (30));
			Assert.AreEqual (false, g.RoomHasBlood (31));
			Assert.AreEqual (false, g.RoomHasBlood (32));
			Assert.AreEqual (false, g.RoomHasBlood (33));
			Assert.AreEqual (false, g.RoomHasBlood (34));
			Assert.AreEqual (false, g.RoomHasBlood (35));
			Assert.AreEqual (false, g.RoomHasBlood (36));
		}

	}
}

