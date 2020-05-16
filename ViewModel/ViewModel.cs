using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model2048;
using System.Windows.Input;
using Xamarin.Forms;

namespace Xamarin2048ViewModel
{
    public class ViewModel: INotifyPropertyChanged
    {
        #region Fields
        private Queue<string> values = new Queue<string>();
        private Model model;
        private int hFieldSize;
        private int vFieldSize;
        private int targetValue;
        private int score;
        #endregion

        #region Properties
        public int TargetValue
        {
            get
            {
                return this.targetValue;
            }
            set
            {
                if ((value != this.targetValue) && (value > 0) && (value % 2 == 0))
                    this.targetValue = value;
                OnPropertyChanged("TargetValue");
            }
        }
        public int HFieldSize
        {
            get
            {
                return this.hFieldSize;
            }
            set
            {
                if ((value > 0) && (value != this.hFieldSize)) this.hFieldSize = value;
                OnPropertyChanged("HFieldSize");
            }
        }
        public int VFieldSize
        {
            get
            {
                return this.vFieldSize;
            }
            set
            {
                if ((value > 0) && (value != this.vFieldSize)) this.vFieldSize = value;
                OnPropertyChanged("VFieldSize");
            }
        }
        public string FieldValue
        {
            get
            {
                if (values.Count > 0)
                    return this.values.Dequeue();
                else
                    return "err";
            }
        }
        public int Score
        {
            get 
            {
                return this.score;
            }
            private set
            {
                if (this.score != value)
                    this.score = value;
            }
        }
        #endregion

        #region Commands
        public ICommand NewGame { get; }
        /*public ICommand SwipedLeft { get; }
        public ICommand SwipedRight { get; }
        public ICommand SwipedUp { get; }
        public ICommand SwipedDown { get; }*/
        public ICommand Swiped { get;  }
        #endregion

        #region Methods
        public ViewModel()
        {
            this.VFieldSize = 4;
            this.HFieldSize = 4;
            this.TargetValue = 2048;
            this.NewGame = new Command(StartNewGame);
            this.Swiped = new Command<Actions>(UserAction);
            StartNewGame();
        }

        public void StartNewGame()
        {
            values = new Queue<string>();
            this.model = new Model(HFieldSize, VFieldSize);
            UpdateValues();
            UpdateScore();
        }
        public void UpdateValues()
        {
            values.Clear();
            for (int i = 0; i < this.model.VSize; i++)
                for (int j = 0; j < this.model.HSize; j++)
                {
                    int value = this.model.Get(new Coordinates(j, i));
                    if (value > 0)
                        this.values.Enqueue(value.ToString());
                    else
                        this.values.Enqueue("");
                }
            OnPropertyChanged("FieldValue");
        }

        public void UpdateScore()
        {
            if (this.Score != this.model.Score)
            {
                this.Score = this.model.Score;
                OnPropertyChanged("Score");
            }
            OnPropertyChanged("Score");
        }

        public void UserAction(Actions action)
        {
            switch (action)
            {
                case Actions.Down:
                    this.model.Action(Model.Directions.Down);
                    break;
                case Actions.Up:
                    this.model.Action(Model.Directions.Up);
                    break;
                case Actions.Left:
                    model.Action(Model.Directions.Left);
                    break;
                case Actions.Right:
                    this.model.Action(Model.Directions.Right);
                    break;
            }
            UpdateValues();
            UpdateScore();
        }


        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
