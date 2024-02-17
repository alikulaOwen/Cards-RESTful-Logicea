using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


var dbCon = builder.Configuration.GetSection("ConnectionStrings:SQLServerConn").Value;

var successCreated = builder.Configuration.GetSection("ResponseMessage:SuccessCreated").Value;
var failCreated = builder.Configuration.GetSection("ResponseMessage:FailCreated").Value;
var notAuthorized = builder.Configuration.GetSection("ResponseMessage:NotAuthorized").Value; 
var requestAllCards = builder.Configuration.GetSection("ResponseMessage:RequestAllCards").Value; 
var requestAllCardsFailed = builder.Configuration.GetSection("ResponseMessage:RequestAllCardsFailed").Value;
var requestedACard = builder.Configuration.GetSection("ResponseMessage:RequestedACard").Value;
var requestACardFailed = builder.Configuration.GetSection("ResponseMessage:RequestACardFailed").Value;
var searchACardFailed = builder.Configuration.GetSection("ResponseMessage:SearchACardFailed").Value;
var updatedACard = builder.Configuration.GetSection("ResponseMessage:UpdatedACard").Value;
var updatedACardFailed = builder.Configuration.GetSection("ResponseMessage:UpdatedACardFailed").Value;
var deletedACard = builder.Configuration.GetSection("ResponseMessage:DeletedACard").Value;
var deletedACardFailed = builder.Configuration.GetSection("ResponseMessage:DeletedACardFailed").Value;
var notValid = builder.Configuration.GetSection("ResponseMessage:NotValid").Value;
var colorError = builder.Configuration.GetSection("ResponseMessage:ColorError").Value;
var emptyString = builder.Configuration.GetSection("ResponseMessage:EmptyString").Value;
var hushSymbol = builder.Configuration.GetSection("ResponseMessage:HushSymbol").Value;
var stringName = builder.Configuration.GetSection("ResponseMessage:StringName").Value;
var stringStatus = builder.Configuration.GetSection("ResponseMessage:StringStatus").Value;
var nameStatusCanNotBeEmpty = builder.Configuration.GetSection("ResponseMessage:NameStatusCanNotBeEmpty").Value;
var definedRegex = builder.Configuration.GetSection("ResponseMessage:DefinedRegex").Value;

var successLogin = builder.Configuration.GetSection("ResponseMessage:SuccessLogin").Value;
var failLogin = builder.Configuration.GetSection("ResponseMessage:FailLogin").Value;
var requestSuccesful = builder.Configuration.GetSection("ResponseMessage:ReqSuccess").Value;
var serverNotResponding = builder.Configuration.GetSection("ResponseMessage:ServerNotRes").Value;
var cardDoesNotExist = builder.Configuration.GetSection("ResponseMessage:CardDoesNotExist").Value;

//STORED-PROCEDURES

var spLogin = builder.Configuration.GetSection("StoredProcedures:SPLogin").Value;
var sPAddCard = builder.Configuration.GetSection("StoredProcedures:SPAddCard").Value;
var sPGetAllCards = builder.Configuration.GetSection("StoredProcedures:SPGetAllCards").Value;
var sPGetSpecificCards = builder.Configuration.GetSection("StoredProcedures:SPGetSpecificCards").Value;
var sPSearchForCards = builder.Configuration.GetSection("StoredProcedures:SPSearchForCards").Value;
var sPUpdateSpecificCards = builder.Configuration.GetSection("StoredProcedures:SPUpdateSpecificCards").Value;
var sPDeleteSpecificCards = builder.Configuration.GetSection("StoredProcedures:SPDeleteSpecificCards").Value;




var docVersion = builder.Configuration.GetSection("SwaggerDoc:DocVersion").Value;
var apiVersion = builder.Configuration.GetSection("SwaggerDoc:ApiVersion").Value;
var docTitle = builder.Configuration.GetSection("SwaggerDoc:Title").Value;
var docDescription = builder.Configuration.GetSection("SwaggerDoc:Description").Value;
var devName = builder.Configuration.GetSection("SwaggerDoc:Name").Value;
var devEmail = builder.Configuration.GetSection("SwaggerDoc:Email").Value;
var projectUrl = builder.Configuration.GetSection("SwaggerDoc:Url").Value;

// Add JWT authentication services
var audience = builder.Configuration.GetSection("Jwt:Audience").Value;
var issuer = builder.Configuration.GetSection("Jwt:Issuer").Value;
var secretKey = builder.Configuration.GetSection("Jwt:Key").Value;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, 
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

    options.SwaggerDoc(docVersion, new OpenApiInfo
    {
        Version = apiVersion,
        Title = docTitle,
        Description = docDescription,
        Contact = new OpenApiContact
        {
            Name = devName,
            Email = devEmail,
            Url = new Uri(projectUrl),
        },
    });

});


builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();

    loggingBuilder.AddFile(Path.Combine(AppContext.BaseDirectory, "Logs", "cards_log_{Date}.txt"));
});



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

ParamsModel.DBCon = dbCon;
ParamsModel.Key = secretKey;
ParamsModel.Issuer = issuer;
ParamsModel.Audience = audience;
ParamsModel.SuccessLogin = successLogin;
ParamsModel.FailLogin = failLogin;
ParamsModel.ApiDocVersion = apiVersion;
ParamsModel.RequestSuccessful = requestSuccesful;
ParamsModel.ServerNotResponding = serverNotResponding;
ParamsModel.SuccessCreated = successCreated;
ParamsModel.FailCreated = failCreated; 
ParamsModel.NotAuthorized = notAuthorized; 
ParamsModel.RequestAllCards = requestAllCards; 
ParamsModel.RequestAllCardsFailed = requestAllCardsFailed; 
ParamsModel.RequestedACard = requestedACard;
ParamsModel.RequestACardFailed = requestACardFailed;
ParamsModel.SearchACardFailed = searchACardFailed;
ParamsModel.UpdatedACard = updatedACard;
ParamsModel.UpdatedACardFailed = updatedACardFailed;
ParamsModel.DeletedACard = deletedACard;
ParamsModel.DeletedACardFailed = deletedACardFailed;
ParamsModel.NotValid = notValid;
ParamsModel.ColorError = colorError;
ParamsModel.EmptyString = emptyString;
ParamsModel.HushSymbol = hushSymbol;
ParamsModel.StringName = stringName;
ParamsModel.StringStatus = stringStatus;
ParamsModel.NameStatusCanNotBeEmpty = nameStatusCanNotBeEmpty;
ParamsModel.DefinedRegex = definedRegex;
ParamsModel.CardDoesNotExist = cardDoesNotExist;

ParamsModel.SPLogin = spLogin;
ParamsModel.SPAddCard = sPAddCard;
ParamsModel.SPGetAllCards = sPGetAllCards;
ParamsModel.SPGetSpecificCards = sPGetSpecificCards;
ParamsModel.SPSearchForCards = sPSearchForCards;
ParamsModel.SPUpdateSpecificCards = sPUpdateSpecificCards;
ParamsModel.SPDeleteSpecificCards = sPDeleteSpecificCards;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
