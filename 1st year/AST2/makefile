#UNIVERSAL
SOURCES =src\*.c
HEADERS =include\*.h
EXECUTABLE =bin\AST2.exe
OBJ =objects\checkInput.obj objects\createNumber.obj objects\getNum.obj objects\giveOutput.obj objects\numberGame.obj

#WINDOWS
CFLAGS =/c 
LDFLAGS =/Fe
OBJEXT =.obj
EXE =.exe
#END WINDOWS

#LINUX
#CFLAGS = -c
#LDFLAGS = -o
#OBJECTS = $(SOURCES:.c=.o)
#OBJEXT = .o
#EXE = 
#END LINUX


$(EXECUTABLE): $(OBJ)
	$(CC) $(OBJ) $(LDFLAGS)$(EXECUTABLE)

objects\checkInput$(OBJEXT): src\checkInput.c $(HEADERS) 
	$(CC) src\checkInput.c $(CFLAGS) /FoObjects\checkInput$(OBJEXT)

objects\createNumber$(OBJEXT): src\createNumber.c $(HEADERS) 
	$(CC) src\createNumber.c $(CFLAGS) /FoObjects\createNumber$(OBJEXT)

objects\getNum$(OBJEXT): src\getNum.c $(HEADERS) 
	$(CC) src\getNum.c $(CFLAGS) /FoObjects\getNum$(OBJEXT)

objects\giveOutput$(OBJEXT): src\giveOutput.c $(HEADERS) 
	$(CC) src\giveOutput.c $(CFLAGS) /FoObjects\giveOutput$(OBJEXT)

objects\numberGame$(OBJEXT): src\numberGame.c $(HEADERS) 
	$(CC) src\numberGame.c $(CFLAGS) /FoObjects\numberGame$(OBJEXT)
