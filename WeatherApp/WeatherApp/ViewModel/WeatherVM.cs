﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using WeatherApp.Model;

namespace WeatherApp.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        private string query;

        public string Query
        {
            get { return query; }
            set 
            { 
                query = value;
                OnPropertyChanged("Query");
            }
        }

        private CurrentCondition currentConditions;

        public CurrentCondition CurrentConditions
        {
            get { return currentConditions; }
            set 
            { 
                currentConditions = value;
                OnPropertyChanged("CurrentConditions");
            }
        }


        private City selectedCity;

        public City SelectedCity
        {
            get { return selectedCity; }
            set 
            { 
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
            }
        }

        public WeatherVM()
        {
            if(DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = new City
                {
                    LocalizedName = "New York"
                };

                CurrentConditions = new CurrentCondition
                {
                    WeatherText = "Partly Cloudy",
                    Temperature = new Temperature
                    {
                        Metric = new Units
                        {
                            Value = 21
                        }
                    }
                };
            }
            
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}