#Programer: William Pring
#File Makefile
#Project Makefile
#Date 2/11/15


#Marco for windows 
OBJ = obj\main.obj obj\checkUserInput.obj obj\checkRandomNumber.obj obj\getRandomNumber.obj
EXE =bin\application.exe
CC =cl
OUTPUT_FLAG =/Fe
MAIN =obj\main.obj
CHECK_USER_INPUT =obj\checkUserInput.obj
CHECK_RANDOM_NUMBER =obj\checkRandomNumber.obj
GET_RANDOM_NUMBER =obj\getRandomNumber.obj
CFLAG =/c /Fo
STD_HEADER =inc/Header.h
MAIN_C =src\main.c
CHECK_USER_INPUT_C =src\checkUserInput.c
CHECK_RANDOM_NUMBER_C =src\checkRandomNumber.c
GET_RANDOM_NUMBER_C =src\getRandomNumber.c




#Marco for linux
#LD=cc
#CFLAG=-c -o 
#CC=cc
#OUTPUT_FLAG=-o 
#OBJ=obj/main.o obj/checkUserInput.o obj/checkRandomNumber.o obj/getRandomNumber.o
#STD_HEADER=inc/Header.h
#EXE=bin/application.exe
 
#MAIN=obj/main.o 
#CHECK_USER_INPUT=obj/checkUserInput.o
#CHECK_RANDOM_NUMBER=obj/checkRandomNumber.o
#GET_RANDOM_NUMBER=obj/getRandomNumber.o

#MAIN_C=src/main.c 

#CHECK_USER_INPUT_C=src/checkUserInput.c
#CHECK_RANDOM_NUMBER_C=src/checkRandomNumber.c
#GET_RANDOM_NUMBER_C=src/getRandomNumber.c

$(EXE): $(OBJ)
	$(CC) $(OBJ) $(OUTPUT_FLAG)$(EXE)

$(MAIN): $(MAIN_C) $(STD_HEADER) 
	$(CC) $(MAIN_C) $(CFLAG)$(MAIN) 

$(CHECK_USER_INPUT): $(CHECK_USER_INPUT_C) $(STD_HEADER)
	$(CC) $(CHECK_USER_INPUT_C) $(CFLAG)$(CHECK_USER_INPUT)

$(CHECK_RANDOM_NUMBER): $(CHECK_RANDOM_NUMBER_C) $(STD_HEADER)
	$(CC) $(CHECK_RANDOM_NUMBER_C) $(CFLAG)$(CHECK_RANDOM_NUMBER) 

$(GET_RANDOM_NUMBER): $(GET_RANDOM_NUMBER_C) $(STD_HEADER)
	$(CC) $(GET_RANDOM_NUMBER_C) $(CFLAG)$(GET_RANDOM_NUMBER) 

