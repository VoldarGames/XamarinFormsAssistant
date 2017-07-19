using System.Collections.Generic;
using Xamarin.Forms;
using XamarinFormsAssistant.Assistant.Global.Styles;

namespace XamarinFormsAssistant.Assistant.Views.Components
{
    public class AssistantCube : Frame
    {
        public StackLayout FirstStackLayout { get; set; }
        public StackLayout SecondStackLayout { get; set; }
        public StackLayout ThirdStackLayout { get; set; }
        public IList<Button> Buttons = new List<Button>()
        {
            new Button() { BackgroundColor = Color.Snow, WidthRequest = 10, HeightRequest = 10, BorderRadius = 0},
            new Button() { BackgroundColor = Color.Snow, WidthRequest = 10, HeightRequest = 10 , BorderRadius = 0},
            new Button() { BackgroundColor = Color.Snow, WidthRequest = 10, HeightRequest = 10, BorderRadius = 0},
            new Button() { BackgroundColor = Color.Snow, WidthRequest = 10,HeightRequest = 10, BorderRadius = 0},
            new Button() { BackgroundColor = Color.Snow, WidthRequest = 10,HeightRequest = 10, BorderRadius = 0},
            new Button() { BackgroundColor = Color.Snow, WidthRequest = 10,HeightRequest = 10, BorderRadius = 0},
            new Button() { BackgroundColor = Color.Snow, WidthRequest = 10, HeightRequest = 10, BorderRadius = 0},
            new Button() { BackgroundColor = Color.Snow, WidthRequest = 10, HeightRequest = 10, BorderRadius = 0},
            new Button() { BackgroundColor = Color.Snow, WidthRequest = 10, HeightRequest = 10, BorderRadius = 0},
        };

        public AssistantCube()
        {
            Style = AssistantStyles.AssistantCube;
            
            FirstStackLayout = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Horizontal
            };

            SecondStackLayout = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Horizontal
            };

            ThirdStackLayout = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Horizontal
            };

            FirstStackLayout.Children.Add(Buttons[0]);
            FirstStackLayout.Children.Add(Buttons[1]);
            FirstStackLayout.Children.Add(Buttons[2]);

            SecondStackLayout.Children.Add(Buttons[3]);
            SecondStackLayout.Children.Add(Buttons[4]);
            SecondStackLayout.Children.Add(Buttons[5]);

            ThirdStackLayout.Children.Add(Buttons[6]);
            ThirdStackLayout.Children.Add(Buttons[7]);
            ThirdStackLayout.Children.Add(Buttons[8]);



            Content = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    FirstStackLayout,
                    SecondStackLayout,
                    ThirdStackLayout
                }
            };
        }
    }
}