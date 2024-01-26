using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TodoApi.Models;
using Microsoft.Build.Framework;
using Microsoft.AspNetCore.Cors;
/*
using 지시문을 추가합니다.
DI 컨테이너에 데이터베이스 컨텍스트를 추가합니다.
데이터베이스 컨텍스트가 메모리 내 데이터베이스를 사용하도록 지정합니다.
*/

var builder = WebApplication.CreateBuilder(args);
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();

// builder.Services.AddCors(p => p.AddPolicy("corsapp",builder=>{
//     builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
// }));


builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("default");

var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

// app.UseCors("corsapp");
app.Run();