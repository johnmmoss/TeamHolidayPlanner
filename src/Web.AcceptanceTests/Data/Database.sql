DELETE FROM Employees
GO
DELETE FROM Department
GO
DELETE FROM EmploymentGrade
GO
DELETE FROM HolidayPeriod
GO
DELETE FROM EmployeeHoliday
GO
DELETE FROM RolePermission
GO
DELETE FROM [User]
GO
DELETE FROM Roles
GO
DELETE FROM Permission
GO

SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([DepartmentID], [Name], [GroupName], [CreatedDate], [ModifiedDate]) VALUES (1, N'Helpdesk', N'IT', CAST(N'2018-09-12 12:12:00.0000000' AS DateTime2), CAST(N'2018-09-10 12:12:00.0000000' AS DateTime2))
INSERT [dbo].[Department] ([DepartmentID], [Name], [GroupName], [CreatedDate], [ModifiedDate]) VALUES (3, N'Desktop Support', N'IT', CAST(N'2018-09-22 12:12:00.0000000' AS DateTime2), CAST(N'2018-09-22 12:12:00.0000000' AS DateTime2))
INSERT [dbo].[Department] ([DepartmentID], [Name], [GroupName], [CreatedDate], [ModifiedDate]) VALUES (4, N'Development', N'IT', CAST(N'2018-09-22 12:12:00.0000000' AS DateTime2), CAST(N'2018-09-21 12:12:00.0000000' AS DateTime2))
INSERT [dbo].[Department] ([DepartmentID], [Name], [GroupName], [CreatedDate], [ModifiedDate]) VALUES (5, N'HR', N'Corporate Services', CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Department] ([DepartmentID], [Name], [GroupName], [CreatedDate], [ModifiedDate]) VALUES (6, N'Finance', N'Corporate Services', CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Department] ([DepartmentID], [Name], [GroupName], [CreatedDate], [ModifiedDate]) VALUES (7, N'Business Support', N'Corporate Services', CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Department] OFF
SET IDENTITY_INSERT [dbo].[EmploymentGrade] ON 

INSERT [dbo].[EmploymentGrade] ([EmploymentGradeID], [Name], [Grade], [AnnualLeaveEntitlement], [CreatedDate], [ModifiedDate]) VALUES (1, N'Assistant', N'1', 20, CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EmploymentGrade] ([EmploymentGradeID], [Name], [Grade], [AnnualLeaveEntitlement], [CreatedDate], [ModifiedDate]) VALUES (2, N'Junior', N'2', 23, CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EmploymentGrade] ([EmploymentGradeID], [Name], [Grade], [AnnualLeaveEntitlement], [CreatedDate], [ModifiedDate]) VALUES (3, N'Mid-Level', N'5', 26, CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EmploymentGrade] ([EmploymentGradeID], [Name], [Grade], [AnnualLeaveEntitlement], [CreatedDate], [ModifiedDate]) VALUES (4, N'Senior', N'6', 28, CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EmploymentGrade] ([EmploymentGradeID], [Name], [Grade], [AnnualLeaveEntitlement], [CreatedDate], [ModifiedDate]) VALUES (5, N'Board', N'7', 30, CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[EmploymentGrade] OFF
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleID], [Name], [Description]) VALUES (1, N'Admin', N'Full system access')
INSERT [dbo].[Roles] ([RoleID], [Name], [Description]) VALUES (2, N'Manager', N'Manages employee holidays')
INSERT [dbo].[Roles] ([RoleID], [Name], [Description]) VALUES (3, N'Employee', N'Books holidays')
SET IDENTITY_INSERT [dbo].[Roles] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [UserName], [PasswordHash], [RoleID]) VALUES (1, N'johnmmoss', N'AQAAAAEAACcQAAAAEGOx66dVs1PgFIdUGyLSH1ReIKSZJQ9JdJkawHReAUrH/Zj4B8WzfTMnhFxkcFLOhw==', 1)
INSERT [dbo].[User] ([UserID], [UserName], [PasswordHash], [RoleID]) VALUES (2, N'johnmmoss2', N'AQAAAAEAACcQAAAAEO7FkAQ7b5yAeOpGZBQ8PzNqmRqxoOiRHBk+e8r8T5QZjhe/YI4jrCPH8aTetEucrA==', NULL)
INSERT [dbo].[User] ([UserID], [UserName], [PasswordHash], [RoleID]) VALUES (3, N'johnmmoss3', N'AQAAAAEAACcQAAAAEFGzo5KwnvBddu1FAB8+ZhO4N2iu6dnwv6qjdgUqDBRDXWOErFOPofWXWuyIYZoiQg==', NULL)
INSERT [dbo].[User] ([UserID], [UserName], [PasswordHash], [RoleID]) VALUES (4, N'johnmmoss4', N'AQAAAAEAACcQAAAAEBy5O+MRIjGeXOezlgRxXBbB4HSHlKBlISDj5wMg6wItXqHnRZsV//DwZt4Tg8ymag==', NULL)
INSERT [dbo].[User] ([UserID], [UserName], [PasswordHash], [RoleID]) VALUES (5, N'johnmmoss5', N'AQAAAAEAACcQAAAAEKC9uApkSx2giyzJKBtm2K113GI1sJDM5wgjduVg1aZP8R6T32k3JQJ6BzpY+FXt6A==', NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([EmployeeID], [JobTitle], [BirthDate], [Title], [FirstName], [MiddleName], [LastName], [MaritalStatus], [Gender], [HireDate], [EmploymentGradeID], [DepartmentID], [CreatedDate], [ModifiedDate], [UserID]) VALUES (1, N'Software Developer', CAST(N'2018-09-22' AS Date), N'Mr', N'Peter', NULL, N'Fisher', N'S', N'M', CAST(N'2012-09-20' AS Date), 3, 4, CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Employees] ([EmployeeID], [JobTitle], [BirthDate], [Title], [FirstName], [MiddleName], [LastName], [MaritalStatus], [Gender], [HireDate], [EmploymentGradeID], [DepartmentID], [CreatedDate], [ModifiedDate], [UserID]) VALUES (4, N'Senior Software Developer', CAST(N'2017-01-21' AS Date), N'Mr', N'Bill', NULL, N'Wainwright', N'M', N'M', CAST(N'2018-09-22' AS Date), 4, 4, CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Employees] ([EmployeeID], [JobTitle], [BirthDate], [Title], [FirstName], [MiddleName], [LastName], [MaritalStatus], [Gender], [HireDate], [EmploymentGradeID], [DepartmentID], [CreatedDate], [ModifiedDate], [UserID]) VALUES (5, N'Junior Developer', CAST(N'1999-08-19' AS Date), N'Mr', N'Phil', NULL, N'Lima', N'S', N'M', CAST(N'2017-09-21' AS Date), 2, 4, CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Employees] ([EmployeeID], [JobTitle], [BirthDate], [Title], [FirstName], [MiddleName], [LastName], [MaritalStatus], [Gender], [HireDate], [EmploymentGradeID], [DepartmentID], [CreatedDate], [ModifiedDate], [UserID]) VALUES (6, N'Junior Developer', CAST(N'1999-07-01' AS Date), N'Mr', N'Jimmy', NULL, N'Brooks', N'S', N'M', CAST(N'2017-09-21' AS Date), 2, 4, CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Employees] ([EmployeeID], [JobTitle], [BirthDate], [Title], [FirstName], [MiddleName], [LastName], [MaritalStatus], [Gender], [HireDate], [EmploymentGradeID], [DepartmentID], [CreatedDate], [ModifiedDate], [UserID]) VALUES (7, N'Junior Developer', CAST(N'1998-07-23' AS Date), N'Miss', N'Daphne', NULL, N'Middlemiss', N'S', N'F', CAST(N'2017-09-21' AS Date), 2, 4, CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Employees] ([EmployeeID], [JobTitle], [BirthDate], [Title], [FirstName], [MiddleName], [LastName], [MaritalStatus], [Gender], [HireDate], [EmploymentGradeID], [DepartmentID], [CreatedDate], [ModifiedDate], [UserID]) VALUES (8, N'Development Manager', CAST(N'1976-02-12' AS Date), N'Mr', N'Stephen', NULL, N'Blakely', N'M', N'M', CAST(N'2007-04-03' AS Date), 4, 4, CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Employees] ([EmployeeID], [JobTitle], [BirthDate], [Title], [FirstName], [MiddleName], [LastName], [MaritalStatus], [Gender], [HireDate], [EmploymentGradeID], [DepartmentID], [CreatedDate], [ModifiedDate], [UserID]) VALUES (9, N'HR Assistant', CAST(N'1997-02-12' AS Date), N'Miss', N'Haley', NULL, N'Smith', N'S', N'F', CAST(N'2007-02-13' AS Date), 1, 5, CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Employees] ([EmployeeID], [JobTitle], [BirthDate], [Title], [FirstName], [MiddleName], [LastName], [MaritalStatus], [Gender], [HireDate], [EmploymentGradeID], [DepartmentID], [CreatedDate], [ModifiedDate], [UserID]) VALUES (10, N'HR Manager', CAST(N'1975-02-04' AS Date), N'Mrs', N'Belinda', NULL, N'Waite', N'M', N'F', CAST(N'2008-04-17' AS Date), 4, 5, CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[Employees] OFF
SET IDENTITY_INSERT [dbo].[HolidayPeriod] ON 

INSERT [dbo].[HolidayPeriod] ([HolidayPeriodID], [Description], [Startdate], [EndDate], [CreatedDate], [ModifiedDate]) VALUES (4, N'2017', CAST(N'2017-01-01 00:00:00.0000000' AS DateTime2), CAST(N'2017-12-30 00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2))
INSERT [dbo].[HolidayPeriod] ([HolidayPeriodID], [Description], [Startdate], [EndDate], [CreatedDate], [ModifiedDate]) VALUES (5, N'2018', CAST(N'2018-01-01 00:00:00.0000000' AS DateTime2), CAST(N'2018-12-30 00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01 00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[HolidayPeriod] OFF
SET IDENTITY_INSERT [dbo].[Permission] ON 

INSERT [dbo].[Permission] ([PermissionID], [Name], [Description]) VALUES (1, N'DepartmentIndex', N'')
INSERT [dbo].[Permission] ([PermissionID], [Name], [Description]) VALUES (2, N'DepartmentDetails', N'')
INSERT [dbo].[Permission] ([PermissionID], [Name], [Description]) VALUES (3, N'DepartmentCreate', N'')
INSERT [dbo].[Permission] ([PermissionID], [Name], [Description]) VALUES (4, N'DepartmentEdit', NULL)
INSERT [dbo].[Permission] ([PermissionID], [Name], [Description]) VALUES (5, N'DepartmentDelete', NULL)
INSERT [dbo].[Permission] ([PermissionID], [Name], [Description]) VALUES (6, N'EmployeeIndex', NULL)
INSERT [dbo].[Permission] ([PermissionID], [Name], [Description]) VALUES (7, N'EmployeeDetails', NULL)
INSERT [dbo].[Permission] ([PermissionID], [Name], [Description]) VALUES (8, N'EmployeeCreate', NULL)
INSERT [dbo].[Permission] ([PermissionID], [Name], [Description]) VALUES (9, N'EmployeeEdit', NULL)
INSERT [dbo].[Permission] ([PermissionID], [Name], [Description]) VALUES (10, N'EmployeeDelete', NULL)
INSERT [dbo].[Permission] ([PermissionID], [Name], [Description]) VALUES (11, N'EmploymentGradeIndex', NULL)
INSERT [dbo].[Permission] ([PermissionID], [Name], [Description]) VALUES (12, N'EmploymentGradeDetails', NULL)
INSERT [dbo].[Permission] ([PermissionID], [Name], [Description]) VALUES (13, N'EmploymentGradeCreate', NULL)
INSERT [dbo].[Permission] ([PermissionID], [Name], [Description]) VALUES (14, N'EmploymentGradeEdit', NULL)
INSERT [dbo].[Permission] ([PermissionID], [Name], [Description]) VALUES (15, N'EmploymentGradeDelete', NULL)
INSERT [dbo].[Permission] ([PermissionID], [Name], [Description]) VALUES (16, N'HolidayPeriodIndex', NULL)
INSERT [dbo].[Permission] ([PermissionID], [Name], [Description]) VALUES (17, N'HolidayPeriodDetails', NULL)
INSERT [dbo].[Permission] ([PermissionID], [Name], [Description]) VALUES (18, N'HolidayPeriodCreate', NULL)
INSERT [dbo].[Permission] ([PermissionID], [Name], [Description]) VALUES (19, N'HolidayPeriodEdit', NULL)
SET IDENTITY_INSERT [dbo].[Permission] OFF
INSERT [dbo].[RolePermission] ([RoleID], [PermissionID]) VALUES (1, 1)
INSERT [dbo].[RolePermission] ([RoleID], [PermissionID]) VALUES (1, 2)
INSERT [dbo].[RolePermission] ([RoleID], [PermissionID]) VALUES (1, 3)
INSERT [dbo].[RolePermission] ([RoleID], [PermissionID]) VALUES (1, 4)
INSERT [dbo].[RolePermission] ([RoleID], [PermissionID]) VALUES (1, 5)
INSERT [dbo].[RolePermission] ([RoleID], [PermissionID]) VALUES (1, 6)
INSERT [dbo].[RolePermission] ([RoleID], [PermissionID]) VALUES (1, 7)
INSERT [dbo].[RolePermission] ([RoleID], [PermissionID]) VALUES (1, 8)
INSERT [dbo].[RolePermission] ([RoleID], [PermissionID]) VALUES (1, 9)
INSERT [dbo].[RolePermission] ([RoleID], [PermissionID]) VALUES (1, 10)
INSERT [dbo].[RolePermission] ([RoleID], [PermissionID]) VALUES (1, 11)
INSERT [dbo].[RolePermission] ([RoleID], [PermissionID]) VALUES (1, 12)
INSERT [dbo].[RolePermission] ([RoleID], [PermissionID]) VALUES (1, 13)
INSERT [dbo].[RolePermission] ([RoleID], [PermissionID]) VALUES (1, 14)
INSERT [dbo].[RolePermission] ([RoleID], [PermissionID]) VALUES (1, 15)
INSERT [dbo].[RolePermission] ([RoleID], [PermissionID]) VALUES (1, 16)
INSERT [dbo].[RolePermission] ([RoleID], [PermissionID]) VALUES (1, 17)
INSERT [dbo].[RolePermission] ([RoleID], [PermissionID]) VALUES (1, 18)
INSERT [dbo].[RolePermission] ([RoleID], [PermissionID]) VALUES (1, 19)
