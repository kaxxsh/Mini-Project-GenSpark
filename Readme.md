# Railway Reservation System

## Overview

The Railway Reservation System is a comprehensive application designed to manage railway reservations, including station and train management, ticket bookings, and user authentication.

## Features

- **Admin Module**: Manage stations, trains, and tickets.
- **Authentication Module**: User registration, login, password management, and user profile updates.
- **User Module**: View stations, trains, and tickets, and manage personal bookings.

## API Endpoints

### Admin Endpoints

#### Station Management
- **Create Station**
  - `POST /api/Admin/Station`
- **Update Station**
  - `PUT /api/Admin/Station/{id}`
- **Delete Station**
  - `DELETE /api/Admin/Station/{id}`

#### Train Management
- **Get Trains**
  - `GET /api/Admin/Train`
- **Create Train**
  - `POST /api/Admin/Train`
- **Update Train**
  - `PUT /api/Admin/Train/{id}`
- **Delete Train**
  - `DELETE /api/Admin/Train/{id}`

#### Ticket Management
- **Get Tickets**
  - `GET /api/Admin/Ticket`
- **Update Ticket**
  - `PUT /api/Admin/Ticket/{id}`
- **Delete Ticket**
  - `DELETE /api/Admin/Ticket/{id}`
- **Get Pending Tickets**
  - `GET /api/Admin/Ticket/Pending`
- **Approve Ticket**
  - `GET /api/Admin/Ticket/Approve/{id}`
- **Get Booked Tickets by Train**
  - `GET /api/Admin/Ticket/Booked/{TrainId}`

### Authentication Endpoints

- **Register**
  - `POST /api/Auth/register`
- **Login**
  - `POST /api/Auth/login`
- **Reset Password**
  - `PUT /api/Auth/reset-password`
- **Delete User**
  - `DELETE /api/Auth/delete-user/{userId}`
- **Update User**
  - `PUT /api/Auth/update-user/{userId}`

### User Endpoints

- **Get Users**
  - `GET /api/User`
- **Get User by ID**
  - `GET /api/User/{id}`
- **Get User Tickets**
  - `GET /api/User/User/Ticket/{id}`
- **Cancel User Ticket**
  - `GET /api/User/User/Ticket/Cancel/{id}`

#### Station Information
- **Get Stations**
  - `GET /api/User/Station`
- **Get Station by ID**
  - `GET /api/User/Station/{id}`

#### Train Information
- **Get Trains**
  - `GET /api/User/Train`
- **Get Train by ID**
  - `GET /api/User/Train/{id}`

#### Ticket Management
- **Get Ticket by ID**
  - `GET /api/User/Ticket/{id}`
- **Book Ticket**
  - `POST /api/User/Ticket`
- **Cancel Ticket**
  - `PUT /api/User/Ticket/Cancel/{id}`

## Project Structure

- `.dockerignore` - Specifies files to be ignored by Docker.
- `.git` - Contains git version control files.
- `.gitignore` - Specifies files to be ignored by git.
- `RailwayReservation` - Core project directory containing source code.
- `RailwayReservationSystem.sln` - Solution file for the project.
- `Unit-Testing` - Directory containing unit tests.

## Setup

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Docker](https://www.docker.com/get-started) (optional, for containerized deployment)

### Running the Application

1. **Clone the Repository**

   ```sh
   git clone https://github.com/kaxxsh/Mini-Project-Genspark
   cd RailwayReservationSystem
   ```

2. **Build the Solution**

   ```sh
   dotnet build RailwayReservationSystem.sln
   ```

3. **Run the Application**

   ```sh
   dotnet run --project RailwayReservation
   ```

4. **Access the Application**

   Open a web browser and navigate to `http://localhost:7139`.

### Running Unit Tests

```sh
dotnet test Unit-Testing
```

