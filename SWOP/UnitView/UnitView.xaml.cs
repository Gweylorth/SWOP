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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SmallWorld;

namespace SWOP
{
    /// <summary>
    /// Logique d'interaction pour UnitView.xaml
    /// </summary>
    public partial class UnitView : UserControl
    {
        public IUnit Unit { get; private set; }
        private TileView ParentTile { get; set; }
        
        public UnitView(IUnit u)
        {
            InitializeComponent();
            this.Unit = u;
            this.ParentTile = MainWindow.INSTANCE.MapView.TilesView[this.Unit.Position];
        }

        private void OnUnitLoaded(object sender, RoutedEventArgs e)
        {
           this.SetAppearance();
        }

        /// <summary>
        /// Sets appearance depending on Faction
        /// (and UnitType in the future ?)
        /// </summary>
        public void SetAppearance()
        {
            switch(this.Unit.Faction)
            {
                case FactionName.Vikings:
                    this.selectedSquare.Stroke = (SolidColorBrush) Resources["VikingsColor"];
                    this.sprite.Source = (BitmapImage) Resources["VikingsImg"];
                    break;

                case FactionName.Gauls:
                    this.selectedSquare.Stroke = (SolidColorBrush) Resources["GaulsColor"];
                    this.sprite.Source = (BitmapImage) Resources["GaulsImg"];
                    break;

                case FactionName.Dwarves:
                    this.selectedSquare.Stroke = (SolidColorBrush) Resources["DwarvesColor"];
                    this.sprite.Source = (BitmapImage) Resources["DwarvesImg"];
                    break;
            }
        }

        /// <summary>
        /// Updates appearance if unit selected/idle
        /// Shows SelectionRectangle and toggles animation
        /// </summary>
        public void UpdateAppearance()
        {
            Storyboard rectangleOpacityAnim = (Storyboard) this.grid.FindResource("rectangleOpacity");

            if (this.Unit.State == UnitState.Selected)
            {
                this.selectedSquare.Visibility = Visibility.Visible;
                rectangleOpacityAnim.Begin(this);
            }
            else
            {
                this.selectedSquare.Visibility = Visibility.Hidden;
                rectangleOpacityAnim.Stop(this);
            }
            
        }

        /// <summary>
        /// UI update on Unit selection
        /// </summary>
        public void Select()
        {
            if (this.Unit.Faction != MainWindow.INSTANCE.GM.CurrentGame.GetCurrentPlayer().CurrentFaction.Name)
                return;

            if (MainWindow.INSTANCE.ActiveUnitView != null)
            {
                MainWindow.INSTANCE.ActiveUnitView.Unit.ChangeState(UnitState.Idle);
                MainWindow.INSTANCE.ActiveUnitView.UpdateAppearance();
            }
            MainWindow.INSTANCE.ActiveUnitView = this;

            this.Unit.ChangeState(UnitState.Selected);
            this.UpdateAppearance();

            MainWindow.INSTANCE.unitName.Text = this.Unit.Name;
            MainWindow.INSTANCE.unitHp.Text = "HP : " + this.Unit.Hp.ToString();
            MainWindow.INSTANCE.unitMvt.Text = "MVT : " + this.Unit.Mvt.ToString();
            MainWindow.INSTANCE.unitAtk.Text = "ATK : " + this.Unit.Atk.ToString();
            MainWindow.INSTANCE.unitDef.Text = "DEF : " + this.Unit.Def.ToString();
            MainWindow.INSTANCE.unitImg.Source = this.sprite.Source;
            MainWindow.INSTANCE.selectedUnit.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// UI update on Unit move
        /// </summary>
        public void Move()
        {
            this.ParentTile.grid.Children.Remove(this);
            TileView newTileView = MainWindow.INSTANCE.MapView.TilesView[this.Unit.Position];
            newTileView.grid.Children.Add(this);
            this.ParentTile = newTileView;
        }
    }
}
