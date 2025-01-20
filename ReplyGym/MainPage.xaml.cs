using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using Microsoft.Maui.Controls;

namespace ReplyGym
{
    public partial class MainPage : ContentPage
    {
        private readonly ScheduleDatabase _dbSchedule;
        private readonly UserDatabase _dbUser;

        // Property per il binding
        public ObservableCollection<Field> Fields { get; set; } = [];

        public MainPage()
        {
            InitializeComponent();

            BindingContext = this; // Imposta la BindingContext

            _dbSchedule = new ScheduleDatabase();
            _dbUser = new UserDatabase();

            InitializeFields();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            foreach (var field in Fields)
            {
                field.SelectedOption = (_dbSchedule.GetScheduleByDay(field.Label)).Course;
            }

            SaveScheduleButton.IsEnabled = false;
            SaveScheduleButton.BackgroundColor = Colors.Gray;
            
            if (_dbUser.GetUserData() != null)
            {
                SendButton.IsEnabled = true;
                SendButton.BackgroundColor = Colors.Green;
            }
            else
            {
                SendButton.IsEnabled = false;
                SendButton.BackgroundColor = Colors.Gray;

                var toast = Toast.Make("Please entry your credentials before sending the email", ToastDuration.Long);
                toast.Show();
            }
        }

        private void InitializeFields()
        {
            Fields.Add(new Field
            {
                Label = "Monday",
                Options = ["No selection", "FUNCTIONAL TRAINING", "GAG", "SALA ATTREZZI - 7:00", "SALA ATTREZZI - 8:00", "SALA ATTREZZI - 12:30", "SALA ATTREZZI - 13:30", "SALA ATTREZZI - 18:15", "SALA ATTREZZI - 19:15" ],
                SelectedOption = (_dbSchedule.GetScheduleByDay("Monday")).Course
            });

            Fields.Add(new Field
            {
                Label = "Tuesday",
                Options = ["No selection", "FIT BOXE", "TOTAL BODY", "YOGA", "SALA ATTREZZI - 7:00", "SALA ATTREZZI - 8:00", "SALA ATTREZZI - 12:30", "SALA ATTREZZI - 13:30", "SALA ATTREZZI - 18:15", "SALA ATTREZZI - 19:15" ],
                SelectedOption = (_dbSchedule.GetScheduleByDay("Tuesday")).Course
            });

            Fields.Add(new Field
            {
                Label = "Wednesday",
                Options = ["No selection", "BODY PUMP", "CROSS TRAINING", "TOTAL BODY", "SALA ATTREZZI - 7:00", "SALA ATTREZZI - 8:00", "SALA ATTREZZI - 12:30", "SALA ATTREZZI - 13:30", "SALA ATTREZZI - 18:15", "SALA ATTREZZI - 19:15" ],
                SelectedOption = (_dbSchedule.GetScheduleByDay("Wednesday")).Course
            });

            Fields.Add(new Field
            {
                Label = "Thursday",
                Options = [ "No selection", "DIFESA PERSONALE", "PILATES", "POWER YOGA", "SALA ATTREZZI - 7:00", "SALA ATTREZZI - 8:00", "SALA ATTREZZI - 12:30", "SALA ATTREZZI - 13:30", "SALA ATTREZZI - 18:15", "SALA ATTREZZI - 19:15" ],
                SelectedOption = (_dbSchedule.GetScheduleByDay("Thursday")).Course
            });

            Fields.Add(new Field
            {
                Label = "Friday",
                Options = [ "No selection", "CROSS TRAINING", "FUNCTIONAL TRAINING", "STEP & TONE", "SALA ATTREZZI - 7:00", "SALA ATTREZZI - 8:00", "SALA ATTREZZI - 12:30", "SALA ATTREZZI - 13:30", "SALA ATTREZZI - 18:15", "SALA ATTREZZI - 19:15" ],
                SelectedOption = (_dbSchedule.GetScheduleByDay("Friday")).Course
            });
        }

        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var field in Fields)
            {
                if (field.SelectedOption != (_dbSchedule.GetScheduleByDay(field.Label)).Course)
                {
                    SaveScheduleButton.IsEnabled = true;
                    SaveScheduleButton.BackgroundColor = Colors.Green;

                    SendButton.IsEnabled = false;
                    SendButton.BackgroundColor = Colors.Gray;

                    break;
                }
            }
        }

        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            // Update del database
            foreach (var field in Fields)
            {
                _dbSchedule.UpdateCourseForDay(field.Label, field.SelectedOption);
            }

            SaveScheduleButton.IsEnabled = false;
            SaveScheduleButton.BackgroundColor = Colors.Gray;

            DisplayAlert("Success!", "Weekly schedule saved successfully.", "OK");

            if (_dbUser.GetUserData() != null)
            {
                SendButton.IsEnabled = true;
                SendButton.BackgroundColor = Colors.Green;
            }
            else
            {
                // Create a toast message that notyfy the user to fill the account page entries
                var toast = Toast.Make("Please entry your credentials before sending the email", ToastDuration.Long);
                toast.Show();
            }
        }

        private async void OnSendButtonClicked(object sender, EventArgs e)
        {
            var user = _dbUser.GetUserData();
            var schedules = _dbSchedule.GetAllSchedules();

            // Destinatario, oggetto e corpo dell'email
            var recipient = "script.python.result@gmail.com";
            var subject = "Prenotazioni Pale Reply";
            var body = "Username: "  + user.Username       + "\n" +
                       "Password: "  + user.Password       + "\n" +
                       "Mail: "      + user.Email          + "\n" +
                       "Monday: "    + schedules[0].Course + "\n" +
                       "Tuesday: "   + schedules[1].Course + "\n" +
                       "Wednesday: " + schedules[2].Course + "\n" +
                       "Thursday: "  + schedules[3].Course + "\n" +
                       "Friday: "    + schedules[4].Course + "\n";

            // Crea l'URI mailto con il destinatario, oggetto e corpo
            string mailtoUri = $"mailto:{recipient}?subject={Uri.EscapeDataString(subject)}&body={Uri.EscapeDataString(body)}";

            // Aprire il client di posta elettronica
            await Launcher.OpenAsync(mailtoUri);

            var toast = Toast.Make("Email is ready to be sent", ToastDuration.Long);
            await toast.Show();
        }
        
    }
}
