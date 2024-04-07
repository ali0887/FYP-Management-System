create database SE;
-- drop database SE;
use SE;

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

INSERT INTO TEAMS VALUES (0, '','','','','',0,'',1);
-- DROP TABLE Teams;

-- SELECT * FROM TEAMS;
-- SELECT * FROM Teams WHERE approved != 1;

INSERT INTO Teams (team_id, roll_number_1, roll_number_2, roll_number_3, supervisor_id, team_name, fyp_year, mission_statement, approved)
VALUES 
(1,'i21-1234', 'i21-5678', 'i21-9012', 's-9342', 'TechTitans', 2025, 'Creating a sustainable energy solution for rural areas.', true),
(2,'i21-2468', 'i21-1357', 'i21-9753', 's-7685', 'ByteBuilders', 2025, 'Developing a machine learning algorithm for early disease detection.', false),
(3,'i21-9876', 'i21-5432', 'i21-1098', 's-2387', 'InnovateX', 2025, 'Designing a mobile application to promote mental health awareness.', true),
(4,'i21-3698', 'i21-7854', 'i21-2134', 's-5902', 'CodeCrafters', 2025, 'Building an IoT-based smart home system for energy efficiency.', false),
(5,'i21-6543', 'i21-8901', 'i21-4321', 's-4092', 'CyberSquad', 2025, 'Creating a platform for online tutoring and skill development.', true),
(6,'i21-5678', 'i21-1234', 'i21-9012', 's-6812', 'DataDynamos', 2025, 'Developing an e-commerce platform for local artisans.', false),
(7,'i21-5432', 'i21-9876', 'i21-1098', 's-1258', 'TechTinkerers', 2025, 'Designing a low-cost water purification system for rural communities.', true),
(8,'i21-7854', 'i21-3698', 'i21-2134', 's-3476', 'LogicLegends', 2025, 'Creating a mobile application for waste management and recycling.', false),
(9,'i21-8901', 'i21-6543', 'i21-4321', 's-8023', 'InventiveMinds', 2025, 'Developing a virtual reality-based educational platform.', true),
(10,'i21-1234', 'i21-5678', 'i21-9012', 's-2159', 'DataDreamers', 2025, 'Designing a community-driven urban gardening system.', false);

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

INSERT INTO repository (repos_id, roll_number_1, roll_number_2, roll_number_3, supervisor_id, mission_statement, report_link, languages, short_title)
VALUES 
(1,'i21-1234', 'i21-5678', 'i21-9012', 's-9342', 'Creating a sustainable energy solution for rural areas.', 'http://example.com/report1', 'Python', 'EnergySol'),
(2,'i21-2468', 'i21-1357', 'i21-9753', 's-7685', 'Developing a machine learning algorithm for early disease detection.', 'http://example.com/report2', 'Python, R', 'DiseaseML'),
(3,'i21-9876', 'i21-5432', 'i21-1098', 's-2387', 'Designing a mobile application to promote mental health awareness.', 'http://example.com/report3', 'Java', 'MentalHealth'),
(4,'i21-3698', 'i21-7854', 'i21-2134', 's-5902', 'Building an IoT-based smart home system for energy efficiency.', 'http://example.com/report4', 'C++, JavaScript', 'SmartHome'),
(5,'i21-6543', 'i21-8901', 'i21-4321', 's-4092', 'Creating a platform for online tutoring and skill development.', 'http://example.com/report5', 'Java, HTML, CSS', 'TutorPlat'),
(6,'i21-5678', 'i21-1234', 'i21-9012', 's-6812', 'Developing an e-commerce platform for local artisans.', 'http://example.com/report6', 'Python, JavaScript', 'ArtisanEcom'),
(7,'i21-5432', 'i21-9876', 'i21-1098', 's-1258', 'Designing a low-cost water purification system for rural communities.', 'http://example.com/report7', 'C, Arduino', 'WaterPurify'),
(8,'i21-7854', 'i21-3698', 'i21-2134', 's-3476', 'Creating a mobile application for waste management and recycling.', 'http://example.com/report8', 'Java, Kotlin', 'RecycleApp'),
(9,'i21-8901', 'i21-6543', 'i21-4321', 's-8023', 'Developing a virtual reality-based educational platform.', 'http://example.com/report9', 'C#, Unity', 'VR-Edu'),
(10,'i21-1234', 'i21-5678', 'i21-9012', 's-2159', 'Designing a community-driven urban gardening system.', 'http://example.com/report10', 'Python, JavaScript', 'UrbanGarden');

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