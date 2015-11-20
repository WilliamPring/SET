CREATE DATABASE Trivia;
Use Trivia;

Create Table User(
UserID INTEGER PRIMARY KEY AUTO_INCREMENT,
Name VARCHAR(45),
Password VarChar(45));

Create Table Question(
QuestionID INTEGER PRIMARY KEY,
QuestionContext VARCHAR(100),
AnswerA VarChar(45),
AnswerB VarChar(45),
AnswerC VarChar(45),
AnswerD VarChar(45),
CorrectAnswer VarChar(2));

Create Table UserAnswer(
UserAnswerID INTEGER PRIMARY KEY,
UserID INTEGER,
QuestionID INTEGER,
User_Answer VARCHAR(2),
Time INTEGER,
CONSTRAINT FOREIGN KEY (UserID) REFERENCES User(UserID),
CONSTRAINT FOREIGN KEY (QuestionID) REFERENCES Question(QuestionID));

INSERT INTO Question (QuestionID, QuestionContext, AnswerA, AnswerB, AnswerC, AnswerD, CorrectAnswer)
VALUES (1, 'What is 2+2', '2', '10', '4', '5', 'c');
INSERT INTO Question (QuestionID, QuestionContext, AnswerA, AnswerB, AnswerC, AnswerD, CorrectAnswer)
VALUES (2, 'What is 2*2', '4', '11', '2', '22', 'a');
INSERT INTO Question (QuestionID, QuestionContext, AnswerA, AnswerB, AnswerC, AnswerD, CorrectAnswer)
VALUES (3, 'What is 5*0', '50', '5', '10', '0', 'd');
INSERT INTO Question (QuestionID, QuestionContext, AnswerA, AnswerB, AnswerC, AnswerD, CorrectAnswer)
VALUES (4, 'What is 2+2*2', '8', '6', '4', '10', 'b');
INSERT INTO Question (QuestionID, QuestionContext, AnswerA, AnswerB, AnswerC, AnswerD, CorrectAnswer)
VALUES (5, 'What is 0+0', '0', '14', '50', '100', 'a');
INSERT INTO Question (QuestionID, QuestionContext, AnswerA, AnswerB, AnswerC, AnswerD, CorrectAnswer)
VALUES (6, 'What is 50/50', '50', '100', '1', '0', 'c');
INSERT INTO Question (QuestionID, QuestionContext, AnswerA, AnswerB, AnswerC, AnswerD, CorrectAnswer)
VALUES (7, 'What is 10+10', '20', '10', '0', '50', 'a');
INSERT INTO Question (QuestionID, QuestionContext, AnswerA, AnswerB, AnswerC, AnswerD, CorrectAnswer)
VALUES (8, 'What is 300/30', '3', '30', '10', '100', 'c');
INSERT INTO Question (QuestionID, QuestionContext, AnswerA, AnswerB, AnswerC, AnswerD, CorrectAnswer)
VALUES (9, 'What is 80+20', '20', '120', '100', '50', 'c');
INSERT INTO Question (QuestionID, QuestionContext, AnswerA, AnswerB, AnswerC, AnswerD, CorrectAnswer)
VALUES (10, 'What is 1+1', '2', '1', '4', '6', 'a');

INSERT INTO User(Name, Password)
VALUES ('root', 'root');