using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using StarBastard.Core.Universe.Systems;

namespace StarBastard.Windows.Prototype.Rendering
{
    public class WinformsRenderer
    {
        private const int BorderOffset = 50;
        private const int PieceSize = 45;
        private const int Scale = 1;
        private const int ScaledPieceSize = PieceSize*Scale;

        public Action<TileEnvelope, Panel> TileClicked { get; set; }

        private readonly Dictionary<Panel, TileEnvelope> _lookup;

        public WinformsRenderer()
        {
            TileClicked = (loc, ctrl) => { };
            _lookup = new Dictionary<Panel, TileEnvelope>();
        }

        public void Render(GameBoard gameBoard, Panel targetPanel, Label messages)
        {
            foreach (var planet in gameBoard)
            {
                var thisLocation = new TileEnvelope(planet.Location.X, planet.Location.Y, planet);
                var drawPosX = (planet.Location.X * ScaledPieceSize) + BorderOffset + CalculateHexOffset(gameBoard, planet);
                var drawPosY = (planet.Location.Y * ScaledPieceSize) + BorderOffset;

                Panel panel;
                if (_lookup.ContainsValue(thisLocation))
                {
                    panel = _lookup.Single(_ => _.Value.Equals(thisLocation)).Key;
                }
                else
                {
                    panel = new Panel { Location = new Point(drawPosX, drawPosY) };
                    panel.Click += panel_Click;

                    _lookup.Add(panel, thisLocation);
                    targetPanel.Controls.Add(panel);
                }

                panel.Width = ScaledPieceSize;
                panel.Height = ScaledPieceSize;
                panel.Controls.Add(new Label{Text = planet.PlanetId});

                RefreshTile(panel, planet);
            }
        }

        public int CalculateHexOffset(GameBoard gameBoard, Planet current)
        {
            var boxesOnThisLine = gameBoard.Count(x => x.Location.Y == current.Location.Y);
            switch (boxesOnThisLine)
            {
                case 4:
                    return (int)(ScaledPieceSize * 1.5);
                case 5:
                    return ScaledPieceSize * 1;
                case 6:
                    return (int)(ScaledPieceSize * 0.5);
                default:
                    return 0;
            }
        }

        public void RefreshTile(Panel panel, Planet planet)
        {
            panel.BackColor = DetermineTileColour(planet);
            panel.BorderStyle = BorderStyle.None;
        }

        private static Color DetermineTileColour(Planet boardTile)
        {
            return Color.Gray;
            /*Color colour;

            if (boardTile.IsOccupied)
            {
                if (boardTile.Occupant is DefenderKing)
                {
                    colour = Color.BlanchedAlmond;
                }
                else if (boardTile.Occupant is Defender)
                {
                    colour = Color.White;
                }
                else if (boardTile.Occupant is Attacker)
                {
                    colour = Color.Black;
                }
                else
                {
                    colour = Color.LightGreen;
                }
            }
            else
            {
                switch (boardTile.TileType)
                {
                    case TileType.Castle:
                        colour = Color.Silver;
                        break;
                    case TileType.AttackerTerritory:
                        colour = Color.DarkGreen;
                        break;
                    default:
                        colour = Color.LightGreen;
                        break;
                }
            }

            return colour;*/
        }

        private void panel_Click(object sender, EventArgs e)
        {
            var location = _lookup.Single(x => x.Key == sender).Value;
            TileClicked(location, (sender as Panel));
        }
    }
}