using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CareOnDemand.ViewModels
{
    class ServiceSelectedDetailsViewModel : BaseServiceViewModel
    {
        public ServiceSelectedDetailsViewModel()
        {
            DurationList = new ObservableCollection<Duration>();
            selected_duration = new Duration();
            PopulateDurationList();
        }

        public ObservableCollection<Duration> DurationList { get; set; }
        protected static Duration selected_duration;

        public Duration SelectedDuration
        {
            get => selected_duration;
            set
            {
                selected_duration = value;
                OnPropertyChanged(nameof(SelectedDuration));
            }
        }

        public void PopulateDurationList()
        {
            if (service.Length == 1)
            {
                for (int i = 1; i <= 3; i++)
                {
                    DurationList.Add(new Duration { Time = i, TimeSentence = i + " hours", Price = service.ServicePrice * i });
                }
            }
        }
        public class Duration
        {
            public int Time { get; set; }
            public string TimeSentence { get; set; }
            public int Price { get; set; }
        }

    }
}
