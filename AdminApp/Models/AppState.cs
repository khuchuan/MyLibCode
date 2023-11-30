namespace AdminApp.Models
{
    public class AppState
    {
        public int AppLoading { get; set; }
        public string CurrentPage { get; set; }

        public AppState()
        {
            AppLoading = 0;
            CurrentPage = "";
        }

    }
}
