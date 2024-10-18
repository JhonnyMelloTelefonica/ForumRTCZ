using BlazorDownloadFile;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Blazorise.LoadingIndicator;
using Blazorise.RichTextEdit;
using ForumRTCZ;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.FluentUI.AspNetCore.Components;
using Shared_Razor_Components.Services;
using Shared_Razor_Components.Shared;
using Shared_Static_Class.Converters;
using Shared_Static_Class.DB_Context_Vivo_MAIS;
using Shared_Static_Class.Model;
using Shared_Razor_Components.FundamentalModels;
using Shared_Razor_Components;
using Shared_Razor_Components.Shared.BasicForApplication;
using ForumRTCZ.ViewModels;

var builder = WebApplication.CreateBuilder(args);

//builder.AddServiceDefaults();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpContextAccessor();
builder.Services.AddQuickGridEntityFrameworkAdapter();
builder.Services.AddFluentUIComponents();
builder.Services.AddDbContext<DemandasContext>();
builder.Services.AddLoadingIndicator();
builder.Services.AddBlazorDownloadFile();
builder.Services.AddBlazorBootstrap();
builder.Services.AddSharedServices();
builder.Services
    .AddBlazorise(opt =>
    {
        opt.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

builder.Services.Configure<Blazorise.Animate.AnimateOptions>(options =>
{
    options.Animation = Blazorise.Animate.Animations.FadeDown;
    options.Duration = TimeSpan.FromMilliseconds(300);
});
builder.Services.AddBlazoriseRichTextEdit(options =>
{
    options.UseBubbleTheme = true;
});

builder.Services.AddSignalR(options =>
{
    // Set the maximum size for incoming and outgoing messages
    options.MaximumReceiveMessageSize = 102400000; // Set to the desired maximum size in bytes
});
builder.Services.AddResponseCompression(op =>
{
    op.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});

builder.Services.AddSweetAlert2();

builder.Services.AddOptions();
builder.Services.AddTransient<IAuthorizationHandler, GenericPolicyHandler>();
builder.Services.AddAuthentication(IISDefaults.AuthenticationScheme);
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddSingleton<StaticUserRedecorp>(); // necessariamente Singleton pois guardam valores que s�o comuns a todos
builder.Services.AddSingleton<GetUser_REDECORP>(); // necessariamente Singleton pois guardam valores que s�o comuns a todos
builder.Services.AddScoped<Radzen.DialogService>();
builder.Services.AddScoped<UserCard>();

builder.Services.AddTransient<Register>();
builder.Services.AddSingleton<UsersAtivos>();// necessariamente Singleton pois guardam valores que s�o comuns a todos
builder.Services.AddScoped<UserService>(); // necessariamente Scoped pois s�o valores que s� seram alterados ao recarregarem a p�gina


builder.Services.AddSingleton<IAcessoPendenteByIdService, AcessoPendenteByIdService>();
builder.Services.AddSingleton<IAcessoTerceirosService, AcessoTerceirosService>();
builder.Services.AddSingleton<IAnswerFormService, AnswerFormService>();
builder.Services.AddSingleton<ICardapioDigitalService, CardapioDigitalService>();
builder.Services.AddSingleton<IConsultarDemandasService, ConsultarDemandasService>();
builder.Services.AddSingleton<IControleDemandaService, ControleDemandaService>();
builder.Services.AddSingleton<IControleUsuariosAppService, ControleUsuariosAppService>();
builder.Services.AddSingleton<ICreateFormService, CreateFormService>();
builder.Services.AddSingleton<ICreateQuestionService, CreateQuestionService>();
builder.Services.AddSingleton<IDesligamentosService, DesligamentosService>();
builder.Services.AddSingleton<IEditQuestionService, EditQuestionService>();
builder.Services.AddSingleton<IEditSingleQuestionService, EditSingleQuestionService>();
builder.Services.AddSingleton<IEditUserService, EditUserService>();
builder.Services.AddSingleton<IJornadaHierarquiaService, JornadaHierarquiaService>();
builder.Services.AddSingleton<IListaFormService, ListaFormService>();
builder.Services.AddSingleton<IPainelProvasRealizadasService, PainelProvasRealizadasService>();
builder.Services.AddSingleton<IPainelUsuariosService, PainelUsuariosService>();
builder.Services.AddSingleton<IPrincipalService, PrincipalService>();
builder.Services.AddSingleton<IPWService, PWService>();
builder.Services.AddSingleton<IRegisterService, RegisterService>();
builder.Services.AddSingleton<IResultadosProvaService, ResultadosProvaService>();
builder.Services.AddSingleton<IForumRTCZService, ForumRTCZService>();

builder.Services.AddScoped<ForumRTCZViewModel>();

var app = builder.Build();

//app.MapDefaultEndpoints();
//app.UseResponseCompression();
app.UseDeveloperExceptionPage();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Error");
}

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

//app.MapBlazorHub();
//app.MapFallbackToPage("/_Host");

app.Run();
