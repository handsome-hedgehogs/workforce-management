# Bangazon
## Workforce Management System
This management system allows the user to keep track of information related to employees, computers, training programs, and departments.

## Technical Requirements

### Visual Studio
[Visual Studio Community](https://www.visualstudio.com/downloads/) -- Download Visual studio (the Community edition is free).
Run the installer that gets downloaded, and on the first window that appears, click "Individual Components" at the top of the screen. Make sure the following items are checked:
 * .NET Core runtime
 * ASP.NET Web development tools
 * Nuget package manager
 * SQL Server LocalDB
 * Entity Framework 6
Click `Install` at the bottom.

### Git Bash
[Git Bash](https://git-scm.com/downloads) -- Download Git Bash 32-Bit version for Windows

## Follow These Steps to Run the Management System
After installing Visual Studio and Git Bash, create a new directory and cd into it. Once you are in your new directory, run the following command to clone the repo:
``git clone https://github.com/handsome-hedgehogs/workforce-management.git``

Once you have cloned down the repo into your directory, open Visual Studio. From Visual Studio, open the HandsomeHedgehogHoedown Solution file. Now go to the Tools tab, down to NuGet Package Manager and then Package Manager Console to open terminal in Visual Studio. In the terminal run the following command:
``Update-Database``

Once you have updated the database, now click the green arrow at the top of the page to run HandsomeHedgehogHoedown Solution. This will direct the user to a webpage with the management system. From there, follow these next steps to test the application.

## Managagement System Walkthrough
### Homepage
When landing on the Home page of the management system, the last five employees that have been added to the system, along with their corresponding department will be displayed. A list of upcoming training programs that will be starting in the next four weeks from the current day will also be available to view.
    
### Employees
When navigating to the Employees tab in the navigation bar, the page will display a list of all employees including their assigned department. The user will be able to click "Create New" to add a new employee to the system. When creating a new employee, the user will be prompted to enter the employee's first and last name, start date of their employment, and select a department to assign them to. 

User will also be able to edit a single employee and view their specific details. When clicking details, user will be directed to another view where they can see the employee's name, the department they're assigned to, their currently assigned computer, training programs they plan on attending, and past training programs they've attended.

When editing a specific employee, user will be able to change their last name, change the department they are in, change the computer they are assigned to, and add/remove training programs to attend in the future.

### Departments
When navigating to the Department tab in the navigation bar, user will be directed to a page that lists all departments in the system. When choosing "Create New", user will be able to add a new department with it's name to the system. They will also be able to view details of a specific department. When viewing details, name of the department will be displayed larger and will show a list of employees that are assigned to that department.

### Computers
When navigating to the Computers tab in the navigation bar, user will be directed to a view of all computers. When choosing "Create New", user will be prompted to add the computer's manufacturer, the make, and the purchase date of the computer. When choosing "Edit", user will only be able to enter a decomission date to the computer. 

When viewing a single computer, user will be able to "Delete" a computer from the system only if the computer has never been assigned to an employee. When choosing to delete, user will be directed to a page to confirm the removal of the computer.

### Training Programs
When navigating to the Training Programs tab in the navigation bar, user will be directed to a view of all training programs that have not taken place yet. When choosing "Create New", user will be prompted to enter the name of the program, description, the day the program begins, the day the program ends, and the max number of people that can attend.

When viewing a single training program, user will be able to see the details of that specific program. View will show any employees that are currently attending the program.