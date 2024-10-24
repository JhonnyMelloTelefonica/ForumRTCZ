using Shared_Static_Class.Model;
using BlazorDownloadFile;
using Blazorise.LoadingIndicator;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Shared_Razor_Components.Services;
using Shared_Static_Class.Model_DTO;
using Shared_Razor_Components.FundamentalModels;
using System.ComponentModel;
using Newtonsoft.Json;
using Shared_Razor_Components.Shared;
using Newtonsoft.Json.Linq;
using Radzen;
using Microsoft.FluentUI.AspNetCore.Components;
using Shared_Static_Class.Data;
using Shared_Razor_Components.ViewModel;
using Blazorise;
using Microsoft.JSInterop;
using BlazorBootstrap;
using Shared_Static_Class.Converters;
using System.Linq;
using Shared_Static_Class.Model_DTO.FilterModels;
using Shared_Static_Class.Model_ForumRTCZ_Context;
using Shared_Static_Class.ErrorModels;

namespace ForumRTCZ.ViewModels
{
    public class ForumRTCZViewModel : VivoAppsViewModel
    {
        public ForumRTCZViewModel(IHttpContextAccessor httpAccessor, IWebHostEnvironment networkacessor, GetUser_REDECORP getUser_REDECORP, IPageProgressService pageProgressService, IJSRuntime jSRuntime, PreloadService preloadService, IBlazorDownloadFileService dowloader, Radzen.DialogService radzendialog, IDialogService fluentDialog, Blazorise.IMessageService messageService, ILoadingIndicatorService applicationLoadingIndicatorService, SweetAlertService swal, NavigationManager navigationManager, UserService userservice, Microsoft.FluentUI.AspNetCore.Components.IToastService _ToastService, IAcessoPendenteByIdService _AcessoPendenteByldService, IAcessoTerceirosService _AcessoTerceirosService, IAnswerFormService _AnswerFormService, ICardapioDigitalService _CardapioDigitalService, IConsultarDemandasService _ConsultarDemandasService, IControleDemandaService _ControleDemandaService, IControleUsuariosAppService _ControleUsuariosAppService, ICreateFormService _CreateFormService, ICreateQuestionService _CreateQuestionService, IDesligamentosService _DesligamentosService, IEditQuestionService _EditQuestionService, IEditSingleQuestionService _EditSingleQuestionService, IEditUserService _EditUserService, IJornadaHierarquiaService _JornadaHierarquiaService, IListaFormService _ListaFormService, IPainelProvasRealizadasService _PainelProvasRealizadasService, IPainelUsuariosService _PainelUsuariosService, IPrincipalService _PrincipalService, IPWService _PWService, IRegisterService _RegisterService, IResultadosProvaService _ResultadosProvaService, IForumRTCZService _ForumRTCZService) : base(httpAccessor, networkacessor, getUser_REDECORP, pageProgressService, jSRuntime, preloadService, dowloader, radzendialog, fluentDialog, messageService, applicationLoadingIndicatorService, swal, navigationManager, userservice, _ToastService, _AcessoPendenteByldService, _AcessoTerceirosService, _AnswerFormService, _CardapioDigitalService, _ConsultarDemandasService, _ControleDemandaService, _ControleUsuariosAppService, _CreateFormService, _CreateQuestionService, _DesligamentosService, _EditQuestionService, _EditSingleQuestionService, _EditUserService, _JornadaHierarquiaService, _ListaFormService, _PainelProvasRealizadasService, _PainelUsuariosService, _PrincipalService, _PWService, _RegisterService, _ResultadosProvaService, _ForumRTCZService)
        {
        }

        public IEnumerable<PUBLICACAO_SOLICITACAODTO> Data { get; set; } = [];
        public GenericPaginationModel<PainelForumRTCZ> filterPagination { get; set; } = new(new(string.Empty, 0, [], []));
        public GenericPagedResponse<IEnumerable<PUBLICACAO_SOLICITACAODTO>> DataResponse { get; set; } = new([], 1, 10);
        public PainelForumRTCZ filter { get; set; } = new(string.Empty, 0, [], []);

        public async Task Get(int? NewPage = null, bool ByUser = false)
        {
            if (NewPage.HasValue)
                filterPagination.PageNumber = NewPage.Value;

            filterPagination.Value = new PainelForumRTCZ(filter.search, filter.avaliacao, filter.tema, filter.subtema);

            MainResponse result;
            if (ByUser)
            {
                result = await ForumRTCZService.SearchByFilters(filterPagination, Userservice.User.MATRICULA);
            }
            else
            {
                result = await ForumRTCZService.SearchByFilters(filterPagination);
            }

            if (result.IsSuccess)
            {
                var saida = JsonConvert.DeserializeObject<Response<object>>(result.Content.ToString());
                if (saida.Succeeded)
                {
                    DataResponse = JsonConvert.DeserializeObject<GenericPagedResponse<IEnumerable<PUBLICACAO_SOLICITACAODTO>>>(saida.Data.ToString());
                    Data = DataResponse.Data.Select(x => new PUBLICACAO_SOLICITACAODTO(x)).ToList();
                }
                else
                {
                    await FluentDialog.ShowErrorAsync(saida.Message, "Erro!");

                    if (JSRuntime != null && saida.Errors != null && saida.Errors.Any())
                    {
                        await JSRuntime.InvokeVoidAsync("console.log", string.Join(';', saida.Errors));
                    }
                }
            }
            else
            {
                await FluentDialog.ShowErrorAsync(result.ErrorMessage, "Algum erro ocorreu");
            }

            await Task.CompletedTask;
        }
        public async Task GetByAnalista(int? NewPage = null)
        {
            if (NewPage.HasValue)
                filterPagination.PageNumber = NewPage.Value;

            filterPagination.Value = new PainelForumRTCZ(filter.search, filter.avaliacao, filter.tema, filter.subtema);

            MainResponse result;

            result = await ForumRTCZService.SearchByAnalista(filterPagination, Userservice.User.MATRICULA);

            if (result.IsSuccess)
            {
                var saida = JsonConvert.DeserializeObject<Response<object>>(result.Content.ToString());
                if (saida.Succeeded)
                {
                    DataResponse = JsonConvert.DeserializeObject<GenericPagedResponse<IEnumerable<PUBLICACAO_SOLICITACAODTO>>>(saida.Data.ToString());
                    Data = DataResponse.Data.Select(x => new PUBLICACAO_SOLICITACAODTO(x)).ToList();
                }
                else
                {
                    await FluentDialog.ShowErrorAsync(saida.Message, "Erro!");

                    if (JSRuntime != null && saida.Errors != null && saida.Errors.Any())
                    {
                        await JSRuntime.InvokeVoidAsync("console.log", string.Join(';', saida.Errors));
                    }
                }
            }
            else
            {
                await FluentDialog.ShowErrorAsync(result.ErrorMessage, "Algum erro ocorreu");
            }

            await Task.CompletedTask;
        }
        public async Task<IEnumerable<JORNADA_BD_TEMAS_SUB_TEMA>> GetTemas(bool ByAnalista = false)
        {
            MainResponse result;
            if (ByAnalista)
            {
                result = await ForumRTCZService.GetTemas(Userservice.User.MATRICULA);
            }
            else
            {
                result = await ForumRTCZService.GetTemas();
            }

            if (result.IsSuccess)
            {
                var saida = JsonConvert.DeserializeObject<Response<object>>(result.Content.ToString());
                if (saida.Succeeded)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<JORNADA_BD_TEMAS_SUB_TEMA>>(saida.Data.ToString());
                }
                else
                {
                    await FluentDialog.ShowErrorAsync(saida.Message, "Erro!");

                    if (JSRuntime != null && saida.Errors != null && saida.Errors.Any())
                    {
                        await JSRuntime.InvokeVoidAsync("console.log", string.Join(';', saida.Errors));
                    }
                    return [];
                }
            }
            else
            {
                await FluentDialog.ShowErrorAsync(result.ErrorMessage, "Algum erro ocorreu");
            }
            return [];
        }
        public async Task<bool> PostPublicacao(PUBLICACAO_SOLICITACAODTO data)
        {
            var result = await ForumRTCZService.PostPublicacao(new PublicacaoModel(data.TEXT_PUBLICACAO,data.SUB_TEMA,data.MAT_RESPONSAVEL,DateTime.Now));
            if (result.IsSuccess)
            {
                var saida = JsonConvert.DeserializeObject<Response<object>>(result.Content.ToString());
                if (saida.Succeeded)
                {
                    await FluentDialog.ShowSuccessAsync(saida.Message, "Tudo Certo!");
                    return true;
                }
                else
                {
                    if (JSRuntime != null && saida.Errors != null && saida.Errors.Any())
                    {
                        await JSRuntime.InvokeVoidAsync("console.log", string.Join(';', saida.Errors));
                        await FluentDialog.ShowErrorAsync(string.Join(';', saida.Errors), "Erro!");
                    }
                    else
                    {
                        await FluentDialog.ShowErrorAsync(saida.Message, "Erro!");
                    }
                }
            }
            else
            {
                try
                {
                    var saida = result.Content as ErrorResponse;
                    if (JSRuntime != null && saida.Errors != null && saida.Errors.Any())
                    {
                        await JSRuntime.InvokeVoidAsync("console.log", result.ErrorMessage);
                        await FluentDialog.ShowErrorAsync(result.ErrorMessage, "Por favor tente novamente!");
                    }
                    else
                    {
                        await FluentDialog.ShowErrorAsync(result.ErrorMessage, "Erro!");
                    }
                }
                catch
                {
                    await FluentDialog.ShowErrorAsync(result.ErrorMessage, "Algum erro ocorreu.");
                }
            }

            return false;
        }
        public async Task<bool> PostRespostaPublicacao(RESPOSTA_PUBLICACAODTO data)
        {
            var result = await ForumRTCZService.PostRespostaPublicacao(new RespostaPublicacaoModel(data.ID_SOLICITACAO_PUBLICACAO,data.TEXT_PUBLICACAO,data.MAT_SOLICITANTE,DateTime.Now));
            if (result.IsSuccess)
            {
                var saida = JsonConvert.DeserializeObject<Response<object>>(result.Content.ToString());
                if (saida.Succeeded)
                {
                    await FluentDialog.ShowSuccessAsync(saida.Message, "Tudo Certo!");
                    return true;
                }
                else
                {

                    if (JSRuntime != null && saida.Errors != null && saida.Errors.Any())
                    {
                        await JSRuntime.InvokeVoidAsync("console.log", string.Join(';', saida.Errors));
                        await FluentDialog.ShowErrorAsync(string.Join(';', saida.Errors), "Erro!");
                    }
                    else
                    {
                        await FluentDialog.ShowErrorAsync(saida.Message, "Erro!");
                    }

                }
            }
            else
            {
                try
                {
                    var saida = result.Content as ErrorResponse;
                    if (JSRuntime != null && saida.Errors != null && saida.Errors.Any())
                    {
                        await JSRuntime.InvokeVoidAsync("console.log", result.ErrorMessage);
                        await FluentDialog.ShowErrorAsync(result.ErrorMessage, "Por favor tente novamente!");
                    }
                    else
                    {
                        await FluentDialog.ShowErrorAsync(result.ErrorMessage, "Erro!");
                    }
                }
                catch
                {
                    await FluentDialog.ShowErrorAsync(result.ErrorMessage, "Algum erro ocorreu.");
                }
            }

            return false;
        }
        public async Task<bool> PostAvaliacaoToPublicacao(int value, Guid id_argumentacao)
        {
            if (id_argumentacao != Guid.Empty)
            {
                var saidasingleimage = await ForumRTCZService.PostAvaliacaoToPublicacao(
                    new AvaliacaoPublicacaoModel(id_argumentacao,Userservice.User.MATRICULA,value)
                    );

                if (saidasingleimage.IsSuccess)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
