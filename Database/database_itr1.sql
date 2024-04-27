create database SE;
-- drop database SE;
use SE;


create table supervisorUpload (
	upload_id INT PRIMARY KEY,
    team_id INT,
    fileName VARCHAR(200),
    filePath VARCHAR(300),
    fileDescription VARCHAR(500)
);

create table studentUpload (
	upload_id INT PRIMARY KEY,
    team_id INT,
    fileName VARCHAR(200),
    filePath VARCHAR(200),
    fileDescription VARCHAR(500),
    fileDateTime datetime,
    isGradable bool,
    weightage float,
    totalMarks float,
    obtainedMarks float,
    comments VARCHAR(500)
);


INSERT INTO studentUpload VALUES (0, 0, null, null, null, null, null, null, null, null, null);
INSERT INTO supervisorUpload VALUES (0,0,'','','');

create table Teams (
	team_id INT PRIMARY KEY,
	roll_number_1  varchar(10),
    roll_number_2  varchar(10),
    roll_number_3  varchar(10),
    supervisor_id varchar(10),
    team_name varchar(15),
    fyp_year INT,
    mission_statement varchar(100),
    approved bool
);

create table announcements (
	team_id INT,
    announcement_id INT PRIMARY KEY,
    announcement_by_id VARCHAR(10),
    announcement_to_id VARCHAR(10),
    announcement_text VARCHAR(200),
	viewewd bool
);

INSERT INTO announcements VALUES (0, 0,'','','',0);

drop table deadlines;

create table deadlines (
	team_id INT,
    deadline_id INT PRIMARY KEY,
    deadline_text VARCHAR(200),
    deadline_date DATE,
    deadline_met bool
);

INSERT INTO deadlines VALUES (0, 0,NULL,NULL,0);
INSERT INTO TEAMS VALUES (0, '','','','','',0,'',1);

 
create table repository(
	repos_id int primary key,
	roll_number_1  varchar(10),
    roll_number_2  varchar(10),
    roll_number_3  varchar(10),
    supervisor_id varchar(10),
	mission_statement varchar(100),
    report_link varchar(50),
    languages varchar(20),
    short_title varchar(20)
);

CREATE TABLE User (
    Username VARCHAR(10) UNIQUE NOT NULL,
    FName VARCHAR(30) NOT NULL,
    MName VARCHAR(30),
    LName VARCHAR(30),
    PhoneNum VARCHAR(30) NOT NULL,
    Type VARCHAR(10) NOT NULL CHECK (Type in ('S', 'A', 'T')),
    Gender VARCHAR(10) NOT NULL CHECK (Gender in ('M', 'F')),
    Email VARCHAR(50) NOT NULL,
    Address VARCHAR(50) NOT NULL,
    CGPA FLOAT,
    Degree varchar(50),
    DOB DATE,

    PRIMARY KEY (Username)
);

INSERT INTO User (Username, FName, MName, LName, PhoneNum, Type, Gender, Email, Address, CGPA, Degree, DOB)
VALUES
('i21-1234', 'John', 'M', 'Doe', '123-456-7890', 'S', 'M', 'john.doe@example.com', '123 Main St, Anytown, USA', 3.7, 'Bachelor of Science', '1995-08-15'),
('i21-5678', 'Jane', 'F', 'Smith', '987-654-3210', 'S', 'F', 'jane.smith@example.com', '456 Elm St, Othertown, USA', 3.9, 'Bachelor of Arts', '1996-03-20'),
('i21-9012', 'James', 'M', 'Johnson', '555-123-4567', 'S', 'M', 'james.johnson@example.com', '789 Oak St, Another Town, USA', 3.5, 'Bachelor of Engineering', '1997-11-10'),
('i21-2468', 'Emily', 'F', 'Brown', '777-888-9999', 'S', 'F', 'emily.brown@example.com', '321 Pine St, Somewhere, USA', 3.8, 'Bachelor of Business Administration', '1998-05-25'),
('i21-1357', 'Michael', 'M', 'Wilson', '444-555-6666', 'S', 'M', 'michael.wilson@example.com', '654 Maple St, Anywhere, USA', 3.6, 'Bachelor of Medicine', '1996-09-30'),
('i21-9753', 'Sophia', 'F', 'Miller', '222-333-4444', 'S', 'F', 'sophia.miller@example.com', '987 Cedar St, Nowhere, USA', 3.9, 'Bachelor of Science', '1997-07-05'),
('s-9342', 'Matthew', 'M', 'Martinez', '111-222-3333', 'T', 'M', 'matthew.martinez@example.com', '111 Pineapple St, Elsewhere, USA', NULL, NULL, '1977-11-05'),
('s-7685', 'Olivia', 'F', 'Taylor', '999-888-7777', 'T', 'F', 'olivia.taylor@example.com', '777 Strawberry St, Here, USA', NULL, NULL, '1980-04-10'),
('a-1234', 'Anna', 'F', 'Anderson', '444-555-6666', 'A', 'F', 'anna.anderson@example.com', '123 Lemon St, Anytown, USA', NULL, NULL, '1985-07-20'),
('a-5678', 'Jacob', 'M', 'Smith', '777-666-5555', 'A', 'M', 'jacob.smith@example.com', '456 Orange Ave, Somewhere, USA', NULL, NULL, '1990-02-15');

CREATE TABLE Login (
    Username VARCHAR(20) UNIQUE NOT NULL,
    Password VARCHAR(60) NOT NULL,

    FOREIGN KEY (Username) REFERENCES User (Username),
    PRIMARY KEY (Username) 
);

INSERT INTO Login (Username, Password) VALUES
('i21-1234', '1234'),
('i21-5678', '1234'),
('i21-9012', '1234'),
('i21-2468', '1234'),
('i21-1357', '1234'),
('i21-9753', '1234'),
('s-9342', '1234'),
('s-7685', '1234'),
('a-1234', '1234'),
('a-5678', '1234');


CREATE TABLE FYPRepository (
	Title VARCHAR(200),
    Description VARCHAR(2000),
    Link VARCHAR(200),
    Tools VARCHAR(300)
);
