using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfMLPomodoro.Foundation;

namespace wpfMLPomodoro.Controller
{
    public class ObservableData : Bindable
    {
        private ObservableCollection<string> _observableA = new ObservableCollection<string>();
        private ObservableCollection<string> _observableB = new ObservableCollection<string>();
        private ObservableCollection<string> _observableAB = new ObservableCollection<string>();
        private ObservableCollection<int> _observableTokens = new ObservableCollection<int>();
        private ObservableCollection<string> _observableDictionaryList = new ObservableCollection<string>();

        public ObservableCollection<string> ObservableA
        {
            get => _observableA;
            set
            {
                _observableA = value;
                PropertyIsChanged();
            }
        }

        public ObservableCollection<string> ObservableB
        {
            get => _observableB;
            set
            {
                _observableB = value;
                PropertyIsChanged();
            }
        }

        public ObservableCollection<string> ObservableAB
        {
            get => _observableAB;
            set
            {
                _observableAB = value;
                PropertyIsChanged();
            }
        }

        public ObservableCollection<int> ObservableTokens
        {
            get => _observableTokens;
            set
            {
                _observableTokens = value;
                PropertyIsChanged();
            }
        }

        public ObservableCollection<string> ObservableDictionaryList
        {
            get => _observableDictionaryList;

            set
            {
                _observableDictionaryList = value;
                PropertyIsChanged();
            }
        }
    }
}
