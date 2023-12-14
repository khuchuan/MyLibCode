using AdminApp.Services;
using AntDesign;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Pages.HomePage
{
    public partial class HomePage
    {
        [Inject] IAppStateService _appStateService { get; set; }
        [Inject] MessageService _messageService { get; set; }
        
        [Inject] private ILogger<HomePage> logger { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
    }
}
