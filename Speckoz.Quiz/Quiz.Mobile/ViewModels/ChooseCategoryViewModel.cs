﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Quiz.Dependencies.Enums;
using Quiz.Helpers;
using Quiz.Mobile.Properties;
using Quiz.Models;
using Quiz.Views;

using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;

using XF.Material.Forms;
using XF.Material.Forms.Resources;

namespace Quiz.ViewModels
{
    internal class ChooseCategoryViewModel : ViewModelBase
    {
        private ImageSource __image = ConvertImageHelper.Convert(Resources.choose);
        private ObservableCollection<ChooseCategoryModel> __chooseCategories;

        public ObservableCollection<ChooseCategoryModel> ChooseCategories
        {
            get => __chooseCategories;
            set
            {
                __chooseCategories = value;
                RaisePropertyChanged();
            }
        }

        public ImageSource Image
        {
            get => __image;
            set
            {
                __image = value;
                RaisePropertyChanged();
            }
        }

        public ChooseCategoryViewModel() => CreateButtonsChoose();

        private void CreateButtonsChoose()
        {
            ChooseCategories = new ObservableCollection<ChooseCategoryModel>
            {
                new ChooseCategoryModel
                {
                    BackgroundColor = Material.GetResource<Color>(MaterialConstants.Color.PRIMARY),
                    PaddingButton = new Thickness(0,30,0,20),
                    ChooseAnswerCommand = new RelayCommand<Button>(CategoryChosenAsync),
                    TextButton = CategoryEnum.Todas
                },
                CreateButtonGray(CategoryEnum.Arte),
                CreateButtonGray(CategoryEnum.Ciencia),
                CreateButtonGray(CategoryEnum.Esporte),
                CreateButtonGray(CategoryEnum.Geograria),
                CreateButtonGray(CategoryEnum.Historia)
            };
        }

        private ChooseCategoryModel CreateButtonGray(CategoryEnum category)
        {
            return new ChooseCategoryModel
            {
                ChooseAnswerCommand = new RelayCommand<Button>(CategoryChosenAsync),
                TextButton = category
            };
        }

        private async void CategoryChosenAsync(Button bt)
        {
            if (Enum.TryParse(bt.Text, out CategoryEnum result))
                await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new GameView(result)), true);
        }
    }
}