namespace ReplyGym;

public partial class AccountPage : ContentPage
{
    private bool isUserValid;
    private bool isPasswordValid;
    private bool isEmailValid;

    private readonly UserDatabase _database;

	public AccountPage()
	{
		InitializeComponent();

        _database = new UserDatabase();
        
        isUserValid = false;
        isPasswordValid = false;
        isEmailValid = false;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        LoadUserData();
    }

    private void LoadUserData()
    {
        var userData = _database.GetUserData();

        // Se ci sono dati, carica nelle Entry
        if (userData != null)
        {
            username.Text = userData.Username;
            password.Text = userData.Password;
            email.Text = userData.Email;

            SaveButton.IsEnabled = false;
            SaveButton.BackgroundColor = Colors.Gray;
        }
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        isUserValid = !string.IsNullOrWhiteSpace(username.Text);
        isPasswordValid = !string.IsNullOrWhiteSpace(password.Text);
        isEmailValid = !string.IsNullOrWhiteSpace(email.Text);

        var userData = new UserData
        {
            Username = username.Text,
            Password = password.Text,
            Email = email.Text,
        };

        if (isUserValid & isPasswordValid & isEmailValid & userData != _database.GetUserData())
        {
            SaveButton.IsEnabled = true;
            SaveButton.BackgroundColor = Colors.Green;
        }
        else
        {
            SaveButton.IsEnabled = false;
            SaveButton.BackgroundColor = Colors.Gray;
        }
    }

    private void OnClickedSaveButton(object sender, EventArgs e)
    {
        var userData = new UserData
        {
            Username = username.Text,
            Password = password.Text,
            Email = email.Text
        };

        _database.SaveUserData(userData);

        SaveButton.IsEnabled = false;
        SaveButton.BackgroundColor = Colors.Gray;

        // Feedback per l'utente
        DisplayAlert("Success!", "Account data saved successfully.", "OK");
    }
}