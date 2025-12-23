# Society Management System (SMS)

A comprehensive web-based platform for managing residential societies, built using ASP.NET Web Forms. The system provides digital solutions for maintenance payment management, resident administration, notice board communication, and financial tracking.

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Installation & Setup](#installation--setup)
- [Configuration](#configuration)
- [Usage](#usage)
- [Key Modules](#key-modules)

## 🎯 Overview

Society Management System (SMS) is an innovative web-based platform developed to simplify and modernize residential society management. The system empowers administrators and residents by providing user-friendly tools for efficient maintenance operations, transparent financial transactions, and seamless communication.

### Mission
To modernize residential society management by providing innovative digital solutions that enhance efficiency, transparency, and community engagement.

### Vision
A future where society living is seamless and harmonious, enabled by technology that simplifies administrative tasks, fosters communication, and promotes a sense of belonging.

## ✨ Features

### Primary Features

1. **Digital Payments**
   - Seamless online maintenance dues payment
   - Multiple payment types (Maintenance, Service, Balance)
   - Payment history tracking
   - Transaction records management

2. **Virtual Notice Board**
   - Centralized announcement system
   - Society events and important notices
   - Accessible to all residents
   - Admin-controlled content management

3. **Defaulter List Management**
   - Automated identification of residents with outstanding dues
   - Defaulters list for both admin and residents
   - Payment reminders and tracking

### Additional Features

- **Resident Management**: Complete resident profile management system
- **Admin Dashboard**: Comprehensive admin control panel
- **Payment History**: Detailed transaction and payment records
- **Maintenance Schedule**: Scheduling and tracking of maintenance activities
- **User Authentication**: Separate login portals for residents and administrators
- **Profile Management**: User profile customization and management

## 🛠️ Tech Stack

### Backend Technologies

- **Framework**: ASP.NET Web Forms
- **Language**: C# (C-Sharp)
- **Runtime**: .NET Framework 4.8
- **Development Environment**: Visual Studio
- **Code Compilation**: Microsoft.CodeDom.Providers.DotNetCompilerPlatform (v2.0.1)

### Database

- **Database Server**: Microsoft SQL Server Express
- **Database Access**: ADO.NET with SqlConnection
- **Connection Method**: Integrated Security (Windows Authentication)

### Frontend Technologies

- **CSS Framework**: Bootstrap (Minified)
- **JavaScript Library**: jQuery 3.3.1 (Slim)
- **UI Components**: Popper.js (for Bootstrap tooltips/popovers)
- **Data Tables**: DataTables jQuery Plugin
- **Icons**: Font Awesome (Complete library with all icon sets)
- **Custom Styling**: Custom CSS stylesheets

### Development Tools

- **Package Manager**: NuGet
- **Build Tool**: MSBuild
- **Web Server**: IIS Express (for development)
- **Version Control**: Git (recommended)

## 📁 Project Structure

```
SMS/
├── SMS/                          # Main application folder
│   ├── *.aspx                   # Web Forms pages
│   ├── *.aspx.cs                # Code-behind files
│   ├── *.aspx.designer.cs       # Designer files
│   ├── Site1.Master             # Main master page
│   ├── Site2Test.Master         # Test master page
│   ├── Web.config               # Application configuration
│   ├── packages.config          # NuGet packages
│   └── SMS.csproj               # Project file
├── bootstrap/                    # Bootstrap framework files
│   ├── css/
│   │   └── bootstrap.min.css
│   └── js/
│       ├── bootstrap.min.js
│       ├── jquery-3.3.1.slim.min.js
│       └── popper.min.js
├── datatables/                  # DataTables plugin
│   ├── css/
│   │   └── jquery.dataTables.min.css
│   └── js/
│       └── jquery.dataTables.min.js
├── fontawesome/                 # Font Awesome icons library
│   ├── css/
│   ├── js/
│   ├── svgs/
│   └── webfonts/
├── css/                         # Custom stylesheets
│   └── custumstylesheet.css
├── imgs/                        # Image assets
├── bin/                         # Compiled binaries
├── obj/                         # Build artifacts
└── Properties/                  # Assembly information
```

## 🔧 Prerequisites

Before setting up the project, ensure you have the following installed:

- **.NET Framework 4.8** or later
- **Microsoft Visual Studio** 2017 or later (with ASP.NET and web development workload)
- **SQL Server Express** or higher (with SQL Server Management Studio)
- **IIS Express** (usually included with Visual Studio)
- **NuGet Package Manager** (included with Visual Studio)

## 📦 Installation & Setup

### 1. Clone the Repository

```bash
git clone <repository-url>
cd SMS
```

### 2. Database Setup

1. Open SQL Server Management Studio (SSMS)
2. Create a new database named `sms`
3. Import or run the database schema scripts (if available)
4. Ensure SQL Server authentication is configured

### 3. Configure Connection String

1. Open `SMS/Web.config`
2. Update the connection string in the `<connectionStrings>` section:

```xml
<connectionStrings>
    <add name="con" 
         connectionString="Data Source=YOUR_SERVER_NAME\SQLEXPRESS;Initial Catalog=sms;Integrated Security=True"/>
</connectionStrings>
```

**Note**: Replace `YOUR_SERVER_NAME` with your SQL Server instance name.

### 4. Restore NuGet Packages

1. Open the solution in Visual Studio
2. Right-click on the solution in Solution Explorer
3. Select "Restore NuGet Packages"

Or use the Package Manager Console:
```powershell
Update-Package -reinstall
```

### 5. Build the Project

1. In Visual Studio, go to `Build` → `Build Solution` (or press `Ctrl+Shift+B`)
2. Ensure the build completes without errors

### 6. Run the Application

1. Press `F5` or click the "Start" button in Visual Studio
2. The application will launch in your default browser
3. IIS Express will start automatically

## ⚙️ Configuration

### Application Settings

The `Web.config` file contains important configuration:

- **Validation Settings**: Unobtrusive validation mode disabled
- **Target Framework**: .NET Framework 4.8
- **Compilation**: Debug mode enabled for development
- **Connection String**: Database connection configuration

### Database Connection

The application uses Integrated Security (Windows Authentication) by default. For SQL Server Authentication, modify the connection string:

```xml
connectionString="Data Source=SERVER;Initial Catalog=sms;User ID=username;Password=password;"
```

## 💻 Usage

### Admin Portal

- Navigate to `/adminlogin.aspx` to access the admin login
- Admins can:
  - Manage residents
  - View and manage payments
  - Post notices on the virtual notice board
  - View defaulter lists
  - Manage maintenance schedules
  - Track all transactions

### Resident Portal

- Navigate to `/residentlogin.aspx` to access resident login
- Residents can:
  - View their profile
  - Make maintenance payments
  - View payment history
  - Access the notice board
  - Check defaulter lists
  - View maintenance schedules

## 📑 Key Modules

### Authentication & Authorization

- `adminlogin.aspx` - Administrator login
- `residentlogin.aspx` - Resident login
- `residentsignup.aspx` - New resident registration

### Resident Management

- `residentmanagement.aspx` - Admin view for managing residents
- `ResidentsView.aspx` - View resident details
- `residentprofile.aspx` - Resident profile page

### Payment Management

- `adminMaintenancePayment.aspx` - Admin payment management
- `residentMaintenancePayment.aspx` - Resident payment interface
- `AdminPaymentManagement.aspx` - Comprehensive payment admin panel
- `ResidentsPaymentHistory.aspx` - Payment history view
- `SocietyPayments.aspx` - Society-wide payment overview
- `Transactions.aspx` - Transaction records

### Notice Board

- `AdminNoticeBoard.aspx` - Admin notice management
- `ResidentNoticeBoard.aspx` - Resident notice view

### Defaulter Management

- `AdminDefaulterList.aspx` - Admin defaulter list
- `ResidentDefaulterList.aspx` - Resident defaulter view

### Other Pages

- `homepage.aspx` - Landing page
- `AboutUs.aspx` - About us page
- `maintenanceSchedule.aspx` - Maintenance scheduling

## 🔒 Security Considerations

- Always use HTTPS in production
- Implement proper authentication and authorization
- Sanitize user inputs to prevent SQL injection
- Use parameterized queries for database operations
- Store sensitive configuration in secure locations
- Regularly update dependencies and packages

## 📝 Notes

- The application uses Windows Authentication for database access by default
- Custom CSS is located in `css/custumstylesheet.css`
- Font Awesome provides comprehensive icon support
- DataTables plugin enhances data presentation with sorting, filtering, and pagination
- The project follows ASP.NET Web Forms architecture with master pages for consistent layout

## 🤝 Contributing

When contributing to this project:

1. Follow the existing code style and structure
2. Ensure all changes are tested before committing
3. Update documentation for any significant changes
4. Use meaningful commit messages

## 📄 License

[Specify your license here]

## 👥 Contact

For queries or support:
- Email: siddheshm303@gmail.com

---

**Developed with ASP.NET Web Forms | .NET Framework 4.8**
