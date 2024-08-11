CREATE DATABASE QuizDB;
USE QuizDB;
CREATE TABLE Questions (
    Id INT PRIMARY KEY IDENTITY(1,1),
    QuestionText VARCHAR(255),
    Option1 VARCHAR(100),
    Option2 VARCHAR(100),
    Option3 VARCHAR(100),
    Option4 VARCHAR(100),
    CorrectAnswer VARCHAR(100)
);
drop table Questions;

INSERT INTO Questions (QuestionText, Option1, Option2, Option3, Option4, CorrectAnswer)
VALUES ('What is the capital of India?', 'Mumbai', 'Delhi', 'Agra', 'Bangalore', 'Delhi');


INSERT INTO Questions (QuestionText, Option1, Option2, Option3, Option4, CorrectAnswer)
VALUES ('What is the national animal of India?', 'Elephant', 'Lion', 'Tiger', 'Peacock', 'Tiger');