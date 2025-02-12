

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[<assembly: LambdaSerializer(typeof<Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer>)>]
()

namespace QuizLambdaFSharp


open System
open System.Text.Json
open Amazon.Lambda.Core
open Amazon.Lambda.APIGatewayEvents
open Npgsql

type Quiz = {
    category:String
    prompt:String
    questions:Question array
}
type Question = {
    question:String
    options:String array
    answer:int
    points:int
}

type Function() =
    /// <summary>
    /// A fucntion that takes a json performs tranformation and submits data to porsgres table
    /// </summary>
    /// <param name="input">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    try
            // Parse JSON body
            let quizData = JsonSerializer.Deserialize<Quiz>(request.Body)

            // PostgreSQL connection string (Use environment variables)
            let connectionString = 
                sprintf "Host=%s;Username=%s;Password=%s;Database=%s" 
                    (Environment.GetEnvironmentVariable "DB_HOST")
                    (Environment.GetEnvironmentVariable "DB_USER")
                    (Environment.GetEnvironmentVariable "DB_PASSWORD")
                    (Environment.GetEnvironmentVariable "DB_NAME")

            use conn = new NpgsqlConnection(connectionString)
            conn.Open()

            use cmd = new NpgsqlCommand("INSERT INTO quizzes (title, questions) VALUES (@title, @questions) RETURNING id;", conn)
            cmd.Parameters.AddWithValue("@title", quizData.title)
            cmd.Parameters.AddWithValue("@questions", JsonSerializer.Serialize(quizData.questions))
            
            let quizId = cmd.ExecuteScalar() :?> int
            
            // Return success response
            let response = 
                { StatusCode = 200
                  Body = JsonSerializer.Serialize({| message = "Quiz saved successfully"; quiz_id = quizId |}) }

            response

        with
        | ex -> 
            { StatusCode = 500
              Body = JsonSerializer.Serialize({| error = ex.Message |}) }
