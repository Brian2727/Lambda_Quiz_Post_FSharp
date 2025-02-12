# 📌 F# AWS Lambda Quiz API

## 🎯 Project Overview
This project is an **AWS Lambda function written in F#** that processes quiz submissions. It accepts a **JSON payload** from an API request, transforms the data, and stores it in a **PostgreSQL database**.

## 🛠️ Tech Stack
- **Language:** F# (.NET 8)
- **Cloud Services:** AWS Lambda, API Gateway, Amazon RDS (PostgreSQL)
- **Database Driver:** `Npgsql`
- **Serialization:** `System.Text.Json`

## 📡 How It Works
1️⃣ **Client (React App, Postman, etc.)** sends a `POST` request with a **quiz JSON payload**.
2️⃣ **API Gateway** forwards the request to the **F# AWS Lambda function**.
3️⃣ **Lambda Function** extracts quiz details, transforms the data, and **inserts it into PostgreSQL**.
4️⃣ **Response** is returned to confirm successful storage or handle errors.

## 📜 JSON Payload Structure
### **Example Quiz JSON Submission**
```json
{
    "category": "F# Basics",
    "prompt": "Answer the following questions:",
    "questions": [
        {
            "question": "What is pattern matching?",
            "options": ["A loop", "A conditional structure", "An array"],
            "answer": 1,
            "points": 5
        },
        {
            "question": "How does F# handle immutability?",
            "options": ["Using 'mutable'", "By default all values are immutable", "Only in classes"],
            "answer": 1,
            "points": 5
        }
    ]
}
```
## 🏗️ Setting Up the Project
### **1️⃣ AWS Lambda Setup**
1. Create a **new AWS Lambda function** with `.NET 8` runtime.
2. Attach the necessary **IAM role permissions** for accessing **RDS PostgreSQL**.
3. Set **environment variables** for database credentials:
   ```
   DB_HOST=your-database-host
   DB_NAME=your-database-name
   DB_USER=your-database-user
   DB_PASSWORD=your-database-password
   ```

### **2️⃣ Deploy to AWS Lambda**
Run the following commands to deploy:
```sh
dotnet lambda package -o deploy.zip
dotnet lambda deploy-function QuizLambdaFSharp
```

### **3️⃣ Configure API Gateway**
1. Create a **new API Gateway (REST API)**.
2. Set up a **POST method** and integrate it with **Lambda**.
3. Deploy the API and copy the **Invoke URL**.

## 🔗 Future Enhancements
🔹 Implement **authentication (JWT)** for secure API access.  
🔹 Add **unit tests** for input validation and error handling.  
🔹 Enable **batch quiz submission processing** for scalability.  
