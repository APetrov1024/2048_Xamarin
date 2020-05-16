using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace _2048_Xamarin
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        ViewModel ViewModel;
        public MainPage()
        {
            InitializeComponent();
            this.ViewModel = new ViewModel();
            CreateTiles();
            this.BindingContext = this.ViewModel;
        }

        private void CreateTiles()
        {
            int hFieldSize = this.ViewModel.HFieldSize;
            int vFieldSize = this.ViewModel.VFieldSize;
            for (int i = 0; i < hFieldSize; i++)
            {
                ColumnDefinition colDef = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
                FieldView.ColumnDefinitions.Add(colDef);
            }
            for (int i = 0; i < vFieldSize; i++)
            {
                RowDefinition rowDef = new RowDefinition { Height = new GridLength(1, GridUnitType.Star) };
                FieldView.RowDefinitions.Add(rowDef);
            }
            for (int i = 0; i < vFieldSize; i++)
                for (int j = 0; j < hFieldSize; j++)
                {
                    Label tile = new Label();
                    Binding binding = new Binding { Path = "FieldValue"};
                    tile.SetBinding(Label.TextProperty, binding);
                    tile.Style = this.Resources["TileStyle"] as Style;
                    Binding heighBinding = new Binding { Source = tile, Path = "Width" };
                    tile.SetBinding(Label.HeightRequestProperty, heighBinding);
                    Grid.SetRow(tile, i);
                    Grid.SetColumn(tile, j);
                    FieldView.Children.Add(tile);
                }
        }

        private void OnFieldSwipedDown(object sender, SwipedEventArgs e)
        {
            this.ViewModel.UserAction(ViewModel.Actions.Down);
        }
        private void OnFieldSwipedUp(object sender, SwipedEventArgs e)
        {
            this.ViewModel.UserAction(ViewModel.Actions.Up);
        }
        private void OnFieldSwipedRight(object sender, SwipedEventArgs e)
        {
            this.ViewModel.UserAction(ViewModel.Actions.Right);
        }
        private void OnFieldSwipedLeft(object sender, SwipedEventArgs e)
        {
            this.ViewModel.UserAction(ViewModel.Actions.Left);
        }
    }
}
