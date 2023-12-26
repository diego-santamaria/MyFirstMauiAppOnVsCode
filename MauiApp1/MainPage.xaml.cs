namespace MauiApp1;

public partial class MainPage : ContentPage
{
	string translatedNumber;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnTranslate(object sender, EventArgs e)
	{
		string enteredNumber = PhoneNumberText.Text;
		translatedNumber = MauiApp1.PhonewordTranslator.ToNumber(enteredNumber);

		if (!string.IsNullOrEmpty(translatedNumber))
		{
			CallButton.IsEnabled = true;
			CallButton.Text = "Call " + translatedNumber;
		}
		else
		{
			CallButton.IsEnabled = false;
			CallButton.Text = "Call";
		}
	}

	async void OnCall(object sender, EventArgs e)
	{
		if (await this.DisplayAlert(
			"Dial a Number",
			"Would you like to call " + translatedNumber + "?",
			"Yes",
			"No"))
		{
			try
			{
				if(PhoneDialer.Default.IsSupported)
					PhoneDialer.Open(translatedNumber);
			}
			catch (ArgumentNullException)
			{
				await this.DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");
			}
			catch (FeatureNotSupportedException)
			{
				await this.DisplayAlert("Unable to dial", "Phone dialing not supported.", "OK");
			}
			catch (Exception)
			{
				// Other error has occurred.
				await this.DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
			}
		}
	}
}

