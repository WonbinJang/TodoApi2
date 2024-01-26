using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace TodoApi.Controllers;

using System.ComponentModel;
using System.Configuration;
using Azure.Identity;
using Microsoft.AspNetCore.Cors;
// 종속성, Dedendency
// 종속성 주입, Dependency Injection

[Route("api/[controller]")]
[ApiController]
// [EnableCors("corsapp")]
public class TodoItemsController : ControllerBase
{
    
    public SqlConnection conn = new SqlConnection("Server=localhost,1433;user=sa;pwd=LILvision12;database=TodoList;TrustServerCertificate=True;");
    
    public class Change(int id, int status){
        public int id{get;set;}

        public int status{get;set;}
    }
    // DbContext.ConnectionStrings
    
    public class TodoItemUpdateDto {
        public int Id {get;set;}
        public string Title {get;set;}
        public string Description {get;set;}
        public int Status {get;set;}
     }
// public TodoItemsController(IConfiguration configuration)
//     {
//         _configuration = configuration;
//     }
//      private readonly IConfiguration _configuration;

    
//     string strConnection = _configuration.GetSection("ConnectionStrings").GetSection("default").Value;



    [HttpGet("user/test")]
    public IActionResult Test()
    {
        using (conn)
        {
            conn.Open();
            
            SqlCommand cmd = new(@"
                select * from todo As a
full outer join TodoList.dbo.[user] AS b
                on a.ownerId = b.id 
                where status = 4
            ", conn);
            var todos = new List<TodoItem>();
            
            using (SqlDataReader reader = cmd.ExecuteReader()){
                while(reader.Read()){
            
                todos.Add(new TodoItem{
                    Id = Convert.ToInt32(reader["id"]),
                    Title = reader["title"].ToString(),
                    userName=reader["userName"].ToString(),
                    Description = reader["description"].ToString(),
                    Status = Convert.ToInt32(reader["status"]),
                    CreatedAt = Convert.ToDateTime(reader["createdAt"]),
                    });
                }
            }//if~elseif
            return Ok(todos);
         }//while
          
        } //using conn
             
    [HttpGet("Backlogs")]
    public IActionResult GetBacklogs()
    { 
       using (conn)
        {
            conn.Open();
            SqlCommand cmd = new(@"
                SELECT
                    *
                FROM todo 
                where disabledAt is null AND status = 1
            ", conn);
            var todos = new List<TodoItem>();
            using (SqlDataReader reader = cmd.ExecuteReader()){

                while(reader.Read()){

                if(reader["DisabledAt"]==System.DBNull.Value){
                todos.Add(new TodoItem{
                    Id = Convert.ToInt32(reader["id"]),
                    Title = reader["title"].ToString(),
                    Description = reader["description"].ToString(),
                    Status = Convert.ToInt32(reader["status"]),
                    CreatedAt = Convert.ToDateTime(reader["createdAt"]),
                    });
                }
            }//if~elseif
            
         }//while
          return Ok(todos);
        } //using conn
       
        }//using SqlConnection
            
    [HttpGet("Todo")]
    public IActionResult GetTodos()
    { 
       using (conn){
            conn.Open();
            SqlCommand cmd = new(@"
                select * from todo As a
full outer join TodoList.dbo.[user] AS b
                on a.ownerId = b.id 
                where status = 2
            ", conn);

            var todos = new List<TodoItem>();
            using (SqlDataReader reader = cmd.ExecuteReader()){

                while(reader.Read()){

                if(reader["DisabledAt"]==System.DBNull.Value){
                todos.Add(new TodoItem{
                  Id = Convert.ToInt32(reader["id"]),
                    Title = reader["title"].ToString(),
                    userName = reader["userName"].ToString(),
                    Description = reader["description"].ToString(),
                    Status = Convert.ToInt32(reader["status"]),
                    CreatedAt = Convert.ToDateTime(reader["createdAt"]),
                });
            }//if~elseif
            
         }//while
        } //using SqlReader
        return Ok(todos);
        }//using SqlConnection
            
       }
    
    [HttpGet("Doing")]
    public IActionResult GetProgress()
    { 
       using (conn)
        {
            conn.Open();
            SqlCommand cmd = new(@"
                select * from todo As a
full outer join TodoList.dbo.[user] AS b
                on a.ownerId = b.id 
                where status = 3
            ", conn);
var todos = new List<TodoItem>();
            using (SqlDataReader reader = cmd.ExecuteReader()){

                while(reader.Read()){

                if(reader["DisabledAt"]==System.DBNull.Value){
                todos.Add(new TodoItem{
                    Id = Convert.ToInt32(reader["id"]),
                    Title = reader["title"].ToString(),
                    userName = reader["userName"].ToString(),
                    Description = reader["description"].ToString(),
                    Status = Convert.ToInt32(reader["status"]),
                    CreatedAt = Convert.ToDateTime(reader["createdAt"]),
                });
            }//if~elseif
            
         }//while
        } //using SqlReader
        return Ok(todos);
        }//using SqlConnection
            
            
        }
    
    [HttpGet("Done")]
    public IActionResult GetDones()
    { 
       using (conn)
        {
            conn.Open();
            SqlCommand cmd = new(@"
               select * from todo As a
full outer join TodoList.dbo.[user] AS b
                on a.ownerId = b.id 
                where status = 4
            ", conn);
var todos = new List<TodoItem>();
            using (SqlDataReader reader = cmd.ExecuteReader()){

                while(reader.Read()){

                if(reader["DisabledAt"]==System.DBNull.Value){
                todos.Add(new TodoItem{
                  Id = Convert.ToInt32(reader["id"]),
                    Title = reader["title"].ToString(),
                    userName = reader["userName"].ToString(),
                    Description = reader["description"].ToString(),
                    Status = Convert.ToInt32(reader["status"]),
                    CreatedAt = Convert.ToDateTime(reader["createdAt"]),
                  
                });
            }//if~elseif
            
         }//while
        } //using SqlReader
        return Ok(todos);
        }//using SqlConnection
}
    
     [HttpGet("{id}")]
     public IActionResult GetDescription(int id){
        using (conn){
            conn.Open();
            SqlCommand cmd = new(@"
                SELECT
                    *
                FROM todo
                WHERE id =@id
            ", conn);
            cmd.Parameters.Add(new SqlParameter("@id",System.Data.SqlDbType.Int));
            cmd.Parameters["@id"].Value=id;
                var todos = new List<TodoItem>();
            using (SqlDataReader reader = cmd.ExecuteReader()){
                while(reader.Read()){
                if(reader["DisabledAt"]==System.DBNull.Value){
                todos.Add(new TodoItem{
                    Id = Convert.ToInt32(reader["id"]),
                    Title = reader["title"].ToString(),
                    Description = reader["description"].ToString(),
                    Status = Convert.ToInt32(reader["status"]),
                    CreatedAt = Convert.ToDateTime(reader["createdAt"]),
                    });
                }else if(reader["DisabledAt"]!=System.DBNull.Value){
                todos.Add(new TodoItem{
                    Id = Convert.ToInt32(reader["id"]),
                    Title = reader["title"].ToString(),
                    Description = reader["description"].ToString(),
                    Status = Convert.ToInt32(reader["status"]),
                    CreatedAt = Convert.ToDateTime(reader["createdAt"]),
                    DisabledAt = Convert.ToDateTime(reader["disabledAt"]),
                });
            }//if~elseif
            
         }//while
        } //using SqlReader
        return Ok(todos);
        }//using SqlConnection
        
     }//public IActionResult     
    
    [HttpDelete("{id}")]
     public IActionResult DeleteTodoItem(int id)
     {
        using (conn){
            conn.Open();
            // SqlCommand cmd = new(@"
            //     DELETE
            //     FROM todo
            //     WHERE id =@id  
            // ", conn);
            SqlCommand cmd = new(@"
                UPDATE
                TodoList.dbo.todo
                SET
                disabledAt = CURRENT_TIMESTAMP
                WHERE id =@id  
             ", conn);
            cmd.Parameters.Add(new SqlParameter("@id",System.Data.SqlDbType.Int));
            cmd.Parameters["@id"].Value=id;
            cmd.ExecuteNonQuery();
                
        }
        return Ok("삭제되었습니다.");
     }
    
    [HttpPut]
     public IActionResult PutItem(string title, string description,int status)
     {
        // 여기서 json 파싱해서 값 변수에 넣어줘야함
        using (conn){
            conn.Open();
            SqlCommand cmd = new(@"
                INSERT INTO  TodoList.dbo.todo 
                (title,description,status,createdAt,disabledAt) 
                VALUES 
                ('@title','@description',@status,GetDate(),null) 
            ", conn);
            cmd.Parameters.Add(new SqlParameter("@id",System.Data.SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@title",System.Data.SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@description",System.Data.SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@status",System.Data.SqlDbType.Int));
            cmd.Parameters["@title"].Value=title;
            cmd.Parameters["@description"].Value=description;
            cmd.Parameters["@status"].Value=status;
            cmd.ExecuteNonQuery();
                
        }
        return Ok("저장되었습니다.");
     }
     
     [HttpPost]
     public IActionResult UpdateItem([FromBody] TodoItemUpdateDto param)
     {Console.WriteLine("{0}",param.Title);
        // 여기서 json 파싱해서 값 변수에 넣어줘야함
        using (conn){
            conn.Open();
            SqlCommand cmd = new(@"
                UPDATE TodoList.dbo.todo 
                SET
                title =@title,description=@description,status=@status
                WHERE id = @id
            ", conn);
            cmd.Parameters.Add(new SqlParameter("@id",System.Data.SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@title",System.Data.SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@description",System.Data.SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@status",System.Data.SqlDbType.Int));
            cmd.Parameters["@id"].Value=param.Id;
            cmd.Parameters["@title"].Value=param.Title;
            cmd.Parameters["@description"].Value=param.Description;
            cmd.Parameters["@status"].Value=param.Status;
            cmd.ExecuteNonQuery();
                
        }
        return Ok("수정되었습니다.");
     }
    
    [HttpPut("user/signup")]
     public IActionResult Signup([FromBody] User param)
     {
        
        // 여기서 json 파싱해서 값 변수에 넣어줘야함
        using (conn){
            conn.Open();
            SqlCommand cmd = new(@"
                INSERT INTO  TodoList.dbo.user 
                (userName,Password,createdAt,disabledAt) 
                VALUES
                ('@userName','@Password',GetDate(),null)
            ", conn);
            cmd.Parameters.Add(new SqlParameter("@userName",System.Data.SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@Password",System.Data.SqlDbType.Text));
            cmd.Parameters["@userName"].Value=param.userName;
            cmd.Parameters["@Password"].Value=param.Password;
            cmd.ExecuteNonQuery();
                
        }
        return Ok("회원가입이 완료 되었습니다.");
     }
    
    [HttpPost("user/login")]
     public IActionResult SignIn([FromBody] User param){
        
        using (conn){
            conn.Open();
            SqlCommand cmd = new(@"
                SELECT
                *
                FROM TodoList.dbo.[user]
                WHERE userName = @userName
            ", conn);
            cmd.Parameters.Add(new SqlParameter("@userName",System.Data.SqlDbType.VarChar));
            
            cmd.Parameters["@userName"].Value=param.userName;
           
            using (SqlDataReader reader = cmd.ExecuteReader()){
              reader.Read();
               
                if(reader["hashPassword"].ToString()==param.Password){
                        return Ok(param.userName);
                }else{
                    return StatusCode(400);
                }
            }
        
        }
        
    }  
  
    // [HttpPost("statusTodo/{id}")]
    //  public IActionResult ChangeStatus(int id){
        
        
    //     // 여기서 json 파싱해서 값 변수에 넣어줘야함
    //     using (conn){
    //         conn.Open();
    //         SqlCommand cmd = new(@"
    //             UPDATE TodoList.dbo.todo 
    //             SET
    //             status=@status
    //             WHERE id = @id
    //         ", conn);
            
    //         cmd.Parameters.Add(new SqlParameter("@id",System.Data.SqlDbType.Int));
    //         cmd.Parameters.Add(new SqlParameter("@status",System.Data.SqlDbType.Int));
            
    //         cmd.Parameters["@id"].Value=id;
    //         cmd.Parameters["@status"].Value=2;
    //         cmd.ExecuteNonQuery();
                
    //     }
    //     return Ok("수정되었습니다.");
    //  }
    //  [HttpPost("statusDoing/{id}")]
    //  public IActionResult ChangeStatusD(int id){
    //     Console.WriteLine("첫줄{0},",id);
    //     // 여기서 json 파싱해서 값 변수에 넣어줘야함
    //     using (conn){
    //         conn.Open();
    //         SqlCommand cmd = new(@"
    //             UPDATE TodoList.dbo.todo 
    //             SET
    //             status=@status
    //             WHERE id = @id
    //         ", conn);
            
    //         cmd.Parameters.Add(new SqlParameter("@id",System.Data.SqlDbType.Int));
    //         cmd.Parameters.Add(new SqlParameter("@status",System.Data.SqlDbType.Int));
            
    //         cmd.Parameters["@id"].Value=id;
    //         cmd.Parameters["@status"].Value=3;
    //         cmd.ExecuteNonQuery();
                
    //     }
    //     return Ok("수정되었습니다.");
    //  }
    //  [HttpPost("statusDone/{id}")]
    //  public IActionResult ChangeStatusDone(int id){
    //     Console.WriteLine("첫줄{0}",id);
    //     // 여기서 json 파싱해서 값 변수에 넣어줘야함
    //     using (conn){
    //         conn.Open();
    //         SqlCommand cmd = new(@"
    //             UPDATE TodoList.dbo.todo 
    //             SET
    //             status=@status
    //             WHERE id = @id
    //         ", conn);
            
    //         cmd.Parameters.Add(new SqlParameter("@id",System.Data.SqlDbType.Int));
    //         cmd.Parameters.Add(new SqlParameter("@status",System.Data.SqlDbType.Int));
            
    //         cmd.Parameters["@id"].Value=id;
    //         cmd.Parameters["@status"].Value=4;
    //         cmd.ExecuteNonQuery();
                
    //     }
    //     return Ok("수정되었습니다.");
    //  }

     [HttpPost("status")]
     public IActionResult ChangeStatus([FromBody]TodoStatus param){
       int ownerId;
       try
       {
        using (conn){
            conn.Open();
            SqlCommand cmd = new(@"
               select
               id
               from TodoList.dbo.[user]
                WHERE userName = @userName
            ", conn);
            
            cmd.Parameters.Add(new SqlParameter("@userName",System.Data.SqlDbType.VarChar));
            
            cmd.Parameters["@userName"].Value=param.userName;
            cmd.ExecuteNonQuery();
            using (SqlDataReader reader = cmd.ExecuteReader()){
               reader.Read();
                ownerId=Convert.ToInt32(reader["id"]);
        }
            SqlCommand cmda = new(@"
                UPDATE TodoList.dbo.todo 
                SET
                status=@status,
                ownerId=@ownerId
                WHERE id = @id
            ", conn);
            
            cmda.Parameters.Add(new SqlParameter("@id",System.Data.SqlDbType.Int));
            cmda.Parameters.Add(new SqlParameter("@status",System.Data.SqlDbType.Int));
            cmda.Parameters.Add(new SqlParameter("@ownerId",System.Data.SqlDbType.Int));

            cmda.Parameters["@id"].Value=param.Id;
            cmda.Parameters["@status"].Value=param.Status;
            cmda.Parameters["@ownerId"].Value=ownerId;
            cmda.ExecuteNonQuery();
                 return Ok("수정되었습니다.");
        }
       }
       catch (System.Exception)
       {
        
        throw;
       }
        // 여기서 json 파싱해서 값 변수에 넣어줘야함
     } 
       
}

