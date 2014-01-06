﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SmallWorld;

namespace SWOP
{
    /// <summary>
    /// Logique d'interaction pour GameCreator.xaml
    /// </summary>
    public partial class PlayerCreator : UserControl
    {
        private static int nbCreatedPlayers = 0;
        private static Color factionIdleColor = Color.FromRgb(150, 150, 150);
        private static Color factionSelectedColor = Color.FromRgb(150, 150, 64);

        public int mapSize = 1; // tmp
        public int playerId;
        public bool isReady = false;
        public Color playerColor;
        public FactionName factionChosen;


        public PlayerCreator()
        {
            InitializeComponent();
        }

        /// <summary>
        /// End current player turn
        /// </summary>
        private void btnNextPlayer_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.INSTANCE.GM.CurrentGame.NextPlayer(); // Next turn
        }


        private void btnViking_Click(object sender, RoutedEventArgs e)
        {
            factionChosen = FactionName.Vikings;
            RefreshUI();
        }

        private void btnGaul_Click(object sender, RoutedEventArgs e)
        {
            factionChosen = FactionName.Gauls;
            RefreshUI();
        }

        private void btnDwarf_Click(object sender, RoutedEventArgs e)
        {
            factionChosen = FactionName.Dwarves;
            RefreshUI();
        }


        private void RefreshUI()
        {
            btnViking.Background = new SolidColorBrush((factionChosen == FactionName.Vikings) ? factionSelectedColor : factionIdleColor);
            btnGaul.Background = new SolidColorBrush((factionChosen == FactionName.Gauls) ? factionSelectedColor : factionIdleColor);
            btnDwarf.Background = new SolidColorBrush((factionChosen == FactionName.Dwarves) ? factionSelectedColor : factionIdleColor);

            //imgViking.Visibility = (factionChosen == FactionName.Vikings) ? Visibility.Visible : Visibility.Hidden;
            //imgGaul.Visibility = (factionChosen == FactionName.Gauls) ? Visibility.Visible : Visibility.Hidden;
            //imgDwarf.Visibility = (factionChosen == FactionName.Dwarves) ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
