using AdminApp.Models;
using System;

namespace AdminApp.Services
{
    public interface IAppStateService
    {
        void SetLoading(bool isLoading);
        void SetCurrentPage(string curentPage);
        bool IsLoading { get; }
        string CurrentPage { get; }
        event Action OnChangeAppLoading;
    }
    public class AppStateService : IAppStateService
    {
        public AppState appState = new AppState();
        public event Action OnChangeAppLoading;
        private void NotifyStateChanged() => OnChangeAppLoading?.Invoke();
        public bool IsLoading
        {
            get
            {
                return appState.AppLoading > 0;
            }
        }
        public void SetLoading(bool isLoading)
        {
            if (isLoading)
            {
                appState.AppLoading += 1;
            }
            else
            {
                appState.AppLoading -= 1;
            }
            NotifyStateChanged();
        }
        public string CurrentPage
        {
            get
            {
                return appState.CurrentPage;
            }
        }
        public void SetCurrentPage(string curentPage)
        {
            appState.CurrentPage = curentPage;
            NotifyStateChanged();
        }

    }
}
