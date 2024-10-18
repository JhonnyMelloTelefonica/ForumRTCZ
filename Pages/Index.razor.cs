using ForumRTCZ.ViewModels;
using Microsoft.AspNetCore.Components;
using Shared_Static_Class.Data;
using Shared_Static_Class.Model_DTO;
using Shared_Static_Class.Model_DTO.FilterModels;
using System.ComponentModel;
using System.Globalization;

namespace ForumRTCZ.Pages
{
    public partial class Index : IDisposable
    {
        //[CascadingParameter]  Model
        TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;
        bool FooterOpen { get; set; } = false;
        bool AddNewPublicacao { get; set; } = false;
        [CascadingParameter] public PUBLICACAO_SOLICITACAODTO Model { get; set; } = null;
        private IEnumerable<JORNADA_BD_TEMAS_SUB_TEMA> Temas { get; set; } = [];
        string value { get; set; }
        [Inject] ForumRTCZViewModel vm { get; set; }
        private void OnStateChanged(object? sender, PropertyChangedEventArgs e) => InvokeAsync(StateHasChanged);
        protected override void OnInitialized()
        {
            vm.PropertyChanged += OnStateChanged;
            vm.filterPagination = new(new PainelForumRTCZ(vm.filter.search,
                vm.filter.avaliacao,
                vm.filter.tema,
                vm.filter.subtema), 1, 3);

            Model = new(new(Guid.Empty, string.Empty, 0, vm.Userservice.User.MATRICULA, DateTime.Now));
            Model.Tema = new JORNADA_BD_TEMAS_SUB_TEMA
            {
                ID_TEMAS = 0,
                ID_SUB_TEMAS = 0
            };
            base.OnInitialized();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                //_jsmodule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Shared_Razor_Components/Shared/ProdutoCard.razor.js");
                vm.isBusy = true;
                await vm.Get();
                Temas = await vm.GetTemas();
                vm.isBusy = false;
                await InvokeAsync(StateHasChanged);
            }
            await base.OnAfterRenderAsync(firstRender);
        }
        async void Get(int? value)
        {
            vm.isBusy = true;
            await vm.Get(value);
            vm.isBusy = false;
            await InvokeAsync(StateHasChanged);
        }
        async void PostPublicacao()
        {
            Model.HORA = DateTime.Now;
            Model.SUB_TEMA = Model.Tema.ID_SUB_TEMAS;
            Model.Responsavel = new();
            var saida = await vm.PostPublicacao(Model);
            if (saida)
            {
                AddNewPublicacao = false;
                vm.IsBusy = true;
                Get(1);
                vm.IsBusy = false;
                await InvokeAsync(StateHasChanged);
                //vm.Data = vm.Data.Append(Model);
                //StateHasChanged();
            }
        }
        async void SetAvaliacaoToPublicacao(int? args, PUBLICACAO_SOLICITACAODTO argumento)
        {
            if (args.HasValue)
            {
                var result = await vm.PostAvaliacaoToPublicacao(args.Value, argumento.ID_SOLICITACAO_PUBLICACAO);
                if (!result)
                {
                    await vm.FluentDialog.ShowErrorAsync("Ocorreu algum erro ao realizar a avaliação neste momento.", "Desculpe :(");
                }
                else
                {
                    argumento.ActualAvaliacao = args.Value;
                    //argumento.QtdAvaliacao += 1;
                }
            }
            StateHasChanged();
        }
        async void SetAvaliacaoToRespostaPublicacao(int? args, RESPOSTA_PUBLICACAODTO argumento)
        {
            if (args.HasValue)
            {
                var result = await vm.PostAvaliacaoToPublicacao(args.Value, argumento.ID_PUBLICACAO);
                if (!result)
                {
                    await vm.FluentDialog.ShowErrorAsync("Ocorreu algum erro ao realizar a avaliação neste momento.", "Desculpe :(");
                }
                else
                {
                    argumento.ActualAvaliacao = args.Value;
                    //argumento.QtdAvaliacao += 1;
                }
            }
            StateHasChanged();
        }
        public void MouseFooterEnter()
        {
            FooterOpen = !FooterOpen;
            StateHasChanged();
        }

        public void MouseFooterLeave()
        {
            FooterOpen = !FooterOpen;
            StateHasChanged();
        }

        public void Dispose()
        {

        }
    }
}