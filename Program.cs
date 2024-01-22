using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

using TodoApi.Models;
/*
using 지시문을 추가합니다.
DI 컨테이너에 데이터베이스 컨텍스트를 추가합니다.
데이터베이스 컨텍스트가 메모리 내 데이터베이스를 사용하도록 지정합니다.
*/

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();