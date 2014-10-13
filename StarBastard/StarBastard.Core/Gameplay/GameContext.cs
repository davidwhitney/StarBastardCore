using System;
using System.Collections.Generic;
using StarBastard.Core.Universe.Systems;

namespace StarBastard.Core.Gameplay
{
    public class GameContext
    {
        private readonly Action<GameContext> _render;
        private int currentRound = 0;
        private int _currentPlayerTurn = 0;
        private Turn _thisTurn = null;
        private Player _currentPlayer = null;

        public Dictionary<int, Player> Players { get; set; }
        public GameBoard Systems { get; private set; }

        public GameContext(GameBoard gameboard, Action<GameContext> render)
        {
            Systems = gameboard;
            _render = render;
            Players = new Dictionary<int, Player>
            {
                {1, new Player(1, "Player 1")},
                {2, new Player(2, "Player 2")}
            };
        }


        public void StartTurn()
        {
            if (_thisTurn != null)
            {
                _thisTurn.commitTurn();
            }

            //start next turn
            if (_currentPlayerTurn == 1)
            {
                _currentPlayerTurn = 2;
            }
            else if (_currentPlayerTurn == 2)
            {
                _currentPlayerTurn = 1;
                currentRound++;
            }

            if (_currentPlayerTurn == 0)
            {
                _currentPlayerTurn = 1;
                currentRound = 1;
            }

            _currentPlayer = Players[_currentPlayerTurn];

            _thisTurn = new Turn(_currentPlayer);
            _thisTurn.tick(this);


            _render(this);
        }
    }
}
