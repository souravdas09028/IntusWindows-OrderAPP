# IntusWindows-Ordering APP
Description

A Blazor WebAssembly app for capturing order data.

Task Details

    Create new database tables Using Code First In Entity Framework.
    Blazor WebAssembly app with an interface to show data from DB.
    Make an ability to change and save data in the application: state, name, and dimensions.
    Add the ability to create and delete orders, windows and elements.
    Optional: Interface validations. DTO. Separated BLL and DAL projects.

Tools and SDk

    Visual Studio 2022
    MS SQL Server
    .NET 6.0 SDK

How to run the project

    1.Clone the repository from GitHub git clone https://github.com/souravdas09028/IntusWindows-OrderAPP.git

    2.Run the project in Visual Studio 2022

    3.Go to Package Manager Console

    4.Run this command in Package Manager Console.
    Update-Database -Project IntusWindows.Core -StartupProject IntusWindows.API
    
    5.Set StartUp Projects : Multiple project -> IntusWindows.API and IntusWindows.Web
    ![image](https://user-images.githubusercontent.com/59077852/234049181-1d9bbefb-2035-4c87-8337-9460b1eab639.png)
    
    6.Run the project

