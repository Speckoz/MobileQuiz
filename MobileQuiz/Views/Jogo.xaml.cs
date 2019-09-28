﻿using MobileQuiz.Helpers;
using MobileQuiz.Models;
using MobileQuiz.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MobileQuiz.Services.QuestionService;

namespace MobileQuiz.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Jogo : ContentPage
    {
        private string Category { get; set; }
        private int ActualRound { get; set; }
        public int Points { get; set; }

        public Jogo(string category, int round = 0, int points = 10)
        {
            QuestionModel question = category == "Todas" ? GetRandomQuestion() : GetRandomQuestion(category);
            InitializeComponent();
            Mount(question, round, points);
            this.Category = category;
            this.ActualRound = round;
            this.Points = points;
        }

        private void Mount(QuestionModel question, int round, int pontos)
        {
            CreateButtons(question);
            CreateTitle(question.Question);
            RenderInfos(round, pontos);
        }

        private void CreateTitle(string question)
        {
            var label = (Label)FindByName("Title");
            label.Text = question;
        }

        private void CreateButtons(QuestionModel question)
        {
            foreach (var answer in GetAnswersFromQuestion(question))
            {
                var btn = CreateAnswerButton(answer, question);
            }
        }

        private async void CheckAnswer(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            if (btn.ClassId == "correct")
            {
                await DisplayAlert("Parabéns", "Você acertou!", "OK");
                NextLevel(++this.ActualRound, (this.Points * 2));
            }
            else
            {
                await DisplayAlert("Fim de jogo", $"Você Perdeu!\nVocê fez: {this.Points} pontos", "OK");
                GameOver();
            }
        }

        private Button CreateAnswerButton(string answer, QuestionModel question)
        {
            Button btn = new Button()
            {
                Text = answer,
                FontSize = 19,
            };

            if (answer == question.CorrectAnswer) btn.ClassId = "correct";

            // Add handler
            btn.Clicked += CheckAnswer;

            MyButtons.Children.Add(btn);

            return btn;
        }

        private List<string> GetAnswersFromQuestion(QuestionModel question)
        {
            List<string> answerList = question.IncorrectAnswers.Split('/').ToList();
            answerList.Add(question.CorrectAnswer);
            return answerList.Randomize().ToList();
        }

        private void RenderInfos(int round, int points)
        {
            Label roundLabel = (Label)FindByName("Round");
            Label pointsLabel = (Label)FindByName("Pontos");

            roundLabel.Text = $"Round: {round.ToString()}";
            pointsLabel.Text = $"Pontos: {points.ToString()}";
        }

        private void NextLevel(int round, int points)
        {
            Application.Current.MainPage = new Jogo(this.Category, round, points);
        }

        private void GameOver()
        {
            Application.Current.MainPage = new EscolherCategoria();
        }
    }
}