diff --git a/README.md b/README.md
index 91650c37d5e9505e4e0bf2f47014f560c1d538f1..9c6bfeff90948273f2c2f848ebf57744e7559303 100644
--- a/README.md
+++ b/README.md
@@ -1,63 +1,59 @@
 # account-management
 
 Project Description: Simple account-management app 
 
 ## Getting Started
 
 These instructions will help you set up the project on your local machine. For deployment notes, refer to the Deployment section.
 
 ### Prerequisites
 
 To run this project, you will need the following software and resources:
 
 - Visual Studio 2019 or a later version
 - .NET 5.0 or a later version
 - Entity Framework Core
 - ASP.NET Identity for membership and authentication
 - Role-based authorization
 - Cookies for user session management
 
 ### Installation
 
 1. Clone this repository to your local machine:
-   
 
    ```bash
    git clone https://github.com/user/project-name.git
- ```
+   ```
 
- 1 - Open the project in Visual Studio.
- 2 - Use the Package Manager Console to create the database and seed it with sample data:
+2. Open the project in Visual Studio.
 
+3. Use the Package Manager Console to update the database and seed it with sample data:
 
-
-# Run the following command to update the database and seed it with sample data.
-Update-Database
-
-
- 
+   ```
+   Update-Database
+   ```
 
 ## Usage
 You can provide examples and instructions on how to use the project here.
 
 # Features
 1- Membership management
 2 - Member login functionality
 3 - MVC (Model-View-Controller) design
 4 - Entity Framework Core for database operations
 5 - Role-based authorization
 6 - User authentication
 7 - Cookie-based user session management
 
 ## Contributing
 
 Fork this project and clone it to your local machine.
 Make changes to the code to add new features or fix bugs.
 Commit your changes with appropriate descriptions.
 Push your changes to your forked repository.
 Create a Pull Request.
 
 ## License
 This project is licensed under the MIT License. For more information, please see the LICENSE file.
 
 ## Contact
