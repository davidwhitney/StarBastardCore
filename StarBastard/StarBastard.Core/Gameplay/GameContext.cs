using System;
using System.Collections;
using System.Collections.Generic;
using StarBastard.Core.Universe.Systems;

namespace StarBastard.Core.Gameplay
{
    public class GameContext
    {
        private readonly Action<GameContext> _render;
        private int currentRound = 0;
        private int currentPlayerTurn = 0;
        private Turn thisTurn = null;
        private Player currentPlayer = null;
        private object updateUiFunction;

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
            if (thisTurn != null)
            {
                thisTurn.commitTurn();
            }

            //start next turn
            if (currentPlayerTurn == 1)
            {
                currentPlayerTurn = 2;
            }
            else if (currentPlayerTurn == 2)
            {
                currentPlayerTurn = 1;
                currentRound++;
            }

            if (currentPlayerTurn == 0)
            {
                currentPlayerTurn = 1;
                currentRound = 1;
            }

            currentPlayer = Players[currentPlayerTurn];

            thisTurn = new Turn(currentPlayer);
            thisTurn.tick(this);


            _render(this);
        }
    }
}
