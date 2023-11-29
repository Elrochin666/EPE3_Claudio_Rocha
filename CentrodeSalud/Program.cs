using Mysql.Data;
using Mysql.Database.Repositorio;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mySQLConnectionConfig = new MySQLConfiguration(builder.Configuration.GetConnectionString("MySqlConnection"));
builder.Services.AddSingleton(mySQLConnectionConfig);
builder.Services.AddScoped<ConexionMedico, ConsultaMedico>();

// Repositorios para Paciente
builder.Services.AddScoped<ConexionPaciente, ConsultaPaciente>();

// Repositorios para Reserva
builder.Services.AddScoped<ConexionReserva, ConsultaReserva>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();