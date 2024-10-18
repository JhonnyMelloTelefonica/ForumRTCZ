using ForumRTCZ.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging.Abstractions;
using Shared_Static_Class.Data;
using Shared_Static_Class.Model_DTO;
using Shared_Static_Class.Model_ForumRTCZ_Context;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static ForumRTCZ.ViewModels.ForumRTCZViewModel;

namespace ForumRTCZ.Pages
{
    public partial class PostPublicacao : ComponentBase, IDisposable
    {
        [CascadingParameter] public PUBLICACAO_SOLICITACAODTO Model { get; set; } = null;
        private IEnumerable<JORNADA_BD_TEMAS_SUB_TEMA> Temas { get; set; } = [];
        TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;
        [Inject] public ForumRTCZViewModel vm { get; set; }

        protected override void OnInitialized()
        {
            Model = new(new(Guid.Empty, string.Empty, 0, vm.Userservice.User.MATRICULA, DateTime.Now));
            Model.Tema = new JORNADA_BD_TEMAS_SUB_TEMA
            {
                ID_TEMAS = 0,
                ID_SUB_TEMAS = 0,
            };
            base.OnInitialized();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                vm.IsBusy = true;
                Temas = await vm.GetTemas();
                vm.IsBusy = false;
                StateHasChanged();
            }
            await base.OnAfterRenderAsync(firstRender);
        }
        public void Dispose()
        {
        }
    }
}
